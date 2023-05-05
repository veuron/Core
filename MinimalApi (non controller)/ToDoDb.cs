using Microsoft.EntityFrameworkCore;

namespace ToDoApi
{
    //Контекст работы с БД
    public class ToDoDb : DbContext
    {
        public ToDoDb(DbContextOptions<ToDoDb> options) : base(options)
        {
        }
        public DbSet<ToDo> ToDos => Set<ToDo>();
    }

}
