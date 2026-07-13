using TaskFlow.AnalyticsApi.Models;

namespace TaskFlow.AnalyticsApi.Strategies;

public interface IStatisticsStrategy
{
    string Name { get; }

    StatisticsResult Calculate(StatisticsRequest request);
}
