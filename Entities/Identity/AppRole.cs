using System;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole(string name):base(name)
        {

        }
    }
}