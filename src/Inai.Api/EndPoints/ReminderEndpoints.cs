using Inai.Api.Services;
using Inai.Core.Models;

namespace Inai.Api.Endpoints;

public static class ReminderEndpoints
{
    public static void MapReminders(this WebApplication app)
    {
        app.MapGet("/reminders/{taskId:guid}", async (Guid taskId, ReminderService service) =>
            Results.Ok(await service.GetRemindersAsync(taskId)));

        app.MapPost("/reminder", async (Reminder reminder, ReminderService service) =>
        {
            var created = await service.AddReminderAsync(reminder);
            return Results.Created($"/reminder/{created.Id}", created);
        });

        app.MapDelete("/reminder/{id:guid}", async (Guid id, ReminderService service) =>
            await service.DeleteReminderAsync(id) ? Results.Ok() : Results.NotFound());
    }
}
