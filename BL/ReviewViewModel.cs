using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public string Text { get;  set; }
        public decimal Rate { get;  set; }
        public int Likes { get; set; }
        public string Name { get; set; }
        public string UserName { get;  set; }
        public DateTime Date { get; set; }
        public Guid UserId { get;  set; }
        public Guid ProductId { get;  set; }

        public ReviewViewModel()
        {
        }
        public bool IsEmpty
        {
            get => Name == null;
        }


        public ReviewViewModel(Review review)
        {
            Id = review.Id;
            Text = review.Text;
            Rate = review.Rate;
            Date = review.Date;
            Likes = review.Likes;
            UserId = review.UserId;
            ProductId = review.ProductId;
            UserName = review.UserName;
        }

        public static implicit operator Review(ReviewViewModel model)
        {
            return new Review
            {
                Id = model.Id,
                Text = model.Text,
                Rate = model.Rate,
                UserId = model.UserId,
                ProductId = model.ProductId,
                Likes = model.Likes,
                Date = model.Date,
                UserName = model.UserName,
            };
        }
    }
}
