using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
    }
}
