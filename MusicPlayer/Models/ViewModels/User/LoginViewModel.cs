using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.User
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Error Handling
        public bool IsPasswordCorrect { get; set; } = true;
        public bool IsUserExist { get; set; } = true;

        public const string IncorrectCredentialErrorMessage = "Wrong credentials , please try again!";
    }
}
