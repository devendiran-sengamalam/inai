using Inai.Core.Models;
using Inai.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Inai.Api.Services;

public class TaskService
{
    private readonly InaiDbContext _db;

    public TaskService(InaiDbContext db)
    {
        _db = db;
    }

    // Get all tasks for a user
    public async Task<List<TaskItem>> GetTasksAsync(Guid userId)
    {
        return await _db.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    // Get single task by ID
    public async Task<TaskItem?> GetTaskAsync(Guid id)
    {
        return await _db.Tasks.FindAsync(id);
    }

    // Add new task
    public async Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    // Update task
    public async Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem input)
    {
        var existing = await _db.Tasks.FindAsync(id);
        if (existing == null) return null;

        existing.Title = input.Title;
        existing.Description = input.Description;
        existing.RemindAt = input.RemindAt;

        await _db.SaveChangesAsync();
        return existing;
    }

    // Delete task
    public async Task<bool> DeleteTaskAsync(Guid id)
    {
        var existing = await _db.Tasks.FindAsync(id);
        if (existing == null) return false;

        _db.Tasks.Remove(existing);
        await _db.SaveChangesAsync();
        return true;
    }
}
