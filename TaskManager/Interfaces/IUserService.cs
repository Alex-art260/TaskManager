using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface IUserService
    {
         Task<User> GetCurrentUser();
    }
}
