using TrackerApp.Domain.Abstractions;
using TrackerApp.Domain.Models;

namespace TrackerApp.Application.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;
    public TasksService(ITasksRepository taskRepository)
    {
        _tasksRepository = taskRepository;
    }

    public async Task<List<TaskModel>?> GetAllTasks(Guid userId)
    {
        return await _tasksRepository.Get(userId);
    }

    public async Task<Guid> CreateTask(TaskModel task)
    {
        return await _tasksRepository.Create(task);
    }

    public async Task<bool> UpdateTask(TaskModel task, Guid userId)
    {
        return await _tasksRepository.Update(task, userId);
    }

    public async Task<bool> DeleteTask(Guid taskId, Guid userId)
    {
        return await _tasksRepository.Delete(taskId, userId);
    }
}
