using BL;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class MiscDataViewModel
    {

        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<BrandViewModel> Brands { get; set; }
    }
}
