using System.Text.Json.Serialization;
using TaskFlow.AnalyticsApi.Models;
using TaskFlow.AnalyticsApi.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TaskPriority>(allowIntegerValues: false));
});

builder.Services.AddSingleton<IStatisticsStrategy, BasicStatisticsStrategy>();
builder.Services.AddSingleton<IStatisticsStrategy, WeightedStatisticsStrategy>();

var app = builder.Build();

app.MapGet("/", () => "TaskFlow Analytics API is running.");

app.MapPost("/api/statistics", (
    StatisticsRequest? request,
    string? strategy,
    IEnumerable<IStatisticsStrategy> strategies) =>
{
    if (!IsValidRequest(request))
    {
        return Results.BadRequest(new { message = "The statistics request must contain valid task summaries." });
    }

    var strategyName = string.IsNullOrWhiteSpace(strategy)
        ? "basic"
        : strategy.Trim().ToLowerInvariant();

    var selectedStrategy = strategies.FirstOrDefault(availableStrategy =>
        string.Equals(availableStrategy.Name, strategyName, StringComparison.OrdinalIgnoreCase));

    if (selectedStrategy is null)
    {
        return Results.BadRequest(new { message = "Strategy must be one of: basic, weighted." });
    }

    return Results.Ok(selectedStrategy.Calculate(request!));
});

app.Run();

static bool IsValidRequest(StatisticsRequest? request)
{
    return request?.Tasks is not null &&
        request.Tasks.All(task =>
            task.Priority.HasValue &&
            task.DueDate.HasValue &&
            task.IsCompleted.HasValue);
}
