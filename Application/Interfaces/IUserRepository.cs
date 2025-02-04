using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByEmailAndPassword(string email, string password);

        User GetUserByEmail(string email);
        bool UserExists(string username, string email);
    }
}
