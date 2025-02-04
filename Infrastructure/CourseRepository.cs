using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;

namespace WebApplication1.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString = "Server=HP;Database=Coursera;Trusted_Connection=True;";

        

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT CourseId, CourseName, Instructor FROM Courses";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    courses.Add(new Course
                    {
                        CourseId = (int)reader["CourseId"],
                        CourseName = reader["CourseName"].ToString(),
                        Instructor = reader["Instructor"].ToString()
                    });
                }
            }

            return courses;
        }

        public Course GetCourseById(int id)
        {
            Course course = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT CourseId, CourseName, Instructor FROM Courses WHERE CourseId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    course = new Course
                    {
                        CourseId = (int)reader["CourseId"],
                        CourseName = reader["CourseName"].ToString(),
                        Instructor = reader["Instructor"].ToString(),
                        // Assuming Description is already in the database
                    };
                }
            }

            return course;
        }
    }
}
