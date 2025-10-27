namespace MettecService.DataAccess.Entities;

public class TaskItem : GenericModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}