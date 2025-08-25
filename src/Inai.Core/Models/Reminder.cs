namespace Inai.Core.Models;

public class Reminder
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TaskItemId { get; set; }
    public TaskItem? TaskItem { get; set; }
    public DateTime RemindAt { get; set; }
    public bool IsSent { get; set; } = false;
}