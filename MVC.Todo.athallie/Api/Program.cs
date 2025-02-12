using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
SeedData.Initialize(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//GET:/
app.MapGet(
    "/",
    async (TodoContext db) => await db.Todos.ToListAsync()
);

//GET:/id
app.MapGet(
    "/{id}",
    async (TodoContext db, int id) =>
        await db.Todos.FindAsync(id) 
            is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound()
);

//POST: /
app.MapPost(
    "/",
    async (TodoContext db, Todo todo) =>
    {
        await db.Todos.AddAsync(todo);
        await db.SaveChangesAsync();
        return Results.Created($"/{todo.id}", todo);
    }
);

//PUT: /id
app.MapPut(
    "/{id}",
    async (TodoContext db, int id, Todo inputTodo) =>
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo == null)
        {
            return Results.NotFound();
        }
        todo.Title = inputTodo.Title;
        todo.Description = inputTodo.Description;
        todo.IsCompleted = inputTodo.IsCompleted;

        await db.SaveChangesAsync();

        return Results.NoContent();
    }
);

//DELETE: /id
app.MapDelete(
    "/{id}",
    async (TodoContext db, int id) =>
    {
        if (await db.Todos.FindAsync(id) is Todo todo)
        {
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return Results.NoContent();

        }

        return Results.NotFound();
    }
);

app.Run();
