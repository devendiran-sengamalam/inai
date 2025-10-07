namespace Inai.Core.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; }
    // Optional default reminder
    public DateTime? RemindAt { get; set; }
    public ICollection<Reminder>? Reminders { get; set; }
}
