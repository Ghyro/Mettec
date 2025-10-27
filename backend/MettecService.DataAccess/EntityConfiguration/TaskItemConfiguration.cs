using MettecService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MettecService.DataAccess.EntityConfiguration;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(5000);
        
        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);
    }
}