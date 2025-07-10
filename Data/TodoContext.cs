using Microsoft.EntityFrameworkCore;
using TaskBell.Models;

namespace TaskBell.Data
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TaskBell.Models.ToDoItem>().HasKey(t => t.Id);
            //modelBuilder.Entity<TaskBell.Models.ToDoItem>().Property(t => t.Title).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
            });

            // Additional configurations can be added here
        }


    }
}
