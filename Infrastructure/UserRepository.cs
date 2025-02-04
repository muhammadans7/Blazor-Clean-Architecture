using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Domain.Models;
using WebApplication1.Application.Interfaces;


namespace WebApplication1.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = "Server=HP;Database=coursera;Integrated Security=True;";
        }

        public User GetUserByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Username, Email, Password FROM Users WHERE Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Email = reader.GetString(2),
                        Password = reader.GetString(3) // Password should be handled securely
                    };
                }

                return null;
            }

        }    
        
        // Method to add a new user
        public void AddUser(User user)
        {
            string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                        
                    };
                }
            }
            return null;
        }

        public bool UserExists(string username, string email)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username OR Email = @Email";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public void Update(User user)
        {
            string query = "UPDATE Users SET Username = @Username, Email = @Email, Password = @Password WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Users WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User GetById(int id)
        {
            string query = "SELECT * FROM Users WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
            }
            return null;
        }

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }
            return users;
        }
    }
}
