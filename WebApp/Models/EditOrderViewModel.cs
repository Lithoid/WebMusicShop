using BL;
using System.Collections.Generic;

namespace WebApp.Models
{
    
        public class EditOrderViewModel
        {
            public OrderViewModel Order { get; set; }
            public IEnumerable<StatusViewModel> Statuses { get; set; }

        }
    
}
