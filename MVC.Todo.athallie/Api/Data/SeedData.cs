using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TodoContext(
                serviceProvider.GetRequiredService<DbContextOptions<TodoContext>>()
            );

            if (context.Todos.Any())
            {
                return;
            }

            context.Todos.AddRange(
                new Todo()
                {
                    Title = "Test1",
                    Description = "Test1",
                },
                new Todo()
                {
                    Title = "Test2",
                    Description = "Test2",
                    IsCompleted = true
                },
                new Todo()
                {
                    Title = "Test3",
                },
                new Todo()
                {
                    Title = "Test4",
                    Description = "Test4",
                },
                new Todo()
                {
                    Title = "Test5",
                    Description = "Test5",
                    IsCompleted = true
                }
            );

            context.SaveChanges();
        }
    }
}
