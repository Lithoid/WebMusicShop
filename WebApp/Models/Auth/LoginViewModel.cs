using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName can't be empty", AllowEmptyStrings = false)]
        //[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
