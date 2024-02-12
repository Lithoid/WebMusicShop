using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    [Table("CartItemOrder")]
    public class CartItemOrder
    {
        [Column("CartItem Id")]
        public Guid CartItemId { get; set; }

        public CartItem CartItem { get; set; }

        [Column("Order Id")]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
