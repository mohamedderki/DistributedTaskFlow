using System.Text.Json.Serialization;
using TaskFlow.TaskApi.Models;
using TaskFlow.TaskApi.Repositories;
using TaskFlow.TaskApi.Services;

var builder = WebApplication.CreateBuilder(args);

const string frontendCorsPolicy = "NextJsFrontend";

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TaskPriority>(allowIntegerValues: false));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(frontendCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
            .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<ITaskRepository, SqliteTaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddHttpClient<IAnalyticsClient, AnalyticsClient>((serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseAddress = configuration["AnalyticsApi:BaseAddress"]
        ?? throw new InvalidOperationException("Analytics API base address is not configured.");

    client.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();

app.UseCors(frontendCorsPolicy);

app.MapGet("/", () => "TaskFlow Task API is running.");

var tasks = app.MapGroup("/api/tasks");

tasks.MapGet("", async (
    string? status,
    string? search,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.GetTasksAsync(status, search, cancellationToken);
    return ToHttpResult(result, Results.Ok);
});

tasks.MapPost("", async (
    CreateTaskRequest request,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.CreateTaskAsync(request, cancellationToken);
    return ToHttpResult(result, task => Results.Created($"/api/tasks/{task.Id}", task));
});

tasks.MapPut("{id:int}", async (
    int id,
    UpdateTaskRequest request,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.UpdateTaskAsync(id, request, cancellationToken);
    return ToHttpResult(result, Results.Ok);
});

tasks.MapPatch("{id:int}/toggle", async (
    int id,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.ToggleCompletionAsync(id, cancellationToken);
    return ToHttpResult(result, Results.Ok);
});

tasks.MapDelete("{id:int}", async (
    int id,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.DeleteTaskAsync(id, cancellationToken);
    return ToHttpResult(result, _ => Results.NoContent());
});

app.MapGet("/api/dashboard", async (
    string? strategy,
    TaskService taskService,
    IAnalyticsClient analyticsClient,
    CancellationToken cancellationToken) =>
{
    var normalizedStrategy = NormalizeAnalyticsStrategy(strategy);
    if (normalizedStrategy is null)
    {
        return Results.BadRequest(new { message = "Strategy must be one of: basic, weighted." });
    }

    var tasksResult = await taskService.GetTasksAsync(status: null, search: null, cancellationToken);
    if (!tasksResult.IsSuccess || tasksResult.Value is null)
    {
        return Results.Problem("Tasks could not be loaded for dashboard statistics.");
    }

    try
    {
        var statistics = await analyticsClient.CalculateStatisticsAsync(
            tasksResult.Value,
            normalizedStrategy,
            cancellationToken);

        return Results.Ok(statistics);
    }
    catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or AnalyticsClientException)
    {
        return Results.Json(
            new { message = "Statistics are temporarily unavailable. Your tasks can still be managed." },
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }
});

app.Run();

static IResult ToHttpResult<T>(TaskServiceResult<T> result, Func<T, IResult> successResult)
{
    if (result.IsSuccess && result.Value is not null)
    {
        return successResult(result.Value);
    }

    return result.Error switch
    {
        TaskServiceError.Validation => Results.BadRequest(new { message = result.ErrorMessage }),
        TaskServiceError.NotFound => Results.NotFound(new { message = result.ErrorMessage }),
        _ => Results.Problem("The request could not be completed.")
    };
}

static string? NormalizeAnalyticsStrategy(string? strategy)
{
    if (string.IsNullOrWhiteSpace(strategy))
    {
        return "basic";
    }

    var normalizedStrategy = strategy.Trim().ToLowerInvariant();
    return normalizedStrategy is "basic" or "weighted" ? normalizedStrategy : null;
}
