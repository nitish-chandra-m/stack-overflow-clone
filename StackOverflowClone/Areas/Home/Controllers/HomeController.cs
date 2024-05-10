using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StackOverflowClone.Data;
using StackOverflowClone.Models;

namespace StackOverflowClone.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController(ILogger<HomeController> logger, ApplicationDbContext context) : Controller
    {

        public IActionResult Index()
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
