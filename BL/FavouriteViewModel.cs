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
    public class FavouriteViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get;  set; }
        public Guid ProductId { get;  set; }


        public FavouriteViewModel()
        {
        }


        public FavouriteViewModel(Favourite favourite)
        {
            Id = favourite.Id;
            UserId = favourite.UserId;
            ProductId = favourite.ProductId;
         



        }

        public static implicit operator Favourite(FavouriteViewModel model)
        {
            return new Favourite
            {
                Id = model.Id,
                UserId = model.UserId,
                ProductId = model.ProductId,

               
            };
        }
    }
}