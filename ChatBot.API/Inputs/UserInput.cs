using System.ComponentModel.DataAnnotations;

namespace ChatBot.API.Inputs
{
    public class UserInput
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
