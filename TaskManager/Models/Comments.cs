namespace TaskManager.Models
{
    public class Comments
    {
        public int Id { get; set; }
        public string? CommentText { get; set; }
        public DateTime CreatedDate { get; set; }

        public int TaskId { get; set; }
        public TaskModel? Task { get; set; }

        public string? AuthorId { get; set; }
        public User? Author { get; set; }
    }
}
