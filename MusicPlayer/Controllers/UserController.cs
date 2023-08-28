using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models.Database;
using MusicPlayer.Models.DataModels;
using MusicPlayer.Models.ViewModels;

namespace MusicPlayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MusicPlayerDbContext _db;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, MusicPlayerDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
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
                        // TODO: Change later when Listening Page is created
                        return RedirectToAction("Index", "Home");
                    }
                }
                loginViewModel.IsPasswordCorrect = false;
            }
            loginViewModel.IsUserExist = false;
            return View(loginViewModel);
        }
    }
}
