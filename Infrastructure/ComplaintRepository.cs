using System.Data;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;
namespace WebApplication1.Infrastructure
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly string _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";

        // Method to add a new complaint to the database
        public void AddComplaint(Complaint complaint)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Complaints (Name, Email, ComplaintText) VALUES (@Name, @Email, @ComplaintText)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", complaint.Name);
                    cmd.Parameters.AddWithValue("@Email", complaint.Email);
                    cmd.Parameters.AddWithValue("@ComplaintText", complaint.ComplaintText);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Method to get all complaints from the database
        public List<Complaint> GetAllComplaints()
        {
            List<Complaint> complaints = new List<Complaint>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Complaints";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        complaints.Add(new Complaint
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            ComplaintText = reader["ComplaintText"].ToString()
                        });
                    }
                }
            }
            return complaints;
        }

        // Method to get a complaint by its ID
        public Complaint GetComplaintById(int id)
        {
            Complaint complaint = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Complaints WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        complaint = new Complaint
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            ComplaintText = reader["ComplaintText"].ToString()
                        };
                    }
                }
            }
            return complaint;
        }
    }
}
