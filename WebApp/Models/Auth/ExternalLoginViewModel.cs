using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Auth
{
    public class ExternalLoginViewModel 
    {

        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }
    }
}
