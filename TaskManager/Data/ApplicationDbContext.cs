using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext :IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<TaskModel> TaskModel { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comments> Comments { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Определение связи между TaskModel и User
            modelBuilder.Entity<TaskModel>()
                .HasOne(t => t.Author)
                .WithMany() // Если у пользователя может быть много задач
                .HasForeignKey(t => t.AuthorId); // Идентификатор автора


        }
    }
}

