using Inai.Api.Services;
using Inai.Core.Models;

namespace Inai.Api.Endpoints;

public static class ReminderEndpoints
{
    public static RouteGroupBuilder MapReminders(this RouteGroupBuilder group)
    {
        group.MapGet("/{taskId:guid}", async (ReminderService service, Guid taskId) =>
            Results.Ok(await service.GetRemindersAsync(taskId)));

        group.MapGet("/detail/{id:guid}", async (ReminderService service, Guid id) =>
            await service.GetReminderAsync(id) is Reminder r ? Results.Ok(r) : Results.NotFound());

        group.MapPost("/", async (ReminderService service, Reminder reminder) =>
        {
            var created = await service.CreateReminderAsync(reminder);
            return Results.Created($"/api/reminders/detail/{created.Id}", created);
        });

        group.MapPut("/{id:guid}", async (ReminderService service, Guid id, Reminder input) =>
            await service.UpdateReminderAsync(id, input) is Reminder r ? Results.Ok(r) : Results.NotFound());

        group.MapDelete("/{id:guid}", async (ReminderService service, Guid id) =>
            await service.DeleteReminderAsync(id) ? Results.NoContent() : Results.NotFound());

        return group;
    }
}
