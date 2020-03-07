using System.Collections.Generic;
using System.Threading.Tasks;
using MyAppBack.Models;

namespace MyAppBack.Data
{
  public interface IUserRepository
  {
    void Add<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<bool> SaveAll();
    Task<User> GetUser(int id);
    Task<IEnumerable<User>> GetUsers();
  }
}