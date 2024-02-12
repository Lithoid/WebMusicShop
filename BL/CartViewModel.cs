using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;



namespace BL
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductAbout { get; set; }


        public CartViewModel()
        {
        }

       

        public CartViewModel(CartItem cartItem)
        {
            Id = cartItem.Id;
            CartId = cartItem.CartId;
            Quantity = cartItem.Quantity;
            DateCreated = cartItem.DateCreated;
            ProductName = cartItem.Product?.Name;     
            ProductId = cartItem.ProductId;
            Price = cartItem.Product.RetailPrice;
            ProductAbout =new string(cartItem.Product.About.Take(20).ToArray());


        }

        public static implicit operator CartItem(CartViewModel model)
        {
            return new CartItem
            {
                Id = model.Id,
                CartId = model.CartId,
                Quantity = model.Quantity,
                DateCreated = model.DateCreated,
                ProductId = model.ProductId

            };
        }

        public static CartViewModel GetCartItemById(Guid id,ICartItemRepository repository)
        {
            return (repository.AllItems as DbSet<CartItem>)
                .Where(p => p.Id == id)
                .Include(p => p.Product)
                .Select(p => new CartViewModel(p))
                .FirstOrDefault();
        }

        public static CartViewModel GetCartItemByProdId(Guid ProdId, ICartItemRepository repository)
        {
            return (repository.AllItems as DbSet<CartItem>)
                .Where(p => p.ProductId == ProdId)
                .Include(p => p.Product)
                .Select(p => new CartViewModel(p))
                .FirstOrDefault();
        }


        public static async Task<bool> Delete(Guid id, ICartItemRepository repository)
        {
            return await repository.DeleteItemAsync(id);
        }

        public static async Task<bool> Edit(CartViewModel model, ICartItemRepository repository)
        {
            return await repository.ChangeItemAsync(model);
        }

        public static IQueryable<CartViewModel> GetCartItemList(ICartItemRepository repository, Guid? cartId = null)
        {
            if (cartId.HasValue)
            {
                return repository.AllItems
                    .Where(p => p.CartId == cartId)
                    .Include(p => p.Product)
                    .Select(p => new CartViewModel(p));
            }
            return (repository.AllItems as DbSet<CartItem>)
                .Include(p => p.Product)
                .Select(p => new CartViewModel(p));
        }







    }
}
