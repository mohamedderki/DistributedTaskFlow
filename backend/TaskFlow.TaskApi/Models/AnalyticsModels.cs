namespace TaskFlow.TaskApi.Models;

public sealed record AnalyticsStatisticsRequest(IReadOnlyList<AnalyticsTaskSummary> Tasks);

public sealed record AnalyticsTaskSummary(
    string Priority,
    DateOnly DueDate,
    bool IsCompleted);

public sealed record AnalyticsStatisticsResult(
    int TotalTasks,
    int OpenTasks,
    int CompletedTasks,
    int OverdueTasks,
    double CompletionPercentage,
    int WeightedOpenScore,
    string Strategy);
