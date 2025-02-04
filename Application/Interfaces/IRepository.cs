using WebApplication1.Domain.Models;
namespace WebApplication1.Application.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
   
        IEnumerable<T> GetAll();
    }
}
