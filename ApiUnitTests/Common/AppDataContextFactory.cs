using Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUnitTests.Common
{
    public class AppDataContextFactory
    {

        public static Guid BrandId = Guid.NewGuid();
        public static Guid CategoryId = Guid.NewGuid();

        public static AppDataContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDataContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new AppDataContext(options);
            context.Database.EnsureCreated();
            context.Products.AddRange(
                new Product
                {
                    Id=Guid.Parse("BD85443B-A06B-4D03-924F-8C92377309F5"),
                    Name = "Name1",
                    About = "About1",
                    Description = "Desc1",
                    RetailPrice = 0,
                    BrandId = BrandId,
                    CategoryId = CategoryId,
                    Quantity= 0,
                },
                new Product
                {
                    Id = Guid.Parse("91B6F800-FE97-4F15-94D3-437BBF749C84"),
                    Name = "Name2",
                    About = "About2",
                    Description = "Desc2",
                    RetailPrice = 0,
                    BrandId = BrandId,
                    CategoryId = CategoryId,
                    Quantity = 0,
                },
                new Product
                {
                    Id = Guid.Parse("4DC9E52E - 7B7C - 403A - BD2E - 0A97EAE83928"),
                    Name = "Name2",
                    About = "About2",
                    Description = "Desc2",
                    RetailPrice = 0,
                    BrandId = BrandId,
                    CategoryId = CategoryId,
                    Quantity = 0,
                },
                new Product
                {
                    Id = Guid.Parse("DE627A51-8FB9-4909-9EFC-87F427B0780B"),
                    Name = "Name2",
                    About = "About2",
                    Description = "Desc2",
                    RetailPrice = 0,
                    BrandId = BrandId,
                    CategoryId = CategoryId,
                    Quantity = 0,
                }




                );

            context.SaveChanges();
            return context;
        }
        public static void Destroy(AppDataContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            throw new NotImplementedException();
        }
    }
}
