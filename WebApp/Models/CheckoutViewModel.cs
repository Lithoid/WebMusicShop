using BL;
using System;

namespace WebApp.Models
{
    public class CheckoutViewModel
    {
        public string OwnerName { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public int CVV { get; set; }

        public Guid RelatedOrderId { get; set; }
    }
}
