﻿// <auto-generated />
using System;
using Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace StoreWebApi.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20221228131704_migration")]
    partial class migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Entities.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("FileExtention")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("ext");

                    b.Property<string>("FileName")
                        .HasMaxLength(260)
                        .HasColumnType("nvarchar(260)")
                        .HasColumnName("fileName");

                    b.Property<string>("MimeType")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("mime");

                    b.Property<string>("OriginalFileName")
                        .HasMaxLength(260)
                        .HasColumnType("nvarchar(260)")
                        .HasColumnName("original");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b14da30a-9de6-4702-92e3-7d891ea8c409"),
                            Name = "Ibanez"
                        },
                        new
                        {
                            Id = new Guid("e398c16f-4fe3-4989-b2a7-39d2119e989c"),
                            Name = "ESP"
                        },
                        new
                        {
                            Id = new Guid("736cf5ed-f97e-41e6-bc38-8612cd9bbf7a"),
                            Name = "Fender"
                        },
                        new
                        {
                            Id = new Guid("64c00f60-709e-439c-b665-2fdba13b7eb3"),
                            Name = "Marshal"
                        },
                        new
                        {
                            Id = new Guid("cc211be6-2fbc-4742-99ae-49a0e68ecf8d"),
                            Name = "Line6"
                        },
                        new
                        {
                            Id = new Guid("b7d663be-1dcb-4c52-b850-48afab543a40"),
                            Name = "Yamaha"
                        });
                });

            modelBuilder.Entity("Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("CartId")
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CartId");

                    b.Property<DateTime>("DateCreated")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("DateCreated");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Entities.CartItemOrder", b =>
                {
                    b.Property<Guid>("CartItemId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CartItem Id");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Order Id");

                    b.HasKey("CartItemId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("CartItemOrder");
                });

            modelBuilder.Entity("Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6cdcd2c2-7ba9-4a17-a099-9e048e5cfe3c"),
                            Name = "Electric guitar"
                        },
                        new
                        {
                            Id = new Guid("ec9996d6-c928-4ede-b056-fbc1141c8e1d"),
                            Name = "Classical guitar"
                        },
                        new
                        {
                            Id = new Guid("923b05d5-c9e1-4d60-89b1-aa8352cea9ee"),
                            Name = "Accesories"
                        },
                        new
                        {
                            Id = new Guid("6fc24cd9-cf55-4073-8eed-de465199aa10"),
                            Name = "Amplifiers"
                        },
                        new
                        {
                            Id = new Guid("754866e3-e3df-4401-9aac-0644e77416aa"),
                            Name = "Studio equipment"
                        },
                        new
                        {
                            Id = new Guid("d290f631-faf9-43b1-8420-8309569dc07f"),
                            Name = "Cabinets"
                        },
                        new
                        {
                            Id = new Guid("17ee9bc2-3c92-46b7-98ac-f723f634cd49"),
                            Name = "Proccesors"
                        });
                });

            modelBuilder.Entity("Entities.Favourite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductId");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("City");

                    b.Property<string>("ClientName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ClientName");

                    b.Property<string>("ClientPhone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ClientPhone");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Email");

                    b.Property<int>("NovaPoshta")
                        .HasColumnType("int")
                        .HasColumnName("NovaPoshta");

                    b.Property<DateTime>("OrderDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("OrderDate");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalSumm")
                        .HasColumnType("money")
                        .HasColumnName("TotalSumm");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("About")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasColumnName("About");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)")
                        .HasColumnName("Name");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<decimal>("RetailPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Entities.ProductAsset", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Product Id");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Asset Id");

                    b.HasKey("ProductId", "AssetId");

                    b.HasIndex("AssetId");

                    b.ToTable("ProductAsset");
                });

            modelBuilder.Entity("Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("Date")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2")
                        .HasColumnName("Date");

                    b.Property<int>("Likes")
                        .HasColumnType("int")
                        .HasColumnName("Likes");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Rate")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)")
                        .HasColumnName("Rate");

                    b.Property<string>("Text")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("Text");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Entities.Status", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c946afe4-2e8f-4c97-8296-89ee107486ac"),
                            Name = "Pending"
                        },
                        new
                        {
                            Id = new Guid("c90759ff-a237-4eca-a762-2ca05abb65e7"),
                            Name = "Canceled"
                        },
                        new
                        {
                            Id = new Guid("15dfb474-b0a6-4445-b768-f8af3fd5825a"),
                            Name = "Sended"
                        });
                });

            modelBuilder.Entity("Entities.CartItem", b =>
                {
                    b.HasOne("Entities.Product", "Product")
                        .WithMany("cartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.CartItemOrder", b =>
                {
                    b.HasOne("Entities.CartItem", "CartItem")
                        .WithMany("CartItemOrders")
                        .HasForeignKey("CartItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Order", "Order")
                        .WithMany("CartItemOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Entities.Favourite", b =>
                {
                    b.HasOne("Entities.Product", "Product")
                        .WithMany("Favourites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Order", b =>
                {
                    b.HasOne("Entities.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Entities.Product", b =>
                {
                    b.HasOne("Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Entities.ProductAsset", b =>
                {
                    b.HasOne("Entities.Asset", "Asset")
                        .WithMany("ProductAssets")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Product", "Product")
                        .WithMany("ProductAssets")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Review", b =>
                {
                    b.HasOne("Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entities.Asset", b =>
                {
                    b.Navigation("ProductAssets");
                });

            modelBuilder.Entity("Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entities.CartItem", b =>
                {
                    b.Navigation("CartItemOrders");
                });

            modelBuilder.Entity("Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Entities.Order", b =>
                {
                    b.Navigation("CartItemOrders");
                });

            modelBuilder.Entity("Entities.Product", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("ProductAssets");

                    b.Navigation("Reviews");

                    b.Navigation("cartItems");
                });

            modelBuilder.Entity("Entities.Status", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
