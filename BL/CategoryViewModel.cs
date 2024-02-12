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
    public class CategoryViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }



        public CategoryViewModel()
        {
        }
        public bool IsEmpty
        {
            get => Name == null;
        }


        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;

        }

        public static implicit operator Category(CategoryViewModel model)
        {
            return new Category
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static CategoryViewModel GetCategoryById(Guid id, ICategoryRepository repository)
        {
            return (repository.AllItems as DbSet<Category>)
                .Where(c => c.Id == id)
                .Select(c => new CategoryViewModel(c))
                .FirstOrDefault();
        }
        public static async Task<bool> Delete(Guid id, ICategoryRepository repository)
        {
            return await repository.DeleteItemAsync(id);
        }

        public static async Task<bool> Edit(CategoryViewModel model, ICategoryRepository repository)
        {
            return await repository.ChangeItemAsync(model);
        }

        public static IQueryable<CategoryViewModel> GetCategoryList(ICategoryRepository repository)
        {
            return (repository.AllItems as DbSet<Category>)
                .Select(p => new CategoryViewModel(p));
        }


    }
}
