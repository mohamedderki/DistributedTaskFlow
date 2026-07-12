namespace TaskFlow.TaskApi.Models;

public sealed class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public TaskPriority Priority { get; set; }

    public DateOnly DueDate { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAtUtc { get; set; }
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}
