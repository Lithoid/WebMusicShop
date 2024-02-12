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
    public class StatusRepository : DbRepository<Status>, IStatusRepository
    {
        public StatusRepository(DbContext context) : base(context)
        {

        }
    }
}