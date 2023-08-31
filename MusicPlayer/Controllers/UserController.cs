using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models.DataModels;
using MusicPlayer.Models.ViewModels.User;
using System.Security.Claims;

namespace MusicPlayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            // To hold the value inputted if accidentally reload pages
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
                    /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Playlist");
                    }
                }
                loginViewModel.IsPasswordCorrect = false;
            }
            loginViewModel.IsUserExist = false;
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register() 
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if(user != null)
            {
                registerViewModel.IsEmailInUse = true;
                return View(registerViewModel);
            }
            
            var newUser = new User()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.UserName,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
            };

            var result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (!result.Succeeded)
            {
                registerViewModel.UserCreationFailed = true;
                return View(registerViewModel);
            }

            return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
