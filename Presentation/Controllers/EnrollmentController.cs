// using Microsoft.AspNetCore.Mvc;
// using CourseraClone.Models;
// using CourseraClone.Repositories;

// namespace CourseraClone.Controllers
// {

//     public class EnrollmentController : Controller
//     {
//         private readonly IEnrollmentRepository _enrollmentRepository;

//         // Constructor with Dependency Injection
//         public EnrollmentController(IEnrollmentRepository enrollmentRepository)
//         {
//             _enrollmentRepository = enrollmentRepository;
//         }

//         // GET: Enrollment/Enroll
//         public IActionResult Enroll(string courseName)
//         {
//             var enrollment = new Enrollment
//             {
//                 CourseName = courseName
//             };
//             return View(enrollment);
//         }

//         // POST: Enrollment/Enroll
//         [HttpPost]
//         public IActionResult Enroll(Enrollment enrollment)
//         {
//             if (!_enrollmentRepository.CheckCourseExists(enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "Course not found.");
//                 return View(enrollment);
//             }

//             int enrollmentId = _enrollmentRepository.GetOrCreateEnrollmentId(enrollment.Email);

//             if (_enrollmentRepository.CheckEnrollmentExists(enrollment.Email, enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "You are already enrolled in this course.");
//                 return View(enrollment);
//             }

//             enrollment.EnrollmentId = enrollmentId;
//             _enrollmentRepository.AddEnrollment(enrollment);

//             return RedirectToAction("EnrollmentSuccess");
//         }

//         // GET: Enrollment/EnrollmentSuccess
//         public IActionResult EnrollmentSuccess()
//         {
//             return View();
//         }
//     }
// }
// using Microsoft.AspNetCore.Mvc;
// using CourseraClone.Models;
// using CourseraClone.Repositories;
// using System.Data.SqlClient;
// using System.Collections.Generic;

// namespace CourseraClone.Controllers
// {
//     public class EnrollmentController : Controller
//     {
//         private readonly IEnrollmentRepository _enrollmentRepository;
//         private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

//         // Constructor with Dependency Injection
//         public EnrollmentController(IEnrollmentRepository enrollmentRepository)
//         {
//             _enrollmentRepository = enrollmentRepository;
//         }

//         // GET: Enrollment/Enroll
//         public IActionResult Enroll(string courseName)
//         {
//             // Fetch the list of courses from the database
//             List<Course> courses = new List<Course>();
//             using (SqlConnection conn = new SqlConnection(_connectionString))
//             {
//                 conn.Open();
//                 string query = "SELECT CourseName FROM Courses";
//                 SqlCommand cmd = new SqlCommand(query, conn);
//                 SqlDataReader reader = cmd.ExecuteReader();
//                 while (reader.Read())
//                 {
//                     courses.Add(new Course
//                     {
//                         CourseName = reader["CourseName"].ToString()
//                     });
//                 }
//             }

//             // Pass the list of courses to the view using ViewBag
//             ViewBag.Courses = courses;

//             // Initialize the Enrollment model with the selected course name (if any)
//             var enrollment = new Enrollment
//             {
//                 CourseName = courseName
//             };

//             return View(enrollment);
//         }

//         // POST: Enrollment/Enroll
//         [HttpPost]
//         public IActionResult Enroll(Enrollment enrollment)
//         {
//             if (!_enrollmentRepository.CheckCourseExists(enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "Course not found.");
//                 return View(enrollment);
//             }

//             int enrollmentId = _enrollmentRepository.GetOrCreateEnrollmentId(enrollment.Email);

//             if (_enrollmentRepository.CheckEnrollmentExists(enrollment.Email, enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "You are already enrolled in this course.");
//                 return View(enrollment);
//             }

//             enrollment.EnrollmentId = enrollmentId;
//             _enrollmentRepository.AddEnrollment(enrollment);

//             return RedirectToAction("EnrollmentSuccess");
//         }

//         // GET: Enrollment/EnrollmentSuccess
//         public IActionResult EnrollmentSuccess()
//         {
//             return View();
//         }
//     }
// }
// using Microsoft.AspNetCore.Mvc;



// using CourseraClone.Models;
// using CourseraClone.Repositories;
// using System.Data.SqlClient;
// using System.Collections.Generic;

// namespace CourseraClone.Controllers
// {
//     public class EnrollmentController : Controller
//     {
//         private readonly IEnrollmentRepository _enrollmentRepository;
//         private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

//         // Constructor with Dependency Injection
//         public EnrollmentController(IEnrollmentRepository enrollmentRepository)
//         {
//             _enrollmentRepository = enrollmentRepository;
//         }

