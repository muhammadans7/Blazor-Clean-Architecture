using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces
{
    public interface ICourseRepository
    {
        Course GetCourseById(int courseId); // Method to get course details by ID
        List<Course> GetAllCourses();       // Method to get all courses
    }
}
