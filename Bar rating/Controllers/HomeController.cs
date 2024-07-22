using Bar_rating.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Bar_rating.Models;

namespace Bar_rating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.role = RoleSupplier.role;
            ViewBag.userId = UserIdSupplier.id;
            BarRatingDBContext context= new BarRatingDBContext();
            ViewBag.UserCount = context.Users.Count();
            ViewBag.BarCount = context.Bars.Count();
            ViewBag.ReviewCount = context.Reviews.Count();

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

        public IActionResult Logout()
        {
            RoleSupplier.role = "Empty";
            return View();
        }
    }
}