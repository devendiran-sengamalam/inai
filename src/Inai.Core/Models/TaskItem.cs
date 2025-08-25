namespace Inai.Core.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Relationships
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public List<Reminder> Reminders { get; set; } = [];
}
