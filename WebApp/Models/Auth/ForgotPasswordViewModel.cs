using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Auth
{
    public class ForgotPasswordViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
