using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Entities;

namespace Repositories
{
    public interface ICategoryRepository : IDbRepository<Category>
    {
    }
}