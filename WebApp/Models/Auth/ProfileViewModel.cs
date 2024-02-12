using BL;
using Entities;
using System.Collections.Generic;

namespace WebApp.Models.Auth
{
    public class ProfileViewModel
    {
        public AppUser User { get; set; }
        public List<OrderViewModel> Orders { get; set; }

        public int ReviewCount { get; set; } = 0;
    }
}
