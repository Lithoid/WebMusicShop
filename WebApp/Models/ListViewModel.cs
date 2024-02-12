using BL;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class ListViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
