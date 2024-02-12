using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class AppIdentityContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppIdentityContext(DbContextOptions<Context.AppIdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      


            base.OnModelCreating(modelBuilder);
        }
    }
}