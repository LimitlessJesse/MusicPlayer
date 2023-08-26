using Microsoft.AspNetCore.Mvc;
using MusicPlayer.Models.ViewModels;
using MusicPlayer.Models.Database;
using MusicPlayer.Models.DataModels;
using System.Diagnostics;

namespace MusicPlayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicPlayerDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(MusicPlayerDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IQueryable<User> users = _db.Users;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}