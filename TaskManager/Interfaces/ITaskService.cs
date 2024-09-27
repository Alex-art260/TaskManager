using TaskManager.Models;
using TaskManager.ViewModel;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        Task<int> Create(TaskVM model);
        Task<IEnumerable<TaskModel>> GetAll();
        Task<int> Delete(int id);
        Task<TaskModel> GetTaskById(int id);
        Task<TaskModel> Update(int id, TaskVM model);
    }
}
