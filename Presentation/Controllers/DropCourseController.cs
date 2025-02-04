using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
using System.Collections.Generic;

namespace WebApplication1.Presentation.Controllers
{
    public class DropCourseController : Controller
    {
        private readonly IDropCourseRepository _dropCourseRepository;

        // Inject the repository through the constructor
        public DropCourseController(IDropCourseRepository dropCourseRepository)
        {
            _dropCourseRepository = dropCourseRepository;
        }

        // GET: DropCourse/Index
        public IActionResult Index()
        {
            var model = new DropCourseViewModel
            {
                Courses = new List<CourseViewModel>() // Initialize with an empty list
            };
            return View(model);
        }

        // POST: DropCourse/Index
        [HttpPost]
        public IActionResult Index(DropCourseViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("", "Email cannot be empty.");
                return View(model);
            }

            model.Courses = _dropCourseRepository.GetCoursesByEmail(model.Email);

            return View(model);
        }

        // POST: DropCourse/DeleteCourse
        [HttpPost]
        public IActionResult DeleteCourse(string courseName, string email)
        {
            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Invalid course or email.");
                return RedirectToAction("Index");
            }

            bool isDeleted = _dropCourseRepository.DeleteEnrollment(courseName, email);

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Course successfully deleted.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete course.";
            }

            return RedirectToAction("Index");
        }
    }
}
