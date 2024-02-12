using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;



namespace Entities
{
    [Table("CartItems")]
    public class CartItem:DbEntity
    {

        [Column("CartId")]
        [MaxLength(50)]
        public Guid CartId { get; set; }
        [Column("Quantity")]
        [MaxLength(50)]
        public int Quantity { get; set; }
        [Column("DateCreated")]
        [MaxLength(50)]
        public System.DateTime DateCreated { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }



        public List<Order> Orders { get; set; }
        public List<CartItemOrder> CartItemOrders { get; set; }



    }
}
