using System.ComponentModel.DataAnnotations;

namespace MusicPlayer.Models.ViewModels.UserViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$", 
            ErrorMessage = "Your password must contain at least six characters, a capital letter, a lowercase letter, a number, and a special character.")]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        // Error Handling
        public bool IsEmailInUse { get; set; } = false;
        public bool UserCreationFailed { get; set; } = false;

        public const string EmailInUseErrorMessage = "This email address is already in use";
        public const string UserCreationFailedErrorMessage = "Fail to create this user account";
    }
}
