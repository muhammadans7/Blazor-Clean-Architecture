using System.Collections.Generic;

namespace WebApplication1.Domain.Models
{
    public class DropCourseViewModel
    {
        public string Email { get; set; }
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }

    public class CourseViewModel
    {
        public string CourseName { get; set; }
    }
}
