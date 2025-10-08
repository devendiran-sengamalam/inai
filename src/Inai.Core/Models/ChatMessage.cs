namespace Inai.Core.Models;

public class ChatMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsFromAI { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
