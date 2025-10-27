using MettecService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MettecService.DataAccess;

public class MettecDbContext(DbContextOptions<MettecDbContext> options)
    : DbContext(options)
{
    public virtual DbSet<TaskItem> TaskItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MettecDbContext).Assembly);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<GenericModel>())
        {
            entry.Entity.Id = entry.State switch
            {
                EntityState.Added => Guid.NewGuid(),
                _ => entry.Entity.Id
            };
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}