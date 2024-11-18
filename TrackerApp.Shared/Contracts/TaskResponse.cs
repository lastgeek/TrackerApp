namespace TrackerApp.Shared.Contracts;

public class TaskResponse
{
    public Guid id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string status { get; set; } = "Pending";
    public string priority { get; set; } = "Low";
    public DateTime deadline { get; set; }
}


