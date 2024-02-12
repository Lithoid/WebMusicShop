using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Context
{
    public class AppDataContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<CartItem> CartItems { get; set; }
        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(
                    prod =>
                    {
                        prod.Property(p => p.RetailPrice)
                            .HasColumnType("money");

                        prod.HasOne(p => p.Category)
                            .WithMany(c => c.Products)
                            .HasForeignKey(p => p.CategoryId);

                        prod.HasOne(p => p.Brand).WithMany(b => b.Products).HasForeignKey(p => p.BrandId);

                        prod.HasMany(p => p.Assets)
                            .WithMany(a => a.Products)
                            .UsingEntity<ProductAsset>(
                                pa => pa.HasOne(p => p.Asset)
                                    .WithMany(a => a.ProductAssets)
                                    .HasForeignKey(a => a.AssetId),

                                pa => pa.HasOne(p => p.Product)
                                    .WithMany(a => a.ProductAssets)
                                    .HasForeignKey(a => a.ProductId),

                                pa => pa.HasKey(
                                    qa => new { qa.ProductId, qa.AssetId })
                                );


                   

                    });

            modelBuilder.Entity<Order>(
                or =>
                {
                    or.HasOne(o=>o.Status)
                           .WithMany(s => s.Orders)
                           .HasForeignKey(o =>o.StatusId);
                    or.Property(r => r.TotalSumm).HasColumnType("money");


                });

            modelBuilder.Entity<CartItem>(
                  cart =>
                  {
                      cart.HasOne(c => c.Product)
                          .WithMany(p => p.cartItems)
                          .HasForeignKey(c => c.ProductId);

                      cart.HasMany(c => c.Orders)
                           .WithMany(o => o.CartItems)
                           .UsingEntity<CartItemOrder>(
                               co => co.HasOne(c => c.Order)
                                   .WithMany(o => o.CartItemOrders)
                                   .HasForeignKey(o => o.OrderId),

                               co => co.HasOne(c => c.CartItem)
                                   .WithMany(c => c.CartItemOrders)
                                   .HasForeignKey(o => o.CartItemId),

                               co => co.HasKey(
                                   qa => new { qa.CartItemId, qa.OrderId })
                               );

                  });


            modelBuilder.Entity<Review>(
               review =>
                {
                    review.Property(r=>r.Rate).HasPrecision(4,2);

                    review.HasOne(r => r.Product)
                           .WithMany(p => p.Reviews)
                           .HasForeignKey(r => r.ProductId);


                });

            modelBuilder.Entity<Favourite>(
            favourite =>
            {

                favourite.HasOne(f => f.Product)
                          .WithMany(p => p.Favourites)
                          .HasForeignKey(f => f.ProductId);


            });





            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = Guid.NewGuid(), Name = "Electric guitar" },
                new Category() { Id = Guid.NewGuid(), Name = "Classical guitar" },
                new Category() { Id = Guid.NewGuid(), Name = "Accesories" },
                new Category() { Id = Guid.NewGuid(), Name = "Amplifiers" },
                new Category() { Id = Guid.NewGuid(), Name = "Studio equipment" },
                new Category() { Id = Guid.NewGuid(), Name = "Cabinets" },
                new Category() { Id = Guid.NewGuid(), Name = "Proccesors" }
                );
            modelBuilder.Entity<Status>().HasData(
               new Category() { Id = Guid.NewGuid(), Name = "Pending" },
               new Category() { Id = Guid.NewGuid(), Name = "Canceled" },
               new Category() { Id = Guid.NewGuid(), Name = "Sended" }

               );
            modelBuilder.Entity<Brand>().HasData(
                new Brand() { Id = Guid.NewGuid(), Name = "Ibanez" },
                new Brand() { Id = Guid.NewGuid(), Name = "ESP" },
                new Brand() { Id = Guid.NewGuid(), Name = "Fender" },
                new Brand() { Id = Guid.NewGuid(), Name = "Marshal" },
                new Brand() { Id = Guid.NewGuid(), Name = "Line6" },
                new Brand() { Id = Guid.NewGuid(), Name = "Yamaha" });
            base.OnModelCreating(modelBuilder);
        }
    }
}