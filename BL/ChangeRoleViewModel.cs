using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
namespace BL
{
    public class ChangeRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public List<AppRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<AppRole>();
            UserRoles = new List<string>();
        }
    }
}
