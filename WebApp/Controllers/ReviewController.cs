using BL;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Entities;
using Services.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApp.Models;
using System.Linq;

namespace WebApp.Controllers
{
    public class ReviewController : Controller
    {
        private IProductService _productService;
        private IReviewService _reviewService;

        private readonly UserManager<AppUser> _userManager;

        public ReviewController(IProductService productService, IReviewService reviewService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _reviewService = reviewService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(string text,int rate, Guid productId)
        {
            if (rate>5||rate<0)
            {
                return Ok("Rate only between 0 and 5");
            }
            var user = await _userManager.GetUserAsync(User);
            List<ReviewViewModel> reviews = new();
            var response = await _reviewService.GetAllForUserAsync<APIResponce>(user.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                reviews = JsonConvert.DeserializeObject<List<ReviewViewModel>>(Convert.ToString(response.Result));
            }

            if (reviews.Count(r=>r.ProductId== productId)>0)
            {
                return Ok("You cant add more reviews");
            }
            ReviewViewModel review = new ReviewViewModel();
            review.Text = text;
            review.Rate = rate;
            review.Date = DateTime.Now;
            review.Likes = 0;
            review.UserName = _userManager.GetUserName(User);
            review.UserId = Guid.Parse(_userManager.GetUserId(User));
            review.ProductId = productId;

             response = await _reviewService.CreateAsync<APIResponce>(review, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Review created successfully";
                return Ok("Review created successfully");

            }

            return RedirectToAction("ReviewsList",new { productId = productId});
        }

        [HttpGet]
        public async Task<IActionResult> ReviewsList(Guid productId,bool topRated=false,bool isAll = false)
        {



            List<ReviewViewModel> reviews = new();
            var response = await _reviewService.GetAllForProductAsync<APIResponce>(productId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
                reviews = JsonConvert.DeserializeObject<List<ReviewViewModel>>(Convert.ToString(response.Result));

            List<ReviewViewModel> result = new();
            if (topRated)
            {
                reviews = reviews.OrderByDescending(r=>r.Likes).ToList();
            }
            if (isAll)
            {
                return PartialView("_Reviews", reviews);
            }

            return PartialView("_Reviews", reviews.Take(1));
        }
        [HttpPost]
        public async Task<IActionResult> LikeReview(Guid reviewId)
        {

            ReviewViewModel review = new ReviewViewModel();
            var response = await _reviewService.GetAsync<APIResponce>(reviewId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                review = JsonConvert.DeserializeObject<ReviewViewModel>(Convert.ToString(response.Result));

            }

            review.Likes += 1;

            response = await _reviewService.UpdateAsync<APIResponce>(review, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Review updated successfully";    

            }

            return RedirectToAction("ReviewsList", new { productId = review.ProductId, isAll = true });
        }

        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            ReviewViewModel review = new ReviewViewModel();
            var response = await _reviewService.GetAsync<APIResponce>(reviewId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                review = JsonConvert.DeserializeObject<ReviewViewModel>(Convert.ToString(response.Result));

            }

            response = await _reviewService.DeleteAsync<APIResponce>(reviewId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Review updated successfully";

            }

            return RedirectToAction("ReviewsList", new { productId = review.ProductId, isAll = true });
        }
    }
}
