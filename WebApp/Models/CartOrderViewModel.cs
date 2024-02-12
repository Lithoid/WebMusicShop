
using BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebApp.Models
{
    public class CartOrderViewModel
    {
        public OrderViewModel Order { get; set; }
        public IEnumerable<CartViewModel> CartItems { get; set; }

    }
}
