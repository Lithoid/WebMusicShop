using BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class IndexViewModel
    {
        
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<BrandViewModel> Brands { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
