using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BrandViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }



        public BrandViewModel()
        {
        }
        public bool IsEmpty
        {
            get => Name == null;
        }


        public BrandViewModel(Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
            ProductCount = brand.Products.Count;

        }

        public static implicit operator Brand(BrandViewModel model)
        {
            return new Brand
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static BrandViewModel GetBrandById(Guid id, IBrandRepository repository)
        {
            return (repository.AllItems as DbSet<Brand>)
                .Where(c => c.Id == id)
                .Select(c => new BrandViewModel(c))
                .FirstOrDefault();
        }
        public static async Task<bool> Delete(Guid id, IBrandRepository repository)
        {
            return await repository.DeleteItemAsync(id);
        }

        public static async Task<bool> Edit(BrandViewModel model, IBrandRepository repository)
        {
            return await repository.ChangeItemAsync(model);
        }

        public static IQueryable<BrandViewModel> GetBrandList(IBrandRepository repository)
        {
            return (repository.AllItems as DbSet<Brand>)
                .Select(p => new BrandViewModel(p));
        }


    }
}
