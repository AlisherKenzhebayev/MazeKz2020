using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMaze.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter login")]
        [MinLength(2, ErrorMessage = "Login must be >= 2")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [MinLength(3, ErrorMessage = "Password must be >= 3")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsPersistent { get; set; }
    }
}