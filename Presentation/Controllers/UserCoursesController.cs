
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;



namespace WebApplication1.Presentation.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly IUserCoursesRepository _userCoursesRepository;

        public UserCoursesController(IUserCoursesRepository userCoursesRepository)
        {
            _userCoursesRepository = userCoursesRepository;
        }

        // GET: UserCourses/CheckCourses
        [HttpGet]
        public IActionResult CheckCourses()
        {
            return View();
        }

        // POST: UserCourses/CheckCourses
        // [HttpPost]
        // public IActionResult CheckCourses(string email)
        // {
        //     // Retrieve the email from the session
        //     // var user = HttpContext.Session.Get("UserEmail");
        //     // Retrieve the byte array from the session
        //     byte[] userBytes = HttpContext.Session.Get("UserEmail");

        //     // Convert the byte array to a string
        //     string Email = System.Text.Encoding.UTF8.GetString(userBytes);


        //     if (string.IsNullOrEmpty(Email))
        //     {
        //         // If the session doesn't contain an email, redirect to the login page
        //         return RedirectToAction("Login", "Account");
        //     }

        //     // Fetch the courses for the logged-in user
        //     List<Enrollment> enrollments = _userCoursesRepository.GetCoursesByUserEmail(Email);

        //     // Pass the list of enrollments to the view
        //     return View(enrollments);
        // }

        [HttpPost]
        public IActionResult CheckCourses(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View();
            }

            List<Enrollment> enrollments = _userCoursesRepository.GetCoursesByUserEmail(email);

            // Pass the list of enrollments to the view
            return View(enrollments);
        }




        // public IActionResult CheckCourses(string email)
        // {
        //     // Retrieve the email from the session
        //     email = HttpContext.Session.GetString("UserEmail");

        //     if (string.IsNullOrEmpty(email))
        //     {
        //         ModelState.AddModelError("", "Email cannot be empty.");
        //         return View();
        //     }

        //     // Get the list of enrollments using the email
        //     List<Enrollment> enrollments = _userCoursesRepository.GetCoursesByUserEmail(email);

        //     // Pass the email and the list of enrollments to the view
        //     // ViewBag.UserEmail = email;
        //     return View(enrollments);
        // }

    }
}
