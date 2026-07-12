using TaskFlow.TaskApi.Models;
using TaskFlow.TaskApi.Repositories;

namespace TaskFlow.TaskApi.Services;

public sealed class TaskService
{
    private const int MaxTitleLength = 120;
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskServiceResult<IReadOnlyList<TaskItem>>> GetTasksAsync(
        string? status,
        string? search,
        CancellationToken cancellationToken = default)
    {
        var normalizedStatus = NormalizeStatus(status);
        if (normalizedStatus is null)
        {
            return TaskServiceResult<IReadOnlyList<TaskItem>>.ValidationError(
                "Status must be one of: all, open, completed.");
        }

        var tasks = await _taskRepository.GetAllAsync(cancellationToken);
        IEnumerable<TaskItem> filteredTasks = tasks;

        filteredTasks = normalizedStatus switch
        {
            "open" => filteredTasks.Where(task => !task.IsCompleted),
            "completed" => filteredTasks.Where(task => task.IsCompleted),
            _ => filteredTasks
        };

        var normalizedSearch = search?.Trim();
        if (!string.IsNullOrWhiteSpace(normalizedSearch))
        {
            filteredTasks = filteredTasks.Where(task =>
                task.Title.Contains(normalizedSearch, StringComparison.OrdinalIgnoreCase));
        }

        return TaskServiceResult<IReadOnlyList<TaskItem>>.Success(filteredTasks.ToList());
    }

    public Task<TaskItem?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _taskRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<TaskServiceResult<TaskItem>> CreateTaskAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var validation = ValidateTaskValues(request.Title, request.Priority, request.DueDate);
        if (!validation.IsValid)
        {
            return TaskServiceResult<TaskItem>.ValidationError(validation.ErrorMessage);
        }

        var task = new TaskItem
        {
            Title = validation.Title,
            Priority = validation.Priority,
            DueDate = validation.DueDate,
            IsCompleted = false,
            CreatedAtUtc = DateTime.UtcNow
        };

        var createdTask = await _taskRepository.AddAsync(task, cancellationToken);
        return TaskServiceResult<TaskItem>.Success(createdTask);
    }

    public async Task<TaskServiceResult<TaskItem>> UpdateTaskAsync(
        int id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id, cancellationToken);
        if (existingTask is null)
        {
            return TaskServiceResult<TaskItem>.NotFound($"Task with ID {id} was not found.");
        }

        var validation = ValidateTaskValues(request.Title, request.Priority, request.DueDate);
        if (!validation.IsValid)
        {
            return TaskServiceResult<TaskItem>.ValidationError(validation.ErrorMessage);
        }

        existingTask.Title = validation.Title;
        existingTask.Priority = validation.Priority;
        existingTask.DueDate = validation.DueDate;

        await _taskRepository.UpdateAsync(existingTask, cancellationToken);
        return TaskServiceResult<TaskItem>.Success(existingTask);
    }

    public async Task<TaskServiceResult<TaskItem>> ToggleCompletionAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var existingTask = await _taskRepository.GetByIdAsync(id, cancellationToken);
        if (existingTask is null)
        {
            return TaskServiceResult<TaskItem>.NotFound($"Task with ID {id} was not found.");
        }

        existingTask.IsCompleted = !existingTask.IsCompleted;

        await _taskRepository.UpdateAsync(existingTask, cancellationToken);
        return TaskServiceResult<TaskItem>.Success(existingTask);
    }

    public async Task<TaskServiceResult<bool>> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleted = await _taskRepository.DeleteAsync(id, cancellationToken);
        return deleted
            ? TaskServiceResult<bool>.Success(true)
            : TaskServiceResult<bool>.NotFound($"Task with ID {id} was not found.");
    }

    private static string? NormalizeStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return "all";
        }

        var normalizedStatus = status.Trim().ToLowerInvariant();
        return normalizedStatus is "all" or "open" or "completed" ? normalizedStatus : null;
    }

    private static TaskValidationResult ValidateTaskValues(
        string? title,
        TaskPriority? priority,
        DateOnly? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return TaskValidationResult.Invalid("Title is required.");
        }

        var trimmedTitle = title.Trim();
        if (trimmedTitle.Length > MaxTitleLength)
        {
            return TaskValidationResult.Invalid("Title may contain at most 120 characters.");
        }

        if (priority is null || !Enum.IsDefined(priority.Value))
        {
            return TaskValidationResult.Invalid("Priority must be one of: Low, Medium, High.");
        }

        if (dueDate is null || dueDate.Value == default)
        {
            return TaskValidationResult.Invalid("Due date must be provided.");
        }

        return TaskValidationResult.Valid(trimmedTitle, priority.Value, dueDate.Value);
    }

    private sealed record TaskValidationResult(
        bool IsValid,
        string Title,
        TaskPriority Priority,
        DateOnly DueDate,
        string ErrorMessage)
    {
        public static TaskValidationResult Valid(string title, TaskPriority priority, DateOnly dueDate)
        {
            return new(true, title, priority, dueDate, string.Empty);
        }

        public static TaskValidationResult Invalid(string message)
        {
            return new(false, string.Empty, default, default, message);
        }
    }
}

public enum TaskServiceError
{
    None,
    Validation,
    NotFound
}

public sealed record TaskServiceResult<T>(
    T? Value,
    string ErrorMessage,
    TaskServiceError Error)
{
    public bool IsSuccess => Error == TaskServiceError.None;

    public static TaskServiceResult<T> Success(T value) => new(value, string.Empty, TaskServiceError.None);

    public static TaskServiceResult<T> ValidationError(string message) => new(default, message, TaskServiceError.Validation);

    public static TaskServiceResult<T> NotFound(string message) => new(default, message, TaskServiceError.NotFound);
}
