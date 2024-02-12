using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Repositories;
using Microsoft.AspNetCore.Authorization;
using WebApp.Models;
using Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.IServices;
using Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Controllers
{

    
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private IReviewService _reviewService;
        private IBrandService _brandService;

        private readonly UserManager<AppUser> _userManager;

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService, IReviewService reviewService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(string name,string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder== "Name" ? "name_desc" : "Name";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "category_desc" : "Category";

            List<ProductViewModel> products = new();
            var response = await _productService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
            }


            switch (sortOrder)
            {
                case "Name":
                    products = products.OrderBy(s => s.Name).ToList();
                    break;
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name).ToList();
                    break;
                case "Price":
                    products = products.OrderBy(s => s.RetailPrice).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.RetailPrice).ToList();
                    break;
                case "Category":
                    products = products.OrderBy(s => s.CategoryName).ToList();
                    break;
                case "category_desc":
                    products = products.OrderByDescending(s => s.CategoryName).ToList();
                    break;
            }

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name)).ToList();
            }

            return View(products);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductViewModel product)
        {

            
            if (product.IsEmpty)
            {
                List<CategoryViewModel> categories = new();
                var response = await _categoryService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(Convert.ToString(response.Result));
                }

                List<BrandViewModel> brands = new();
                response = await _brandService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    brands = JsonConvert.DeserializeObject<List<BrandViewModel>>(Convert.ToString(response.Result));
                }

                CreateEditProductViewModel model = new CreateEditProductViewModel();
                model.Product = product;
                model.Brands = brands;
                model.Categories = categories;


                return View(model);
            }

            if (ModelState.IsValid)
            {

                var response = await _productService.CreateAsync<APIResponce>(product, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(List));
                }
            }

            return Redirect("Error");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductViewModel product = new();
            var response = await _productService.GetAsync<APIResponce>(id,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));
            }
            List<CategoryViewModel> categories = new();
            response = await _categoryService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(Convert.ToString(response.Result));
            }

            List<BrandViewModel> brands = new();
            response = await _brandService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                brands = JsonConvert.DeserializeObject<List<BrandViewModel>>(Convert.ToString(response.Result));
            }

            CreateEditProductViewModel model = new CreateEditProductViewModel();
            model.Product = product;
            model.Brands = brands;
            model.Categories = categories;

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptEdit(CreateEditProductViewModel model)
        {
            if (model!=null)
            {
                var response = await _productService.UpdateAsync<APIResponce>(model.Product, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return Redirect("~/Product/List");
                }
            }
            return Redirect("~/Product/List");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ProductViewModel product = new();
            var response = await _productService.GetAsync<APIResponce>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));
            }

            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AcceptDelete(Guid? id)
        {
            var response = await _productService.DeleteAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return Redirect("~/Product/List");
            }
            return Redirect("~/Product/List");
        }

        public async Task<IActionResult> Details(Guid id)
        {
            DetailsViewModel model = new DetailsViewModel();

            ProductViewModel product = new();
            var response = await _productService.GetAsync<APIResponce>(id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
                product = JsonConvert.DeserializeObject<ProductViewModel>(Convert.ToString(response.Result));



            List<ProductViewModel> products = new();
            response = await _productService.GetAllByCategoryAsync<APIResponce>(product.CategoryId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));


            List<ReviewViewModel> reviews = new();
            response = await _reviewService.GetAllForProductAsync<APIResponce>(product.Id,HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
                reviews = JsonConvert.DeserializeObject<List<ReviewViewModel>>(Convert.ToString(response.Result));



            model.Product = product;
            model.Products = products;
            model.Reviews = reviews;

            return View(model);
        }

    }
}