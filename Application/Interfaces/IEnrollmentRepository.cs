using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces
{
    public interface IEnrollmentRepository
    {
        bool CheckCourseExists(string courseName);
        bool CheckEnrollmentExists(string email, string courseName);
        int GetOrCreateEnrollmentId(string email);
        void AddEnrollment(Enrollment enrollment);
    }
}
