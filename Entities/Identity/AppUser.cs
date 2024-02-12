using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class AppUser : IdentityUser<Guid>
    {

        public DateTime RegisterDate { get; set; }
        public string FullName { get; set; }




    }
}