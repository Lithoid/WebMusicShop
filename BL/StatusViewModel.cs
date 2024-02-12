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
    public class StatusViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }



        public StatusViewModel()
        {
        }
        public bool IsEmpty
        {
            get => Name == null;
        }


        public StatusViewModel(Status status)
        {
            Id = status.Id;
            Name = status.Name;

        }

        public static implicit operator Status(StatusViewModel model)
        {
            return new Status
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static StatusViewModel GetStatusById(Guid id, IStatusRepository repository)
        {
            return (repository.AllItems as DbSet<Status>)
                .Where(c => c.Id == id)
                .Select(c => new StatusViewModel(c))
                .FirstOrDefault();
        }
        public static StatusViewModel GetStatusByName(string name, IStatusRepository repository)
        {
            return (repository.AllItems as DbSet<Status>)
                .Where(c => c.Name == name)
                .Select(c => new StatusViewModel(c))
                .FirstOrDefault();
        }
        public static async Task<bool> Delete(Guid id, IStatusRepository repository)
        {
            return await repository.DeleteItemAsync(id);
        }

        public static async Task<bool> Edit(StatusViewModel model, IStatusRepository repository)
        {
            return await repository.ChangeItemAsync(model);
        }

        public static IQueryable<StatusViewModel> GetStatusList(IStatusRepository repository)
        {
            return (repository.AllItems as DbSet<Status>)
                .Select(p => new StatusViewModel(p));
        }


    }
}
