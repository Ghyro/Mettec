using MettecService.DataAccess.Entities;

namespace MettecService.Core.Services;

public interface ITaskService
{
    Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<TaskItem>> GetTasksAsync(CancellationToken cancellationToken = default);
    
    Task<TaskItem> CreateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
    
    Task<TaskItem> UpdateTaskStatusAsync(Guid id, bool isCompleted, CancellationToken cancellationToken = default);
}