using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
using System.Collections.Generic;

namespace WebApplication1.Presentation.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: Courses
        public IActionResult Index()
        {
            var courses = _courseRepository.GetAllCourses();
            return View(courses);
        }

        // GET: Courses/Details/{id}
        public IActionResult Details(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/FetchCourses
        [HttpGet]
        public IActionResult FetchCourses()
        {
            var courses = _courseRepository.GetAllCourses(); // Adjust this to get the courses you want to return
            return PartialView("_CourseListPartial", courses);
        }
    }
}


// using Microsoft.AspNetCore.Mvc;
// using CourseraClone.Models;
// using CourseraClone.Repositories.Interfaces;
// using System.Collections.Generic;

// namespace CourseraClone.Controllers
// {
//     public class CoursesController : Controller
//     {
//         private readonly ICourseRepository _courseRepository;

//         public CoursesController(ICourseRepository courseRepository)
//         {
//             _courseRepository = courseRepository;
//         }

//         // GET: Courses
//         public IActionResult Index()
//         {
//             var courses = _courseRepository.GetAllCourses();
//             return View(courses);
//         }

//         // GET: Courses/Details/{id}
//         public IActionResult Details(int id)
//         {
//             var course = _courseRepository.GetCourseById(id);
//             if (course == null)
//             {
//                 return NotFound();
//             }

//             return View(course);
//         }
//     }
// }
