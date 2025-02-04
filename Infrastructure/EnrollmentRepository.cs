using System.Data;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;


namespace CourseraClone.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        
        
        private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

        public bool CheckCourseExists(string courseName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Courses WHERE CourseName = @CourseName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool CheckEnrollmentExists(string email, string courseName)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Enrollments WHERE Email = @Email AND CourseName = @CourseName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@CourseName", courseName);
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public int GetOrCreateEnrollmentId(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    IF EXISTS (SELECT 1 FROM Enrollments WHERE Email = @Email)
                    BEGIN
                        SELECT EnrollmentId FROM Enrollments WHERE Email = @Email
                    END
                    ELSE
                    BEGIN
                        -- Generate a new EnrollmentId
                        SELECT ISNULL(MAX(EnrollmentId), 0) + 1 FROM Enrollments
                    END";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                return (int)cmd.ExecuteScalar();
            }
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO Enrollments (EnrollmentId, CourseName, Username, Email)
                    VALUES (@EnrollmentId, @CourseName, @Username, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EnrollmentId", enrollment.EnrollmentId);
                cmd.Parameters.AddWithValue("@CourseName", enrollment.CourseName);
                cmd.Parameters.AddWithValue("@Username", enrollment.Username);
                cmd.Parameters.AddWithValue("@Email", enrollment.Email);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
