using Inai.Api.Data;
using Inai.Api.Endpoints;
using Inai.Api.Services;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InaiDbContext>(options =>
    options.UseSqlite("Data Source=inai.db"));

builder.Services.AddScoped<TaskService>();   
builder.Services.AddScoped<ReminderService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Inai API running!");

// ---------------- TASK ENDPOINTS ----------------

app.MapGet("/tasks/{userId:guid}", async (Guid userId, [FromServices] TaskService service) =>
    Results.Ok(await service.GetTasksAsync(userId)));

app.MapGet("/task/{id:guid}", async (Guid id, [FromServices] TaskService service) =>
    await service.GetTaskAsync(id) is TaskItem t ? Results.Ok(t) : Results.NotFound());

app.MapPost("/task", async ([FromBody] TaskItem task, [FromServices] TaskService service) =>
{
    var created = await service.AddTaskAsync(task);
    return Results.Created($"/task/{created.Id}", created);
});

app.MapPut("/task/{id:guid}", async (Guid id, [FromBody] TaskItem updated, [FromServices] TaskService service) =>
    await service.UpdateTaskAsync(id, updated) is TaskItem t ? Results.Ok(t) : Results.NotFound());

app.MapDelete("/task/{id:guid}", async (Guid id, [FromServices] TaskService service) =>
    await service.DeleteTaskAsync(id) ? Results.Ok() : Results.NotFound());

// ---------------- REMINDER ENDPOINTS ----------------
app.MapGroup("/api/reminders")
   .MapReminders();

app.Run();
