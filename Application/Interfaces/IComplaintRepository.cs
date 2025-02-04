using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces
{
    public interface IComplaintRepository
    {
        void AddComplaint(Complaint complaint); // Method to add a new complaint
        List<Complaint> GetAllComplaints(); // Method to get all complaints
        Complaint GetComplaintById(int id); // Method to get a complaint by its ID
    }
}
