using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Error Handling
        public bool IsPasswordCorrect { get; set; } = true;
        public bool IsUserExist { get; set; } = true;

        public const string IncorrectCredentialErrorMessage = "Credential is not correct, please try again!";
    }
}
