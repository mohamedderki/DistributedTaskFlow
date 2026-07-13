using TaskFlow.AnalyticsApi.Models;

namespace TaskFlow.AnalyticsApi.Strategies;

public sealed class BasicStatisticsStrategy : IStatisticsStrategy
{
    public string Name => "basic";

    public StatisticsResult Calculate(StatisticsRequest request)
    {
        return CalculateBasicStatistics(request.Tasks ?? [], Name, weightedOpenScore: 0);
    }

    internal static StatisticsResult CalculateBasicStatistics(
        IReadOnlyCollection<StatisticsTaskSummary> tasks,
        string strategyName,
        int weightedOpenScore)
    {
        var todayUtc = DateOnly.FromDateTime(DateTime.UtcNow);
        var totalTasks = tasks.Count;
        var completedTasks = tasks.Count(task => task.IsCompleted == true);
        var openTasks = totalTasks - completedTasks;
        var overdueTasks = tasks.Count(task =>
            task.IsCompleted == false &&
            task.DueDate.HasValue &&
            task.DueDate.Value < todayUtc);

        var completionPercentage = totalTasks == 0
            ? 0
            : Math.Round(completedTasks * 100.0 / totalTasks, 2);

        return new StatisticsResult(
            totalTasks,
            openTasks,
            completedTasks,
            overdueTasks,
            completionPercentage,
            weightedOpenScore,
            strategyName);
    }
}
