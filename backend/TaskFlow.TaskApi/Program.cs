using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using TaskFlow.TaskApi.Models;
using TaskFlow.TaskApi.Repositories;
using TaskFlow.TaskApi.Services;

var builder = WebApplication.CreateBuilder(args);

const string frontendCorsPolicy = "NextJsFrontend";
string[] frontendDevelopmentOrigins =
[
    "http://localhost:3000",
    "http://127.0.0.1:3000",
    "http://172.27.176.1:3000"
];

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter<TaskPriority>(allowIntegerValues: false));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(frontendCorsPolicy, policy =>
    {
        policy.WithOrigins(frontendDevelopmentOrigins)
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS")
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TaskFlow Task API",
        Version = "v1",
        Description = "Manages tasks and provides distributed dashboard statistics through the Analytics API."
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "TaskFlow Task API";
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskFlow Task API v1");
    });
}

app.UseCors(frontendCorsPolicy);

app.MapGet("/", () => "TaskFlow Task API is running.")
    .WithName("CheckTaskApiAvailability")
    .WithTags("System")
    .WithSummary("Check Task API availability")
    .Produces<string>(StatusCodes.Status200OK, "text/plain");

var tasks = app.MapGroup("/api/tasks");

tasks.MapGet("", async (
    string? status,
    string? search,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.GetTasksAsync(status, search, cancellationToken);
    return ToHttpResult(result, Results.Ok);
})
    .WithName("RetrieveTasks")
    .WithTags("Tasks")
    .WithSummary("Retrieve tasks")
    .WithDescription("Returns tasks and optionally filters them by status or searches their titles. The status query parameter supports all, open, or completed. The search query parameter accepts optional title search text.")
    .Produces<IReadOnlyList<TaskItem>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest);

tasks.MapPost("", async (
    CreateTaskRequest request,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.CreateTaskAsync(request, cancellationToken);
    return ToHttpResult(result, task => Results.Created($"/api/tasks/{task.Id}", task));
})
    .WithName("CreateTask")
    .WithTags("Tasks")
    .WithSummary("Create a task")
    .WithDescription("Creates a task from the provided title, priority, and due date.")
    .Accepts<CreateTaskRequest>("application/json")
    .Produces<TaskItem>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

tasks.MapPut("{id:int}", async (
    int id,
    UpdateTaskRequest request,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.UpdateTaskAsync(id, request, cancellationToken);
    return ToHttpResult(result, Results.Ok);
})
    .WithName("UpdateTask")
    .WithTags("Tasks")
    .WithSummary("Update a task")
    .WithDescription("Updates the title, priority, and due date of an existing task identified by the id path parameter.")
    .Accepts<UpdateTaskRequest>("application/json")
    .Produces<TaskItem>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);

tasks.MapPatch("{id:int}/toggle", async (
    int id,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.ToggleCompletionAsync(id, cancellationToken);
    return ToHttpResult(result, Results.Ok);
})
    .WithName("ToggleTaskCompletion")
    .WithTags("Tasks")
    .WithSummary("Toggle task completion")
    .WithDescription("Toggles the completion status of the task identified by the id path parameter.")
    .Produces<TaskItem>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);

tasks.MapDelete("{id:int}", async (
    int id,
    TaskService taskService,
    CancellationToken cancellationToken) =>
{
    var result = await taskService.DeleteTaskAsync(id, cancellationToken);
    return ToHttpResult(result, _ => Results.NoContent());
})
    .WithName("DeleteTask")
    .WithTags("Tasks")
    .WithSummary("Delete a task")
    .WithDescription("Deletes the task identified by the id path parameter.")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status404NotFound);

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
})
    .WithName("RetrieveDistributedDashboardStatistics")
    .WithTags("Dashboard")
    .WithSummary("Retrieve distributed dashboard statistics")
    .WithDescription("Loads tasks from SQLite and requests calculated statistics from the separately running Analytics API. The optional strategy query parameter supports basic or weighted.")
    .Produces<AnalyticsStatisticsResult>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status503ServiceUnavailable);

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
