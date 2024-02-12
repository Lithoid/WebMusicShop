using BL;
using Domain;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;
using Services;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ShopController : Controller
    {


        private IProductService _productService;
        private ICategoryService _categoryService;
        private IBrandService _brandService;
        private IFavouriteService _favouriteService;

        private readonly UserManager<AppUser> _userManager;


        public ShopController(IProductService productService,
            ICategoryService categoryService,
            IBrandService brandService,
            UserManager<AppUser> userManager,
            IFavouriteService favouriteService)
        {



            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _userManager = userManager;
            _favouriteService = favouriteService;
        }


        public async Task<IActionResult> PartialList(Guid? categoryId, int page = 1, IFormCollection formCollection = null)
        {

            int pageSize = 3; // elems on page
            List<ProductViewModel> products = new();
            var response = await _productService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
            }

            if (categoryId.HasValue)
            {
                ViewData["Category"] = categoryId;
                response = await _productService.GetAllByCategoryAsync<APIResponce>(categoryId.Value, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
                }
            }
            if (formCollection.Count > 0)
            {
                var form = formCollection["formCollection"];
     
                var brands = formCollection["selectedBrands"].ToList();
                decimal min = Convert.ToDecimal(formCollection["min"]);
                decimal max = Convert.ToDecimal(formCollection["max"]);
                var order = formCollection["order"].ToString();




                products = products.Where(prod => (prod.RetailPrice > min && prod.RetailPrice < max)).ToList();
                if (brands.Count > 0)
                {
                    products = products.Where(prod => brands.Any(brand => brand.Equals(prod.BrandName))).ToList();
                }
                if (order == "priceAsc")
                    products = products.OrderBy(p => p.RetailPrice).ToList();
                if (order == "priceDesc")
                    products = products.OrderByDescending(p => p.RetailPrice).ToList();
                if (order == "rateDesc")
                    products = products.OrderBy(p => p.Rate).ToList();

            }


            var count = products.Count();
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            ListViewModel model = new ListViewModel();
            model.Products = items;
            model.PageViewModel = pageViewModel;

            return PartialView("_PartialList", model);
        }


        public async Task<IActionResult> List(Guid? id, Guid? categoryId, int page = 1, IFormCollection formCollection=null)
        {



            APIResponce response = new();
            ViewData["Category"] = categoryId;
            if (categoryId.HasValue)
            {
                CategoryViewModel category = new();
                response = await _categoryService.GetAsync<APIResponce>(categoryId.Value, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    category = JsonConvert.DeserializeObject<CategoryViewModel>(Convert.ToString(response.Result));
                }
                ViewData["CategoryName"] = category.Name;
            }
            else
            {
                ViewData["CategoryName"] = "List";
            }
            List<CategoryViewModel> categories = new();
            response = await _categoryService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(Convert.ToString(response.Result));
            }
            List<BrandViewModel> brandss = new();
            response = await _brandService.GetAllAsync<APIResponce>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                brandss = JsonConvert.DeserializeObject<List<BrandViewModel>>(Convert.ToString(response.Result));
            }
           
            IndexViewModel viewModel = new IndexViewModel
            {
                Categories = categories,
                Brands = brandss
            };
            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AddToFavourite(Guid prodId)
        {

            var user = await _userManager.GetUserAsync(User);


           
            FavouriteViewModel model = new FavouriteViewModel();
            model.ProductId = prodId;
            model.UserId = user.Id;


            var response = await _favouriteService.CreateAsync<APIResponce>(model, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return Ok("Added to favourite");
            }
            return Ok("Already in favourite");
        }
        
    }
}
