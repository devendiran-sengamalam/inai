namespace Inai.Core.Contracts;

public record CreateTaskRequest(string Title, string? Description, DateTime? DueDate, Guid UserId);
public record TaskResponse(Guid Id, string Title, string? Description, DateTime? DueDate, bool IsCompleted);
