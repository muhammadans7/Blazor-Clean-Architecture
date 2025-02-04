using System.Data;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;

namespace WebApplication1.Infrastructure
{
    public class UserCoursesRepository : IUserCoursesRepository
    {
        private readonly string connectionString = "Server=HP;Database=Coursera;Trusted_Connection=True;"; // Replace with your actual connection string

        public List<Enrollment> GetCoursesByUserEmail(string email)
        {
            List<Enrollment> enrollments = new List<Enrollment>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Query to get all courses the user is enrolled in
                string query = @"
                    SELECT CourseName, Username, Email, EnrollmentId
                    FROM Enrollments
                    WHERE Email = @Email";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    enrollments.Add(new Enrollment
                    {
                        CourseName = reader["CourseName"].ToString(),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        EnrollmentId = (int)reader["EnrollmentId"]
                    });
                }
            }

            return enrollments;
        }
    }
}
