using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Presentation.Controllers
{
    public class InstructorController : Controller
    {
        public IActionResult Index()
        {
            // Get list of instructors
            return View();
        }

        public IActionResult Details(int id)
        {
            // Get instructor details using the ID
            return View();
        }
    }
}
