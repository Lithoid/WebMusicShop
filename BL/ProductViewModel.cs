using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace BL
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string About { get; set; }

        public int Quantity { get; set; }

        public Guid BrandId { get; set; }
        public string BrandName { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public decimal RetailPrice { get; set; }

        public decimal WholesalePrice { get; set; }

        public int ReviewCount { get; set; }

        public decimal Rate { get; set; }

        public List<Guid> UsersFavourite { get; set; } = new List<Guid>();

        public List<Guid> ImageIds { get; set; } = new List<Guid>();
        public Guid? Asset { get; set; }

        public ProductViewModel()
        {}

        public bool IsEmpty
        {
            get => Name == null;
        }

        public ProductViewModel(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            About = product.About;
            Quantity = product.Quantity;
            RetailPrice = product.RetailPrice;
            CategoryName = product.Category?.Name;
            CategoryId = product.CategoryId;
            BrandName = product.Brand?.Name;
            BrandId = product.BrandId;
            ImageIds = product.Assets.Select(a => a.Id).ToList();
            UsersFavourite = product.Favourites.Select(f => f.UserId).ToList();
            ReviewCount = product.Reviews.Count;
            Rate = product.Reviews.Select(r=>r.Rate).DefaultIfEmpty(0).Average();

        }

        public static implicit operator Product(ProductViewModel model)
        {
            return new Product
            {
                Id = model.Id,
                Description = model.Description,
                About = model.About,
                Quantity = model.Quantity,
                RetailPrice = model.RetailPrice,
                ProductAssets = model.ImageIds.Select(i => new ProductAsset { AssetId = i, ProductId = model.Id }).ToList(),
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                Name = model.Name,
                Favourites = model.UsersFavourite.Select(i => new Favourite { UserId = i, ProductId = model.Id }).ToList(),
            };
        }
    }
}