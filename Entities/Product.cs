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
    [Table("Products")]
    public class Product : DbEntity
    {
        [Column("Name")]
        [MaxLength(64)]
        public string Name { get; set; }
        [Column("Description")]
        [MaxLength(512)]
        public string Description { get; set; }
        [Column("About")]
        [MaxLength(512)]
        public string About { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }




        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }


        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CartItem> cartItems { get; set; }
        public List<Review> Reviews { get; set; }

        public List<Asset> Assets { get; set; }
        public List<ProductAsset> ProductAssets { get; set; }

        public List<Favourite> Favourites { get; set; }



        public decimal RetailPrice { get; set; }
    }
}