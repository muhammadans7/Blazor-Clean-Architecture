using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces


{
    public interface IDropCourseRepository
    {
        List<CourseViewModel> GetCoursesByEmail(string email);
        bool DeleteEnrollment(string courseName, string email);
    }
}
