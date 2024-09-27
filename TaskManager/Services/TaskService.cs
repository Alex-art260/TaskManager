using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;
using TaskManager.ViewModel;

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

        public async Task<int> Create(TaskVM taskViewModel)
        {

            var newTask = new TaskModel
            {
                Name = taskViewModel.Task.Name,
                Description = taskViewModel.Task.Description,
                CreatedDate = DateTime.SpecifyKind(taskViewModel.Task.CreatedDate, DateTimeKind.Utc),
                FinishedDate = DateTime.SpecifyKind(taskViewModel.Task.FinishedDate, DateTimeKind.Utc),
                StatusTask = taskViewModel.Task.StatusTask,
                AuthorId = (await _userService.GetCurrentUser()).Id.ToString(),
                comments = new List<Comments>() // Инициализируем список комментариев
            };

            // 2. Добавление комментариев
            if (taskViewModel.Comments != null)
            {
                foreach (var comment in taskViewModel.Comments)
                {
                    var newComment = new Comments
                    {
                        CommentText = comment.CommentText,
                        CreatedDate = DateTime.UtcNow,
                        TaskId = newTask.Id,
                        AuthorId = (await _userService.GetCurrentUser()).Id.ToString()
                    };
                    newTask.comments.Add(newComment); 
                }
            }

            _dbContext.TaskModel.Add(newTask);
            await _dbContext.SaveChangesAsync();
            return newTask.Id;

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
            var task = await _dbContext.TaskModel
             .Include(t => t.comments)
             .Include(t =>t.Author)
             .FirstOrDefaultAsync(t => t.Id == id);

            if (task != null)
                return task;
            else
                return null;
        }

        public async Task<TaskModel> Update(int id, TaskVM model)
        {
            var task = await _dbContext.TaskModel.FindAsync(id);

            if (task == null)
            {
                throw new Exception("Задача не найдена.");
            }

            task.Name = model.Task.Name;
            task.Description = model.Task.Description;
            task.CreatedDate = DateTime.SpecifyKind(model.Task.CreatedDate, DateTimeKind.Utc);
            task.FinishedDate = DateTime.SpecifyKind(model.Task.FinishedDate, DateTimeKind.Utc);
            task.StatusTask = model.Task.StatusTask;
            task.comments = new List<Comments>();

            if (model.Comments != null)
            {
                foreach (var comment in model.Comments)
                {
                    var newComment = new Comments
                    {
                        CommentText = comment.CommentText,
                        CreatedDate = DateTime.UtcNow,
                        TaskId = task.Id,
                        AuthorId = (await _userService.GetCurrentUser()).Id.ToString()
                    };
                    task.comments.Add(newComment);
                }
            }

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
