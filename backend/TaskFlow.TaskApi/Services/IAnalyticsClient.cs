using TaskFlow.TaskApi.Models;

namespace TaskFlow.TaskApi.Services;

public interface IAnalyticsClient
{
    Task<AnalyticsStatisticsResult> CalculateStatisticsAsync(
        IReadOnlyCollection<TaskItem> tasks,
        string strategy,
        CancellationToken cancellationToken = default);
}
