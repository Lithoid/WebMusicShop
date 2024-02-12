using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    [Table("Orders")]
    public class Order :DbEntity
    {
        [Column("OrderDate")]
        [MaxLength(50)]
      
        public DateTime OrderDate { get; set; }
        [Column("ClientPhone")]
        [MaxLength(50)]
      
        public string ClientPhone { get; set; }
        [Column("ClientName")]
        [MaxLength(50)]
       
        public string ClientName { get; set; }
        [Column("Email")]
        [MaxLength(50)]
      
        public string Email { get; set; }

        [Column("City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Column("NovaPoshta")]
        public int NovaPoshta { get; set; }
        [Column("TotalSumm")]
        public decimal TotalSumm { get; set; }

        [Column("UserId")]
        public Guid UserId { get; set; }


        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<CartItemOrder> CartItemOrders { get; set; }




    }
}
