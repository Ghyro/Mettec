namespace MettecService.API.Models;

public class TaskDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
}