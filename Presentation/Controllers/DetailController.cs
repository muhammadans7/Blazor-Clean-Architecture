using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;

namespace WebApplication1.Presentation.Controllers
{
    public class DetailController : Controller
    {
        private readonly string _connectionString = "Server=HP;Database=coursera;Trusted_Connection=True;";

        // Action to display course details
        public IActionResult Details(int courseId)
        {
            Course course = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Courses WHERE CourseId = @CourseId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseId", courseId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            course = new Course
                            {
                                CourseId = (int)reader["CourseId"],
                              
                                Instructor = reader["Instructor"].ToString()
                               
                            };
                        }
                    }
                }
            }

            return View(course);
        }

        // Action to handle course enrollment
        [HttpPost]
        public IActionResult Enroll(int courseId)
        {
            var userId = User.Identity?.Name; // Assuming user ID is stored in claims
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Check if already enrolled
                string checkQuery = "SELECT COUNT(*) FROM Enrollments WHERE CourseId = @CourseId AND UserId = @UserId";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@CourseId", courseId);
                    checkCommand.Parameters.AddWithValue("@UserId", userId);

                    int count = (int)checkCommand.ExecuteScalar();
                    if (count > 0)
                    {
                        // User is already enrolled
                        return RedirectToAction("AlreadyEnrolled", new { courseId });
                    }
                }

                // Enroll the user
                string enrollQuery = "INSERT INTO Enrollments (CourseId, UserId, EnrollmentDate) VALUES (@CourseId, @UserId, @EnrollmentDate)";
                using (SqlCommand enrollCommand = new SqlCommand(enrollQuery, connection))
                {
                    enrollCommand.Parameters.AddWithValue("@CourseId", courseId);
                    enrollCommand.Parameters.AddWithValue("@UserId", userId);
                    enrollCommand.Parameters.AddWithValue("@EnrollmentDate", DateTime.UtcNow);

                    enrollCommand.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", "Home"); // Redirect to homepage after enrollment
        }

        // Action to handle the case when user is already enrolled
        public IActionResult AlreadyEnrolled(int courseId)
        {
            ViewBag.Message = "You are already enrolled in this course.";
            return RedirectToAction("Index", "Home"); // Redirect to homepage after displaying message
        }
    }
}
