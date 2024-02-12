using Microsoft.Build.Framework;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Auth
{
    public class ResetPasswordViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Not matching")]
        public string ConfirmPassword { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Code { get; set; }


    }
}
