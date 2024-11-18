using TrackerApp.Domain.Models;

namespace TrackerApp.Domain.Abstractions;

public interface ITasksService
{
    Task<Guid> CreateTask(TaskModel task);
    Task<bool> DeleteTask(Guid taskId, Guid userId);
    Task<List<TaskModel>?> GetAllTasks(Guid userId);
    Task<bool> UpdateTask(TaskModel task, Guid userId);
}