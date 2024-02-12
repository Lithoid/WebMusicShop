using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderViewModel
    {

        public Guid Id { get; set; }
        public Guid StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime OrderDate { get; set; }
        public string ClientPhone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ClientName { get; set; }

        [StringLength(100, ErrorMessage = "Please enter city", MinimumLength = 1)]
        [Required]
        public string City { get; set; }
        [Required]
        public int NovaPoshta { get; set; }

        public Guid UserId { get; set; }
        public decimal TotalSumm { get; set; }

        public List<Guid> CartItemIds { get; set; } = new List<Guid>();

        public bool IsEmpty
        {
            get => true;
        }
        public OrderViewModel()
        {

        }

        public OrderViewModel(Order order)
        {
            Id = order.Id;
            StatusId = order.StatusId;
            StatusName = order.Status.Name;
            OrderDate = order.OrderDate;
            ClientPhone = order.ClientPhone;
            Email = order.Email;
            ClientName = order.ClientName;
            City =  order.City;
            NovaPoshta = order.NovaPoshta;
            UserId = order.UserId;
            TotalSumm = order.TotalSumm;

            CartItemIds = order.CartItems.Select(c => c.Id).ToList();
        }

        public static implicit operator Order(OrderViewModel model)
        {
            return new Order
            {
                Id = model.Id,
                StatusId = model.StatusId,
                OrderDate = model.OrderDate,
                ClientPhone = model.ClientPhone,
                Email = model.Email,
                ClientName = model.ClientName,
                City = model.City,
                NovaPoshta = model.NovaPoshta,
                CartItemOrders = model.CartItemIds.Select(i => new CartItemOrder { CartItemId = i, OrderId = model.Id }).ToList(),
                UserId = model.UserId,
                TotalSumm = model.TotalSumm,

            };
        }
   

    }
}
