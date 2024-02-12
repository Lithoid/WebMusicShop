using BL;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class DetailsViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
