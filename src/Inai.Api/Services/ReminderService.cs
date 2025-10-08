using Inai.Api.Data;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class ReminderService
{
    private readonly InaiDbContext _db;
    public ReminderService(InaiDbContext db) => _db = db;

    public async Task<IEnumerable<Reminder>> GetRemindersAsync(Guid taskId) =>
        await _db.Reminders.Where(r => r.TaskItemId == taskId).ToListAsync();

    public async Task<Reminder> AddReminderAsync(Reminder reminder)
    {
        _db.Reminders.Add(reminder);
        await _db.SaveChangesAsync();
        return reminder;
    }

    public async Task<bool> DeleteReminderAsync(Guid id)
    {
        var reminder = await _db.Reminders.FindAsync(id);
        if (reminder == null) return false;
        _db.Reminders.Remove(reminder);
        await _db.SaveChangesAsync();
        return true;
    }
}
