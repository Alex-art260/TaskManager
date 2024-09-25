using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserService _userService;

        public TaskService(ApplicationDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<int> Create(TaskModel model)
        {

            TaskModel task = new TaskModel()
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.SpecifyKind(model.CreatedDate, DateTimeKind.Utc),
                FinishedDate = DateTime.SpecifyKind(model.FinishedDate, DateTimeKind.Utc),
                Comment = model.Comment,
                Author = await _userService.GetCurrentUser(),
                StatusTask = model.StatusTask
            };

            _dbContext.TaskModel.Add(task);
            await _dbContext.SaveChangesAsync();
            return task.Id;
            
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            var task = await _dbContext.TaskModel.Include(x => x.Author).ToListAsync();

            if(task.Count > 0)
            {
                return task;
            }
            return null;
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            var task = await _dbContext.TaskModel.FindAsync(id);

            if (task != null)
                return task;
            else
                return null;
        }

        public async Task<TaskModel> Update(int id, TaskModel model)
        {
            var task = await _dbContext.TaskModel.FindAsync(id);

            if (task == null)
            {
                throw new Exception("Задача не найдена.");
            }

            task.Name = model.Name;
            task.Description = model.Description;
            task.CreatedDate = model.CreatedDate;
            task.FinishedDate = model.FinishedDate;
            task.Comment = model.Comment;
            task.StatusTask = model.StatusTask;

            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<int> Delete(int id)
        {
            var task = await _dbContext.TaskModel.FindAsync(id);

            if(task!=null)
            {
                _dbContext.TaskModel.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
            return id;
        }

    }
}
