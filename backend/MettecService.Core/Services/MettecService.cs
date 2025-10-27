using MettecService.DataAccess.Entities;
using MettecService.DataAccess.Repositories;

namespace MettecService.Core.Services;

public class MettecService(IMetterRepository metterRepository) : IMetterService
{
    public async Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await metterRepository.GetTaskByIdAsync(id, false, cancellationToken);

    public async Task<IReadOnlyCollection<TaskItem>> GetTasksAsync(CancellationToken cancellationToken = default) 
        => await metterRepository.GetTasksAsync(cancellationToken);

    public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
       => await metterRepository.CreateTaskAsync(taskItem, cancellationToken);

    public async Task<TaskItem> UpdateTaskStatusAsync(Guid id, bool isCompleted, CancellationToken cancellationToken = default)
    {
        var existing = await metterRepository.GetTaskByIdAsync(id, true, cancellationToken);
        existing.IsCompleted = isCompleted;
        await metterRepository.UpdateTaskAsync(existing, cancellationToken);
        return existing;
    }
}