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
    [Table("Reviews")]
    public class Review : DbEntity
    {
        [Column("Text")]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Column("UserName")]
        [MaxLength(1000)]
        public string UserName { get; set; }


        [Column("UserId")]
        [MaxLength(50)]
        public Guid UserId { get; set; }


        [Column("Rate")]
        public decimal Rate { get; set; }

        [Column("Likes")]
        public int Likes { get; set; }


        [Column("Date")]
        [MaxLength(50)]

        public DateTime Date { get; set; }


        public Product Product { get; set; }
        public Guid ProductId { get; set; }



    }
}
