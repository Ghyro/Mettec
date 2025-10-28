using Moq;
using MettecService.Core.Services;
using MettecService.DataAccess.Entities;
using MettecService.DataAccess.Repositories;
using FluentAssertions;

namespace MettecService.Tests.Services;

[TestFixture]
public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _taskService = new TaskService(_taskRepositoryMock.Object);
    }

    [Test]
    public async Task GetTaskByIdAsync_ReturnsTask()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var taskItem = new TaskItem { Id = taskId, Title = "Test", Description = "Desc", IsCompleted = false };
        _taskRepositoryMock
            .Setup(x => x.GetTaskByIdAsync(taskId, false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskItem);

        // Act
        var result = await _taskService.GetTaskByIdAsync(taskId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(taskId);
        _taskRepositoryMock.Verify(x => x.GetTaskByIdAsync(taskId, false, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task GetTasksAsync_ReturnsAllTasks()
    {
        // Arrange
        var tasks = new List<TaskItem>
        {
            new TaskItem { Id = Guid.NewGuid(), Title = "T1", Description = "D1", IsCompleted = false },
            new TaskItem { Id = Guid.NewGuid(), Title = "T2", Description = "D2", IsCompleted = true }
        };
        _taskRepositoryMock
            .Setup(x => x.GetTasksAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);

        // Act
        var result = await _taskService.GetTasksAsync();

        // Assert
        result.Count.Should().Be(2);
        _taskRepositoryMock.Verify(x => x.GetTasksAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task CreateTaskAsync_CreatesTask()
    {
        // Arrange
        var taskItem = new TaskItem { Title = "New Task", Description = "New Desc", IsCompleted = false };
        _taskRepositoryMock
            .Setup(x => x.CreateTaskAsync(taskItem, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskItem);

        // Act
        var result = await _taskService.CreateTaskAsync(taskItem);

        // Assert
        result.Should().Be(taskItem);
        _taskRepositoryMock.Verify(x => x.CreateTaskAsync(taskItem, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateTaskStatusAsync_UpdatesIsCompleted()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        var taskItem = new TaskItem { Id = taskId, Title = "Task", Description = "Desc", IsCompleted = false };
        _taskRepositoryMock
            .Setup(x => x.GetTaskByIdAsync(taskId, true, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskItem);

        _taskRepositoryMock
            .Setup(x => x.UpdateTaskAsync(taskItem, It.IsAny<CancellationToken>()))
            .ReturnsAsync(taskItem);

        // Act
        var result = await _taskService.UpdateTaskStatusAsync(taskId, true);

        // Assert
        result.IsCompleted.Should().BeTrue();
        _taskRepositoryMock.Verify(x => x.GetTaskByIdAsync(taskId, true, It.IsAny<CancellationToken>()), Times.Once);
        _taskRepositoryMock.Verify(x => x.UpdateTaskAsync(taskItem, It.IsAny<CancellationToken>()), Times.Once);
    }
}