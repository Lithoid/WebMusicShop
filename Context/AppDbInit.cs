using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    public static class AppDbInitializer
    {
        public static void SeedUsers(UserManager<AppUser> userManager,RoleManager<AppRole> roleManger)
        {
            if (roleManger.FindByNameAsync("Admin").Result == null)
            {
                AppRole role = new AppRole("Admin");

                IdentityResult result =  roleManger.CreateAsync(role).Result;
                
            }
            if (roleManger.FindByNameAsync("Member").Result == null)
            {
                AppRole role = new AppRole("Member");

                IdentityResult result = roleManger.CreateAsync(role).Result;

            }


            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                AppUser user = new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    RegisterDate= DateTime.Now,
                    FullName = "Eugene Holovetskiy"
                    
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwerty123*").Result;

                if (result.Succeeded)
                {

                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("member@gmail.com").Result == null)
            {
                AppUser user = new AppUser 
                {
                    UserName = "member@gmail.com",
                    Email = "member@gmail.com",
                    EmailConfirmed = true,
                    RegisterDate = DateTime.Now,
                    FullName = "Eugene Holovetskiy"
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwerty123*").Result;

                if (result.Succeeded)
                {

                    userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }
        }

    }
}
