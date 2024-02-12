using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CartItemRepository:DbRepository<CartItem>,ICartItemRepository
    {
        public CartItemRepository(DbContext context) : base(context)
        {

        }
    }
}
