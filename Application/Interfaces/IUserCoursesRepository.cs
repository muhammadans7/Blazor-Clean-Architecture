using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces

{
    public interface IUserCoursesRepository
    {
        List<Enrollment> GetCoursesByUserEmail(string email);
    }
}
