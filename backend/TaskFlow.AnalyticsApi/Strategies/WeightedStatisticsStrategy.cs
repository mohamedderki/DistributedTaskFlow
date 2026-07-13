using TaskFlow.AnalyticsApi.Models;

namespace TaskFlow.AnalyticsApi.Strategies;

public sealed class WeightedStatisticsStrategy : IStatisticsStrategy
{
    public string Name => "weighted";

    public StatisticsResult Calculate(StatisticsRequest request)
    {
        var tasks = request.Tasks ?? [];
        var weightedOpenScore = tasks
            .Where(task => task.IsCompleted == false)
            .Sum(task => GetPriorityWeight(task.Priority));

        return BasicStatisticsStrategy.CalculateBasicStatistics(tasks, Name, weightedOpenScore);
    }

    private static int GetPriorityWeight(TaskPriority? priority)
    {
        return priority switch
        {
            TaskPriority.Low => 1,
            TaskPriority.Medium => 2,
            TaskPriority.High => 3,
            _ => 0
        };
    }
}
