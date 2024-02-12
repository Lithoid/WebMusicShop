using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Repositories
{
    public class BrandRepository : DbRepository<Brand>, IBrandRepository
    {
        public BrandRepository(DbContext context) : base(context)
        {

        }
    }
}