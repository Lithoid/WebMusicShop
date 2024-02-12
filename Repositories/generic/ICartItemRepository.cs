using System;
using Domain;
using Entities;

namespace Repositories
{
    public interface ICartItemRepository : IDbRepository<CartItem>
    {
    }
}
