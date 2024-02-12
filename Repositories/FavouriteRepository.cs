using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FavouriteRepository : DbRepository<Favourite>, IFavouriteRepository
    {
        public FavouriteRepository(DbContext context) : base(context)
        {

        }

    }
}
