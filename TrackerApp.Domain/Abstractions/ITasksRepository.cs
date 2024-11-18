using TrackerApp.Domain.Models;

namespace TrackerApp.Domain.Abstractions;

public interface ITasksRepository
{
    Task<Guid> Create(TaskModel taskModel);
    Task<bool> Delete(Guid id, Guid userId);
    Task<List<TaskModel>?> Get(Guid userId);
    Task<bool> Update(TaskModel updatedModel, Guid userId);
}