//         // GET: Enrollment/Enroll
//         public IActionResult Enroll(string courseName)
//         {
//             // Fetch the list of courses from the database
//             List<Course> courses = new List<Course>();
//             using (SqlConnection conn = new SqlConnection(_connectionString))
//             {
//                 conn.Open();
//                 string query = "SELECT CourseName FROM Courses";
//                 SqlCommand cmd = new SqlCommand(query, conn);
//                 SqlDataReader reader = cmd.ExecuteReader();
//                 while (reader.Read())
//                 {
//                     courses.Add(new Course
//                     {
//                         CourseName = reader["CourseName"].ToString()
//                     });
//                 }
//             }

//             // Pass the list of courses to the view using ViewBag
//             ViewBag.Courses = courses;

//             // Initialize the Enrollment model with the selected course name (if any)
//             var enrollment = new Enrollment
//             {
//                 CourseName = courseName
//             };

//             return View(enrollment);
//         }

//         // POST: Enrollment/Enroll
//         [HttpPost]
//         public IActionResult Enroll(Enrollment enrollment)
//         {
//             // Check if the course exists
//             if (!_enrollmentRepository.CheckCourseExists(enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "Course not found.");
//                 // Reload the courses list for the view
//                 ReloadCourses();
//                 return View(enrollment);
//             }

//             // Get or create the enrollment ID
//             int enrollmentId = _enrollmentRepository.GetOrCreateEnrollmentId(enrollment.Email);

//             // Check if the user is already enrolled in the course
//             if (_enrollmentRepository.CheckEnrollmentExists(enrollment.Email, enrollment.CourseName))
//             {
//                 ModelState.AddModelError("", "You are already enrolled in this course.");
//                 // Reload the courses list for the view
//                 ReloadCourses();
//                 return View(enrollment);
//             }

//             // Proceed with enrollment
//             enrollment.EnrollmentId = enrollmentId;
//             _enrollmentRepository.AddEnrollment(enrollment);

//             return RedirectToAction("EnrollmentSuccess");
//         }

//         // GET: Enrollment/EnrollmentSuccess
//         public IActionResult EnrollmentSuccess()
//         {
//             return View();
//         }

//         // Helper method to reload the courses into ViewBag
//         private void ReloadCourses()
//         {
//             List<Course> courses = new List<Course>();
//             using (SqlConnection conn = new SqlConnection(_connectionString))
//             {
//                 conn.Open();
//                 string query = "SELECT CourseName FROM Courses";
//                 SqlCommand cmd = new SqlCommand(query, conn);
//                 SqlDataReader reader = cmd.ExecuteReader();
//                 while (reader.Read())
//                 {
//                     courses.Add(new Course
//                     {
//                         CourseName = reader["CourseName"].ToString()
//                     });
//                 }
//             }
//             ViewBag.Courses = courses;
//         }
//     }
// }
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace WebApplication1.Presentation.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

        // Constructor with Dependency Injection
        public EnrollmentController(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        // GET: Enrollment/Enroll
        public IActionResult Enroll(string courseName,[FromQuery] string email)
        {
             var user = HttpContext.Session.Get("UserEmail");
            ViewBag.email = user;
            // Fetch the list of courses from the database
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT CourseName FROM Courses";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseName = reader["CourseName"].ToString()
                    });
                }
            }

            // Pass the list of courses to the view using ViewBag
            ViewBag.Courses = courses;

            // Initialize the Enrollment model with the selected course name (if any)
            var enrollment = new Enrollment
            {
                CourseName = courseName
            };
            ViewBag.email = email;
            return View(enrollment);
        }

        // POST: Enrollment/Enroll
        [HttpPost]
        public IActionResult Enroll(Enrollment enrollment)
        {
            // Check if the course exists
            if (!_enrollmentRepository.CheckCourseExists(enrollment.CourseName))
            {
                ModelState.AddModelError("", "Course not found.");
                // Reload the courses list for the view
                ReloadCourses();
                return View(enrollment);
            }

            // Get or create the enrollment ID
            int enrollmentId = _enrollmentRepository.GetOrCreateEnrollmentId(enrollment.Email);

            // Check if the user is already enrolled in the course
            if (_enrollmentRepository.CheckEnrollmentExists(enrollment.Email, enrollment.CourseName))
            {
                ModelState.AddModelError("", "You are already enrolled in this course.");
                // Reload the courses list for the view
                ReloadCourses();
                return View(enrollment);
            }

            // Proceed with enrollment
            enrollment.EnrollmentId = enrollmentId;
            _enrollmentRepository.AddEnrollment(enrollment);

            // Store enrollment details in session
            HttpContext.Session.SetString("UserEmail", enrollment.Email);
            HttpContext.Session.SetString("EnrolledCourse", enrollment.CourseName);

            return RedirectToAction("EnrollmentSuccess");
        }

        // GET: Enrollment/EnrollmentSuccess
        public IActionResult EnrollmentSuccess()
        {
            return View();
        }

        // Helper method to reload the courses into ViewBag
        private void ReloadCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT CourseName FROM Courses";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseName = reader["CourseName"].ToString()
                    });
                }
            }
            ViewBag.Courses = courses;
        }
    }
}
