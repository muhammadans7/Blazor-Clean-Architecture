namespace WebApplication1.Domain.Models
{
    public class Complaint
    {
        public int Id { get; set; } // Unique identifier for the complaint
        public string Name { get; set; } // User's name
        public string Email { get; set; } // User's email
        public string ComplaintText { get; set; } // Complaint text
        public DateTime CreatedAt { get; set; } // Date and time when the complaint was created
    }
}
