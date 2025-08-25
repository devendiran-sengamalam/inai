using Inai.Core.Contracts;

namespace Inai.Core.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateTaskAsync(CreateTaskRequest request);
    Task<IEnumerable<TaskResponse>> GetTasksAsync(Guid userId);
    Task<TaskResponse?> CompleteTaskAsync(Guid taskId);
}
