using MettecService.DataAccess.Entities;
using MettecService.DataAccess.Repositories;

namespace MettecService.Core.Services;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public async Task<TaskItem?> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await taskRepository.GetTaskByIdAsync(id, false, cancellationToken);

    public async Task<IReadOnlyCollection<TaskItem>> GetTasksAsync(CancellationToken cancellationToken = default) 
        => await taskRepository.GetTasksAsync(cancellationToken);

    public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
       => await taskRepository.CreateTaskAsync(taskItem, cancellationToken);

    public async Task<TaskItem> UpdateTaskStatusAsync(Guid id, bool isCompleted, CancellationToken cancellationToken = default)
    {
        var existing = await taskRepository.GetTaskByIdAsync(id, true, cancellationToken);
        existing.IsCompleted = isCompleted;
        await taskRepository.UpdateTaskAsync(existing, cancellationToken);
        return existing;
    }
}