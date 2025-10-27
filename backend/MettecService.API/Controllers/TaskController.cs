using AutoMapper;
using MettecService.API.Models;
using MettecService.Core.Services;
using MettecService.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MettecService.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController(
    IMetterService metterService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TaskDto>>> GetTasks(
        CancellationToken cancellationToken = default)
    {
        var tasks = await metterService.GetTasksAsync(cancellationToken);
        return Ok(mapper.Map<IReadOnlyCollection<TaskDto>>(tasks));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<TaskDto>>> GetTaskById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var tasks = await metterService.GetTaskByIdAsync(id, cancellationToken);
        return Ok(mapper.Map<TaskDto>(tasks));
    }


    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskDto>> CreateTask(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var task = mapper.Map<TaskItem>(request);
        var created = await metterService.CreateTaskAsync(task, cancellationToken);
        return Ok(mapper.Map<TaskDto>(created));
    }

    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskDto>> UpdateTaskStatus(
        Guid id,
        [FromBody] UpdateTaskStatusRequest statusRequest,
        CancellationToken cancellationToken = default)
    {
        var updated = await metterService.UpdateTaskStatusAsync(id, statusRequest.IsCompleted, cancellationToken);
        return Ok(updated);
    }
}