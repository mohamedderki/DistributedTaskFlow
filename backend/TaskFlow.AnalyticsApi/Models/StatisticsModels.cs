namespace TaskFlow.AnalyticsApi.Models;

public sealed record StatisticsRequest(IReadOnlyList<StatisticsTaskSummary>? Tasks);

public sealed record StatisticsTaskSummary(
    TaskPriority? Priority,
    DateOnly? DueDate,
    bool? IsCompleted);

public sealed record StatisticsResult(
    int TotalTasks,
    int OpenTasks,
    int CompletedTasks,
    int OverdueTasks,
    double CompletionPercentage,
    int WeightedOpenScore,
    string Strategy);

public enum TaskPriority
{
    Low,
    Medium,
    High
}
