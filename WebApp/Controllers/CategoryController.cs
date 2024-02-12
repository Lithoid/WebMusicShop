 using BL;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;


namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        private IBrandService _brandService;

        public CategoryController(ICategoryService categoryService, IBrandService brandService)
        {
            _categoryService = categoryService;
            _brandService = brandService;
        }

        public async Task<IActionResult> List()
        {

            MiscDataViewModel items = new MiscDataViewModel();

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

            items.Categories = categories;
            items.Brands = brands;


            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(IFormCollection formCollection)
        {
            string name = formCollection["Name"];
           
            
            CategoryViewModel category = new CategoryViewModel() { Name = name };
            var response = await _categoryService.CreateAsync<APIResponce>(category, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(List));
            }

            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> CreateBrand(IFormCollection formCollection)
        {
            string name = formCollection["Name"];


            BrandViewModel brand = new BrandViewModel() { Name = name };
            var response = await _brandService.CreateAsync<APIResponce>(brand, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(List));
            }
            return RedirectToAction(nameof(List));
        }
        public async Task<IActionResult> DeleteCategory(Guid? id)
        {
            var response = await _categoryService.DeleteAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
                return Redirect("~/Category/List");
            }
            return Redirect("~/Category/List");
        }
        public async Task<IActionResult> DeleteBrand(Guid? id)
        {
            var response = await _brandService.DeleteAsync<APIResponce>(id.Value, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Brand deleted successfully";
                return Redirect("~/Category/List");
            }
            return Redirect("~/Category/List");
        }
       
    }
}
