using System.Data;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;


namespace WebApplication1.Infrastructure
{
    public class DropCourseRepository : IDropCourseRepository
    {
        private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

        

        public List<CourseViewModel> GetCoursesByEmail(string email)
        {
            var courseViewModels = new List<CourseViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT CourseName, Username, Email, EnrollmentId FROM Enrollments WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courseViewModels.Add(new CourseViewModel
                    {
                        CourseName = reader["CourseName"].ToString()
                        // Add other properties if needed
                    });
                }
            }

            return courseViewModels;
        }

        public bool DeleteEnrollment(string courseName, string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Enrollments WHERE CourseName = @CourseName AND Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                cmd.Parameters.AddWithValue("@Email", email);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
