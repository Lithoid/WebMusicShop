using BL;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApp.Models.Auth;
using Entities;
using Services.IServices;
using WebApp.Models;
using System.Linq;
using Services;

namespace WebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISendGridEmail _sendGridEmail;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        private readonly IFavouriteService _favouriteService;


        public ProfileController(SignInManager<AppUser> signInManager,
           UserManager<AppUser> userManager,
           ISendGridEmail sendGridEmail,
           IOrderService orderService,
           IReviewService reviewService,
           IProductService productService,
           IFavouriteService favouriteService)
        {
            _userManager = userManager;
            _sendGridEmail = sendGridEmail;
            _signInManager = signInManager;
            _orderService = orderService;
            _reviewService = reviewService;
            _productService = productService;
            _favouriteService = favouriteService;
        }
        public async Task<IActionResult> Profile()
        {
            ProfileViewModel model = new ProfileViewModel();
            var user = await _userManager.GetUserAsync(User);

            List<OrderViewModel> orders = new();
            var response = await _orderService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
            }

            List<ReviewViewModel> reviews = new();
             response = await _reviewService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                reviews = JsonConvert.DeserializeObject<List<ReviewViewModel>>(Convert.ToString(response.Result));
            }



            model.ReviewCount = reviews.Count();
            model.Orders = orders;
            model.User = user;

            return View(model);
        }
        public async Task<IActionResult> Details()
        {
            UserViewModel model = new UserViewModel();
            var user = await _userManager.GetUserAsync(User);

            List<OrderViewModel> orders = new();
            var response = await _orderService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
            }
            


            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            model.UserName = user.UserName;
            model.FullName = user.FullName;
            model.LastOrder = orders.FirstOrDefault();
            

            return PartialView("_Details",model);
        }
        

        public async Task<IActionResult> Orders()
        {
            ProfileViewModel model = new ProfileViewModel();
            var user = await _userManager.GetUserAsync(User);

            List<OrderViewModel> orders = new();
            var response = await _orderService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
            }




            return PartialView("_Orders", orders);
        }

        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            List<OrderViewModel> orders = new();
            var response = await _orderService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(Convert.ToString(response.Result));
            }
            model.LastOrder = orders.FirstOrDefault();


            if (ModelState.IsValid)
            {
                AppUser newUser = await _userManager.FindByEmailAsync(model.Email);
                if (newUser != null)
                {
                    newUser.UserName = model.UserName;
                    newUser.PhoneNumber = model.PhoneNumber;



                    IdentityResult result = await _userManager.UpdateAsync(newUser);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "Member");
                    }

                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    ModelState.AddModelError("Email", "What?");
                    return PartialView("_Details", model);
                }

                
            }
            return PartialView("_Details", model);
        }


        public async Task<IActionResult> Favourites()
        {
            var user = await _userManager.GetUserAsync(User);

            List<ProductViewModel> products = new();
            var response = await _productService.GetFavouriteForUser<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
            }

            return PartialView("_Favourite", products);
        }

        public async Task<IActionResult> RemoveFromFavourite(Guid prodId)
        {

            var user = await _userManager.GetUserAsync(User);

            var response = await _favouriteService.DeleteAsync<APIResponce>(prodId, user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {

            }
            return RedirectToAction("Favourites");
        }


    }
}
