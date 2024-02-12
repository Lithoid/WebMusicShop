using System;
using Domain;
using Entities;

namespace Repositories
{
    public interface IProductRepository : IDbRepository<Product>
    {
    }
}