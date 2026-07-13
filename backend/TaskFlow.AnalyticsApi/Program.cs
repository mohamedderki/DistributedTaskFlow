using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using TaskFlow.AnalyticsApi.Models;
using TaskFlow.AnalyticsApi.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TaskPriority>(allowIntegerValues: false));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskFlow Analytics API",
        Version = "v1",
        Description = "Calculates basic and weighted statistics for task data received through HTTP and JSON."
    });
});

builder.Services.AddSingleton<IStatisticsStrategy, BasicStatisticsStrategy>();
builder.Services.AddSingleton<IStatisticsStrategy, WeightedStatisticsStrategy>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "TaskFlow Analytics API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow Analytics API v1");
    });
}

app.MapGet("/", () => "TaskFlow Analytics API is running.")
    .WithName("CheckAnalyticsApiAvailability")
    .WithTags("System")
    .WithSummary("Check Analytics API availability")
    .Produces<string>(StatusCodes.Status200OK, "text/plain");

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
})
    .WithName("CalculateTaskStatistics")
    .WithTags("Statistics")
    .WithSummary("Calculate task statistics")
    .WithDescription("Calculates statistics from task summaries using the selected Strategy Pattern implementation. The optional strategy query parameter supports basic or weighted and defaults to basic.")
    .Accepts<StatisticsRequest>("application/json")
    .Produces<StatisticsResult>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);

app.Run();

static bool IsValidRequest(StatisticsRequest? request)
{
    return request?.Tasks is not null &&
        request.Tasks.All(task =>
            task.Priority.HasValue &&
            task.DueDate.HasValue &&
            task.IsCompleted.HasValue);
}
