using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Interfaces;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAll();

            if(tasks != null)
                return View(tasks);
            else
                return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask()
        {
            // Проверка на валидность данных
            var requestBody = await new StreamReader(Request.Body).ReadToEndAsync();

            // Десериализация данных из JSON в объект модели
            var taskData = JsonConvert.DeserializeObject<TaskModel>(requestBody);

            // Проверка на валидность данных
            if (ModelState.IsValid && taskData != null)
            {
                await _taskService.Create(taskData);
                return Ok(new { success = true, message = "Задача успешно создана" });
            }
            return BadRequest("Ошибка при сохранении задачи.");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return Ok(); 
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TaskModel model)
        {
            if (ModelState.IsValid)
            {
                var updatedTask = await _taskService.Update(id, model);

                return View(updatedTask);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskById(int id)
        {
           var task = await _taskService.GetTaskById(id);

            return View("Update", task);
        }
    }
}
