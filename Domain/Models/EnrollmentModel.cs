namespace WebApplication1.Domain.Models
{
    public class Enrollment
    {
        public string CourseName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int EnrollmentId { get; set; }
        
        // Keep this for consistency but it's not used in the key
    }

}
