using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Entities;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using Domain;
using Newtonsoft.Json;
using Services.IServices;
using Services;
using System.Net.Http;
using EllipticCurve;
using System.Text;
using System.Security.Cryptography;
using MailKit.Search;
using Hangfire;

namespace WebApp.Controllers
{
    public class OrderController : Controller
    {



        private ICartItemService _cartItemService;
        private IStatusService _statusService;
        private IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(UserManager<AppUser> userManager,
            ICartItemService cartItemService,
            IStatusService statusService,
            IOrderService orderService,
            IHttpClientFactory httpClientFactory)
        {



            _userManager = userManager;
            _cartItemService = cartItemService;
            _statusService = statusService;
            _orderService = orderService;
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> MakeOrder(Guid? cartId)
        {
            /*if (cartId.HasValue)
            {
                var items = CartViewModel.GetCartItemList(_cartItemRepository, cartId);
                OrderViewModel model = new OrderViewModel()
                {
                    Status = "Waiting",
                    ClientName = _userManager.GetUserName(User),
                    ClientPhone = "123",//_userManager.GetPhoneNumberAsync(user),
                    OrderDate = DateTime.Now,
                    CartItemIds = items.Select(c => c.Id).ToList()

                };
                if (await _orderRepository.AddItemAsync(model))
                {
                    return Redirect("~/Shop/List");

                }
            }*/
            var user = await _userManager.GetUserAsync(User);

            if (cartId.HasValue)
            {
                ViewData["cartId"] = cartId;
            }
            var cartItems = new List<CartViewModel>();

            var response = await _cartItemService.GetAllAsync<APIResponce>(cartId.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                cartItems = JsonConvert.DeserializeObject<List<CartViewModel>>(Convert.ToString(response.Result));
            }



            CartOrderViewModel model = new CartOrderViewModel();
            model.Order = new OrderViewModel();

            model.Order.ClientName = user.FullName;
            model.Order.ClientPhone = user.PhoneNumber;
            model.Order.Email = user.Email;
            model.CartItems = cartItems;

            ViewBag.Subtotal = 0;
            foreach (var item in model.CartItems)
            {
                ViewBag.Subtotal += item.Quantity * item.Price;
            }

            return View("MakeOrder", model);
        }
        public async Task<IActionResult> AcceptOrder(CartOrderViewModel model, Guid? cartId, string submitButton)
        {
            if (ModelState.IsValid)
            {
                var cartItems = new List<CartViewModel>();

                var response = await _cartItemService.GetAllAsync<APIResponce>(cartId.Value, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                    cartItems = JsonConvert.DeserializeObject<List<CartViewModel>>(Convert.ToString(response.Result));
                
                StatusViewModel status = new();
                response = await _statusService.GetAsync<APIResponce>("Pending", HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                    status = JsonConvert.DeserializeObject<StatusViewModel>(Convert.ToString(response.Result));
     


                model.Order.Id = Guid.NewGuid();
                model.Order.OrderDate = DateTime.Now;
                model.Order.StatusName = status.Name;
                model.Order.StatusId = status.Id;
                model.Order.CartItemIds = cartItems.Select(c => c.Id).ToList();
                model.Order.UserId = Guid.Parse(_userManager.GetUserId(User));

                foreach (var item in cartItems)
                    model.Order.TotalSumm += item.Price * item.Quantity;


                response = await _orderService.CreateAsync<APIResponce>(model.Order, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                    TempData["success"] = "Order created successfully";

                string jobId = "";
                switch (submitButton)
                {
                    case "Go to payment":
                        {
                            return RedirectToAction("Checkout", new { orderId = model.Order.Id });
                        }
                    case "Pay later":
                        {
                            jobId = BackgroundJob.Schedule(() => ChangeStatus(model.Order.Id), TimeSpan.FromSeconds(40));
                            return RedirectToAction("List", "Shop");
                        }    
                }
                return RedirectToAction("List", "Shop");

            }
            else
            {
                var cartItems = new List<CartViewModel>();

                var response = await _cartItemService.GetAllAsync<APIResponce>(cartId.Value, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    cartItems = JsonConvert.DeserializeObject<List<CartViewModel>>(Convert.ToString(response.Result));
                }
                model.CartItems = cartItems;
                ViewBag.Subtotal = 0;
                if (cartId.HasValue)
                {
                    ViewData["cartId"] = cartId;
                }
                foreach (var item in model.CartItems)
                {
                    ViewBag.Subtotal += item.Quantity * item.Price;
                }
                return View("MakeOrder", model);
            }

        }

        public async Task ChangeStatus(Guid orderId)
        {
            OrderViewModel order = new();
            var response = await _orderService.GetAsync<APIResponce>(orderId,"token must be here");
            if (response != null && response.IsSuccess)
            {
                order = JsonConvert.DeserializeObject<OrderViewModel>(Convert.ToString(response.Result));
            }
            StatusViewModel status = new();
            response = await _statusService.GetAsync<APIResponce>("Canceled", "token must be here");
            if (response != null && response.IsSuccess)
            {
                status = JsonConvert.DeserializeObject<StatusViewModel>(Convert.ToString(response.Result));
            }
            if (order.StatusName!="Sended")
            {
                order.StatusId = status.Id;
                response = await _orderService.UpdateAsync<APIResponce>(order, "token must be here");

            }
           

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            List<OrderViewModel> orders = new();
            var response = await _orderService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
            }
            orders = orders.OrderBy(o => o.OrderDate).ToList();
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            OrderViewModel order = new();
            var response = await _orderService.GetAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                order = JsonConvert.DeserializeObject<OrderViewModel>(Convert.ToString(response.Result));
            }

            var statuses = new List<StatusViewModel>();
            response = await _statusService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                statuses = JsonConvert.DeserializeObject<List<StatusViewModel>>(Convert.ToString(response.Result));
            }



            EditOrderViewModel model = new EditOrderViewModel();
            model.Order = order;
            model.Statuses = statuses;



            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptEdit(EditOrderViewModel model)
        {
            if (model.Order != null)
            {
                var response = await _orderService.UpdateAsync<APIResponce>(model.Order, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return Redirect("~/Order/List");
                }
            }
            return Redirect("~/Order/List");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            var response = await _orderService.DeleteAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order deleted successfully";
                return Redirect("~/Order/List");
            }
            return Redirect("~/Order/List");
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model,string jobId="")
        {


            var publicKey = "sandbox_i51859305739";
            var privateKey = "sandbox_Am3LUBSoDRs04JeMEa7TgmtM4tWiPDcYeWCC9ahp";

            Dictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("public_key", publicKey);
            parameters.Add("private_key", privateKey);
            parameters.Add("action", "pay");
            parameters.Add("version", "3");
            parameters.Add("phone", "380950000001");
            parameters.Add("amount", "1");
            parameters.Add("currency", "USD");
            parameters.Add("description", "description text");
            parameters.Add("order_id", Guid.NewGuid().ToString());
            parameters.Add("card", model.CardNumber);
            parameters.Add("card_exp_month", "03");
            parameters.Add("card_exp_year", "22");
            parameters.Add("card_cvv", "111");



            var httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri("https://www.liqpay.ua/api/request");

            var jsonContent = JsonConvert.SerializeObject(parameters);
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonContent);
            var data = System.Convert.ToBase64String(plainTextBytes);

            var rawSignature = privateKey + data + privateKey;
            using var sha1 = SHA1.Create();
            var signature = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(rawSignature)));



