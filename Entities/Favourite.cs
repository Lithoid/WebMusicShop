using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{

    [Table("Favourites")]
    public class Favourite:DbEntity
    {

        [Column("ProductId")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }


        [Column("UserId")]
        public Guid UserId { get; set; }
    }
}
