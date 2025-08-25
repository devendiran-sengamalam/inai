using Inai.Api.Data;
using Inai.Api.Endpoints;
using Inai.Api.Services;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQLite
builder.Services.AddDbContext<InaiDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ReminderService>();


var app = builder.Build();

app.MapGet("/tasks/{userId:guid}", async (Guid userId, TaskService service) =>
{
    return Results.Ok(await service.GetTasksAsync(userId));
});

app.MapGet("/task/{id:guid}", async (Guid id, TaskService service) =>
{
    var task = await service.GetTaskAsync(id);
    return task is null ? Results.NotFound() : Results.Ok(task);
});

app.MapPost("/task", async (TaskItem task, TaskService service) =>
{
    var created = await service.AddTaskAsync(task);
    return Results.Created($"/task/{created.Id}", created);
});

app.MapPut("/task/{id:guid}", async (Guid id, TaskItem updated, TaskService service) =>
{
    var task = await service.UpdateTaskAsync(id, updated);
    return task is null ? Results.NotFound() : Results.Ok(task);
});

app.MapDelete("/task/{id:guid}", async (Guid id, TaskService service) =>
{
    return await service.DeleteTaskAsync(id) ? Results.Ok() : Results.NotFound();
});
app.MapGroup("/api/reminders")
    .MapReminders();

app.Run();
