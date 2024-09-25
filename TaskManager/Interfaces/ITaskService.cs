using TaskManager.Models;

namespace TaskManager.Interfaces
{
    public interface ITaskService
    {
        Task<int> Create(TaskModel model);
        Task<IEnumerable<TaskModel>> GetAll();
        Task<int> Delete(int id);
        Task<TaskModel> GetTaskById(int id);
        Task<TaskModel> Update(int id, TaskModel model);
    }
}
