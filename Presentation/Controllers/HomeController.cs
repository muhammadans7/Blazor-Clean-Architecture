using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
namespace WebApplication1.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var email = HttpContext.Session.Get("UserEmail");
            ViewBag.email = email;
            return View();
        }

        public IActionResult About()
        {
             var email = HttpContext.Session.Get("UserEmail");
            ViewBag.email = email;
            return View();
        }

        public IActionResult Contact()
        {
             var email = HttpContext.Session.Get("UserEmail");
            ViewBag.email = email;
            return View();
        }

        public IActionResult Privacy()
        {
             var email = HttpContext.Session.Get("UserEmail");
             ViewBag.email = email;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
