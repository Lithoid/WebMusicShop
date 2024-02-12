using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("ProductAsset")]
    public class ProductAsset
    {
        [Column("Product Id")]
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        [Column("Asset Id")]
        public Guid AssetId { get; set; }

        public Asset Asset { get; set; }
    }
}