            Dictionary<String, String> mainParams = new Dictionary<String, String>();
            mainParams.Add("data", data);
            mainParams.Add("signature", signature);

            //var jsonMainParams = JsonConvert.SerializeObject(mainParams);

            message.Content = new FormUrlEncodedContent(mainParams);
            message.Method = HttpMethod.Post;


            HttpResponseMessage httpResponseMessage = null;
            httpResponseMessage = await httpClient.SendAsync(message);

            var stringContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var responce = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringContent);

            if (responce["result"] == "error")
            {
                ViewBag.ErrorCode = responce["err_code"];
                ViewBag.ErrorDesc = responce["err_description"];
            }
            if (responce["result"] == "ok")
            {
                OrderViewModel order = new();
                var response = await _orderService.GetAsync<APIResponce>(model.RelatedOrderId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    order = JsonConvert.DeserializeObject<OrderViewModel>(Convert.ToString(response.Result));
                }
                StatusViewModel status = new();
                response = await _statusService.GetAsync<APIResponce>("Sended", HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    status = JsonConvert.DeserializeObject<StatusViewModel>(Convert.ToString(response.Result));
                }

                order.StatusId = status.Id;
                response = await _orderService.UpdateAsync<APIResponce>(order, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {

                }
              

                ViewBag.ErrorCode = "Payment successful";
                ViewBag.ErrorDesc = "Traction has reached its destination";
            }

            return View("Result");
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(Guid orderId)
        {

            ViewBag.OrderId = orderId;
            return View();
        }



        public async Task<IActionResult> Details(Guid orderId)
        {
            OrderViewModel order = new();
            var response = await _orderService.GetAsync<APIResponce>(orderId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                order = JsonConvert.DeserializeObject<OrderViewModel>(Convert.ToString(response.Result));
            }

            var cartItems = new List<CartViewModel>();
            var user = await _userManager.GetUserAsync(User);

            foreach (var itemId in order.CartItemIds)
            {
                response = await _cartItemService.GetAsync<APIResponce>(itemId, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    CartViewModel cartItem = JsonConvert.DeserializeObject<CartViewModel>(Convert.ToString(response.Result));
                    cartItems.Add(cartItem);
                }
            }
           



            CartOrderViewModel model = new CartOrderViewModel();
            model.Order = order;
            model.CartItems = cartItems;


            return View(model);

        }
    }
}
