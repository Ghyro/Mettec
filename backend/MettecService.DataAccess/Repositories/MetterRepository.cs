using MettecService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MettecService.DataAccess.Repositories;

public class MetterRepository(MettecDbContext dbContext) : IMetterRepository
{
    public async Task<TaskItem?> GetTaskByIdAsync(Guid id, bool asTracking = false,
        CancellationToken cancellationToken = default)
    {
        var query = dbContext.TaskItems.AsQueryable();

        if (asTracking)
            query = query.AsTracking();

        return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<TaskItem>> GetTasksAsync(CancellationToken cancellationToken = default)
        => await dbContext.TaskItems.ToListAsync(cancellationToken);

    public async Task<TaskItem> CreateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        await dbContext.AddAsync(taskItem, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return taskItem;
    }

    public async Task<TaskItem> UpdateTaskAsync(TaskItem taskItem, CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
        return taskItem;
    }
}