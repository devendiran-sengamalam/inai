using Inai.Api.Data;
using Inai.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class TaskService
{
    private readonly InaiDbContext _db;

    public TaskService(InaiDbContext db)
    {
        _db = db;
    }

    public async Task<List<TaskItem>> GetTasksAsync(Guid userId) =>
        await _db.Tasks.Where(t => t.UserId == userId)
                       .Include(t => t.Reminders)
                       .ToListAsync();

    public async Task<TaskItem?> GetTaskAsync(Guid id) =>
        await _db.Tasks.Include(t => t.Reminders).FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem updated)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null) return null;

        task.Title = updated.Title;
        task.Description = updated.Description;
        task.IsCompleted = updated.IsCompleted;
        task.DueDate = updated.DueDate;

        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var task = await _db.Tasks.FindAsync(id);
        if (task == null) return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
        return true;
    }
}
