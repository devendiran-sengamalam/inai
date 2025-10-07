using Inai.Core.Models;
using Inai.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class ReminderService
{
    private readonly InaiDbContext _db;

    public ReminderService(InaiDbContext db)
    {
        _db = db;
    }

    public async Task<List<Reminder>> GetRemindersAsync(Guid taskId)
    {
        return await _db.Reminders
            .Where(r => r.TaskItemId == taskId)
            .ToListAsync();
    }

    public async Task<Reminder?> GetReminderAsync(Guid id)
    {
        return await _db.Reminders.FindAsync(id);
    }

    public async Task<Reminder> CreateReminderAsync(Reminder reminder)
    {
        _db.Reminders.Add(reminder);
        await _db.SaveChangesAsync();
        return reminder;
    }

    public async Task<Reminder?> UpdateReminderAsync(Guid id, Reminder input)
    {
        var existing = await _db.Reminders.FindAsync(id);
        if (existing == null) return null;

        existing.RemindAt = input.RemindAt;
        existing.TaskItemId = input.TaskItemId;

        await _db.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteReminderAsync(Guid id)
    {
        var existing = await _db.Reminders.FindAsync(id);
        if (existing == null) return false;

        _db.Reminders.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
