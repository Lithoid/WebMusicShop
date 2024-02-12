using BL;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class CreateEditProductViewModel
    {

        public IEnumerable<BrandViewModel> Brands { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
