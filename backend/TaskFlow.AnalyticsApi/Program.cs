var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "TaskFlow Analytics API is running.");

app.Run();
