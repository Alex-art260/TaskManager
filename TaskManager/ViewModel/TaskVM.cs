using TaskManager.Models;

namespace TaskManager.ViewModel
{
    public class TaskVM
    {
        public TaskModel? Task { get; set; }
        public List<Comments>? Comments { get; set; }
    }
}
