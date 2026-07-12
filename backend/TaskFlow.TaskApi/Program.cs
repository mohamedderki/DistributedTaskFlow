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
