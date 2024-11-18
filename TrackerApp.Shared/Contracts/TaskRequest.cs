namespace TrackerApp.Shared.Contracts;

public class TaskRequest
{
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public string status { get; set; } = "Pending";
    public string priority { get; set; } = "Medium";
    public DateTime deadline { get; set; } = DateTime.Now;
}

