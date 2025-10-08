using Inai.Api.Services;
using Inai.Core.Models;

namespace Inai.Api.Endpoints;

public static class TaskEndpoints
{
    public static void MapTasks(this WebApplication app)
    {
        app.MapGet("/tasks/{userId:guid}", async (Guid userId, TaskService service) =>
            Results.Ok(await service.GetTasksAsync(userId)));

        app.MapGet("/task/{id:guid}", async (Guid id, TaskService service) =>
            await service.GetTaskAsync(id) is TaskItem t ? Results.Ok(t) : Results.NotFound());

        app.MapPost("/task", async (TaskItem task, TaskService service) =>
        {
            var created = await service.AddTaskAsync(task);
            return Results.Created($"/task/{created.Id}", created);
        });

        app.MapPut("/task/{id:guid}", async (Guid id, TaskItem updated, TaskService service) =>
            await service.UpdateTaskAsync(id, updated) is TaskItem t ? Results.Ok(t) : Results.NotFound());

        app.MapDelete("/task/{id:guid}", async (Guid id, TaskService service) =>
            await service.DeleteTaskAsync(id) ? Results.Ok() : Results.NotFound());
    }
}
