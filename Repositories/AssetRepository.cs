using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class AssetRepository : DbRepository<Asset>, IAssetRepository
    {
        public AssetRepository(DbContext context) : base(context)
        {
        }
    }
}