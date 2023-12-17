using Microsoft.EntityFrameworkCore;
using Todo.Model;

namespace Todo.Data
{

    public class TodoContext : DbContext
    {
        public DbSet<TodoModel> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");

        }

    }
}