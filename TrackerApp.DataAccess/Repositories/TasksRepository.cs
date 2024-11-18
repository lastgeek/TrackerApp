using Microsoft.EntityFrameworkCore;
using TrackerApp.DataAccess.Entities;
using TrackerApp.Domain.Abstractions;
using TrackerApp.Domain.Models;

namespace TrackerApp.DataAccess.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly TaskDbContext _context;

    public TasksRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskModel>?> Get(Guid userId)
    {
        var taskEntities = await _context.Tasks.Where(t => t.AssignedUserId == userId).AsNoTracking().ToListAsync();

        var tasks = taskEntities
            .Select(t => TaskModel.Create(
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.Priority,
                t.Deadline,
                t.AssignedUserId).Task
            ).ToList();

        return tasks;
    }

    public async Task<Guid> Create(TaskModel taskModel)
    {
        var user = _context.Users.Where(u => u.Id == taskModel.AssignedUserId).FirstOrDefault();

        if(user == null)
        {
            return Guid.Empty;
        }

        var taskEntity = new TaskEntity
        {
            Title = taskModel.Title,
            Description = taskModel.Description,
            Status = taskModel.Status,
            Priority = taskModel.Priority,
            Deadline = taskModel.Deadline,
            AssignedUserId = taskModel.AssignedUserId,
            AssignedUser = user
        };

        _context.Tasks.Add(taskEntity);
        await _context.SaveChangesAsync();

        return taskEntity.Id;
    }

    public async Task<bool> Update(TaskModel updatedModel, Guid userId)
    {
        var taskEntity = await _context.Tasks
            .Where(t => t.Id == updatedModel.Id && t.AssignedUserId == userId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, t => updatedModel.Title)
                .SetProperty(t => t.Description, t => updatedModel.Description)
                .SetProperty(t => t.Status, t => updatedModel.Status)
                .SetProperty(t => t.Priority, t => updatedModel.Priority)
                .SetProperty(t => t.Deadline, t => updatedModel.Deadline)
            );

        return true;
    }

    public async Task<bool> Delete(Guid id, Guid userId)
    {
        var taskEntity = await _context.Tasks.Where(t => t.Id == id && t.AssignedUserId == userId).ExecuteDeleteAsync();

        return true;
    }

}
