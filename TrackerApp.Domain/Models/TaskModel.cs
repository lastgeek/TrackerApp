namespace TrackerApp.Domain.Models;

public class TaskModel
{
    public const int MAX_TITLE_LENGTH = 250;
    public const int MAX_DISCRIPTION_LENGTH = 1000;
    public const int MAX_STATUS_LENGTH = 50;
    public const int MAX_PRIORITY_LENGTH = 20;

    private TaskModel(Guid id, string title, string description, string status, 
        string priority, DateTime deadline, Guid assignedUserId)
    {
        Id = id; 
        Title = title; 
        Description = description; 
        Status = status; 
        Priority = priority; 
        Deadline = deadline;
        AssignedUserId = assignedUserId;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public string Status { get; } = string.Empty;
    public string Priority { get; } = string.Empty;
    public DateTime Deadline { get; } = DateTime.MaxValue;
    public Guid AssignedUserId { get; }

    public static (TaskModel Task, string Error) Create(Guid id, string title, string description, string status,
        string priority, DateTime deadline, Guid assignedUserId)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            error = $"Title can not be empty or longer than {MAX_TITLE_LENGTH} symbols.";
        }
        else if (description.Length > MAX_DISCRIPTION_LENGTH)
        {
            error = $"Discription can not be longer than {MAX_DISCRIPTION_LENGTH} symbols.";
        }
        else if (string.IsNullOrEmpty(status) || status.Length > MAX_STATUS_LENGTH)
        {
            error = $"Status can not be empty or longer than {MAX_STATUS_LENGTH} symbols.";
        }
        else if (string.IsNullOrEmpty(priority) || priority.Length > MAX_PRIORITY_LENGTH) 
        {
            error = $"Priority can not be empty or longer than {MAX_PRIORITY_LENGTH} symbols.";
        }

        var task = new TaskModel(id, title, description, status, priority, deadline, assignedUserId);

        return (task, error);
    }
}
