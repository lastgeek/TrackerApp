namespace TrackerApp.DataAccess.Entities;

public class TaskEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime Deadline { get; set; } = DateTime.MaxValue;
    public Guid AssignedUserId { get; set; }
    public UserEntity AssignedUser { get; set; } = new UserEntity();
}
