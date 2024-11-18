using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackerApp.DataAccess.Entities;
using TrackerApp.Domain.Abstractions;
using TrackerApp.Domain.Models;
using TrackerApp.Shared.Contracts;

namespace TrackerApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrackerController : ControllerBase
{
    private readonly ITasksService _tasksService;
    private readonly ILogger<TrackerController> _logger;
    public TrackerController(ITasksService tasksService, ILogger<TrackerController> logger)
    {
        _tasksService = tasksService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<TaskResponse>?>> GetTasks()
    {
        if (!User.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            return Unauthorized();
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            _logger.LogError("User ID is missing or invalid.");
            return Unauthorized("Invalid or missing user identifier.");
        }

        var tasks = await _tasksService.GetAllTasks(userId);

        if (tasks == null)
        {
            return NotFound();
        }

        var response = tasks.Select(t => new TaskResponse()
        {
            id = t.Id,
            title = t.Title,
            description = t.Description,
            status = t.Status,
            priority = t.Priority,
            deadline = t.Deadline
        });
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            return Unauthorized();
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            _logger.LogError("User ID is missing or invalid.");
            return Unauthorized("Invalid or missing user identifier.");
        }

        var result = await _tasksService.DeleteTask(id, userId);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTask([FromBody] TaskRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
        {
            return BadRequest();
        }

        var (task, error) = TaskModel.Create(
            Guid.NewGuid(), 
            request.title, 
            request.description, 
            request.status,
            request.priority,
            request.deadline,
            userId
            );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        await _tasksService.CreateTask(task);

        return Ok();
    }

    [HttpPut("{taskId:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateTask(Guid taskId, [FromBody] TaskResponse updatedTask)
    {
        if (!User.Identity.IsAuthenticated)
        {
            _logger.LogWarning("User is not authenticated.");
            return Unauthorized();
        }

        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
        {
            _logger.LogError("User ID is missing or invalid.");
            return Unauthorized("Invalid or missing user identifier.");
        }

        var task = TaskModel.Create(
            taskId,
            updatedTask.title,
            updatedTask.description,
            updatedTask.status,
            updatedTask.priority,
            updatedTask.deadline,
            userId
            ).Task;

        if (task == null)
        {
            return NotFound();
        }

        await _tasksService.UpdateTask(task, userId);

        return NoContent();
    }
}
