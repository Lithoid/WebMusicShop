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
using Domain;
using Newtonsoft.Json;

using WebApp.Models;
using Services.IServices;

namespace WebApp.Controllers
{

    [Authorize]
    public class CartItemController : Controller
    {

     
        private IProductService _productService;
        private ICartItemService _cartItemService;
        private readonly UserManager<AppUser> _userManager;


        public CartItemController(UserManager<AppUser> userManager, IProductService productService, ICartItemService cartItemService)
        {
       
            _userManager = userManager;

            _productService = productService;
            _cartItemService = cartItemService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Add(Guid id)
        {
            var userId = Guid.Parse(GetCartId());
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(_userManager.GetUserId(User));
            }

            //GetProduct
            ProductViewModel product= new();
            var response = await _productService.GetAsync<APIResponce>(id,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));
            }

            if (product == null)
                return Redirect("~/CartItem/List");

            //GetCartItem
            CartViewModel cartView = new();
            response = await _cartItemService.GetAsync<APIResponce>(product.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                cartView = JsonConvert.DeserializeObject<CartViewModel>(Convert.ToString(response.Result));
            }

            if (cartView == null || cartView.CartId != userId)
            {
                cartView = new CartViewModel
                {
                    Id = Guid.NewGuid(),
                    CartId = userId,
                    Quantity = 1,
                    DateCreated = DateTime.Now,
                    ProductId = product.Id,
                    ProductName = product.Name
                };
                response = await _cartItemService.CreateAsync<APIResponce>(cartView, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "CartItem created successfully";
                    return RedirectToAction(nameof(List));
                }

            }
            return Redirect("~/CartItem/List");
        }

        public async Task<IActionResult> List()
        {
            var userId = Guid.Parse(GetCartId());
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(_userManager.GetUserId(User));
            }
            var items = new List<CartViewModel>();

            var response = await _cartItemService.GetAllAsync<APIResponce>(userId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                items = JsonConvert.DeserializeObject<List<CartViewModel>>(Convert.ToString(response.Result));
            }

            ViewBag.Subtotal = 0;
            foreach (var item in items)
            {
                ViewBag.Subtotal += item.Quantity * item.Price;
            }
            



            if (items.Count()==0)
            {
                return Redirect("~/Shop/List");

            }
            return View(items);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {

            var response = await _cartItemService.DeleteAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "CartItem deleted successfully";
                return Redirect("~/CartItem/List");
            }
            return Redirect("~/CartItem/List");
        }

        public async Task<IActionResult> DeleteAll()
        {
            var userId = Guid.Parse(GetCartId());
            if (User.Identity.IsAuthenticated)
            {
                userId = Guid.Parse(_userManager.GetUserId(User));
            }

            var items = new List<CartViewModel>();

            var response = await _cartItemService.GetAllAsync<APIResponce>(userId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                items = JsonConvert.DeserializeObject<List<CartViewModel>>(Convert.ToString(response.Result));
            }

            foreach (var item in items)
            {
                response = await _cartItemService.DeleteAsync<APIResponce>(item.Id, HttpContext.Session.GetString(SD.SessionToken));
            }
         
            return Redirect("~/Shop/List");
        }

      

        private string GetCartId()
        {

            if (HttpContext.Session.Keys.Contains("CartId"))
            {

                return HttpContext.Session.GetString("CartId");
            }
            else
            {
                // Generate a new random GUID using System.Guid class.     
                Guid tempCartId = Guid.NewGuid();
                HttpContext.Session.SetString("CartId", tempCartId.ToString());
            }

            return HttpContext.Session.GetString("CartId");
        }



    }
}
