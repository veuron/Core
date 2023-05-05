using Microsoft.EntityFrameworkCore;

namespace ToDoApi_Controller_.Models
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
        {
        }

        public DbSet<ToDoItem> TodoItems { get; set; } = null!;
    }
}
