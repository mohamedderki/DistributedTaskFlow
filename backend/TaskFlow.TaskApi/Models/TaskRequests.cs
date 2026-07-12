namespace TaskFlow.TaskApi.Models;

public sealed record CreateTaskRequest(
    string? Title,
    TaskPriority? Priority,
    DateOnly? DueDate);

public sealed record UpdateTaskRequest(
    string? Title,
    TaskPriority? Priority,
    DateOnly? DueDate);
