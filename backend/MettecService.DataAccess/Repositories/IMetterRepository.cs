using MettecService.DataAccess.Entities;

namespace MettecService.DataAccess.Repositories;

public interface IMetterRepository
{
    Task<TaskItem?> GetTaskByIdAsync(Guid id, bool asTracking, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<TaskItem>> GetTasksAsync(CancellationToken cancellationToken = default);

    Task<TaskItem> CreateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default);

    Task<TaskItem> UpdateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default);
}