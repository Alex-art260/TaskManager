using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TaskManager.Models
{
    public class TaskModel
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }

        public StatusTask StatusTask { get; set; }

        public string? AuthorId{ get; set; }
        public User? Author { get; set; }

        public List<Comments>? comments { get; set; }

    }
}