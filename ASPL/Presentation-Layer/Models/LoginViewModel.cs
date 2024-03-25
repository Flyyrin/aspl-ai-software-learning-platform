using System.ComponentModel.DataAnnotations;

namespace Presentation_Layer.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Incorrect Username or Password.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Incorrect Username or Password.")]
        public string Password { get; set; }
    }
}