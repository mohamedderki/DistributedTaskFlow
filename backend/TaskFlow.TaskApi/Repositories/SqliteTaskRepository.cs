using System.Globalization;
using Microsoft.Data.Sqlite;
using TaskFlow.TaskApi.Models;

namespace TaskFlow.TaskApi.Repositories;

public sealed class SqliteTaskRepository : ITaskRepository
{
    private const string DateFormat = "yyyy-MM-dd";
    private readonly string _connectionString;

    public SqliteTaskRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("TaskFlowDatabase")
            ?? throw new InvalidOperationException("Connection string 'TaskFlowDatabase' is missing.");

        InitializeDatabase();
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT Id, Title, Priority, DueDate, IsCompleted, CreatedAtUtc
            FROM Tasks
            ORDER BY IsCompleted ASC, DueDate ASC, CreatedAtUtc DESC;
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var tasks = new List<TaskItem>();

        while (await reader.ReadAsync(cancellationToken))
        {
            tasks.Add(ReadTask(reader));
        }

        return tasks;
    }

    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT Id, Title, Priority, DueDate, IsCompleted, CreatedAtUtc
            FROM Tasks
            WHERE Id = $id;
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$id", id);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? ReadTask(reader) : null;
    }

    public async Task<TaskItem> AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO Tasks (Title, Priority, DueDate, IsCompleted, CreatedAtUtc)
            VALUES ($title, $priority, $dueDate, $isCompleted, $createdAtUtc);

            SELECT last_insert_rowid();
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddTaskParameters(command, task, includeId: false, includeCreatedAtUtc: true);

        var id = (long)(await command.ExecuteScalarAsync(cancellationToken)
            ?? throw new InvalidOperationException("SQLite did not return a task ID."));

        task.Id = (int)id;
        return task;
    }

    public async Task<bool> UpdateAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        const string sql = """
            UPDATE Tasks
            SET Title = $title,
                Priority = $priority,
                DueDate = $dueDate,
                IsCompleted = $isCompleted
            WHERE Id = $id;
            """;

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        AddTaskParameters(command, task, includeId: true, includeCreatedAtUtc: false);

        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM Tasks WHERE Id = $id;";

        await using var connection = CreateConnection();
        await connection.OpenAsync(cancellationToken);

        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.Parameters.AddWithValue("$id", id);

        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    private void InitializeDatabase()
    {
        const string sql = """
            CREATE TABLE IF NOT EXISTS Tasks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Priority TEXT NOT NULL,
                DueDate TEXT NOT NULL,
                IsCompleted TEXT NOT NULL,
                CreatedAtUtc TEXT NOT NULL
            );
            """;

        using var connection = CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private SqliteConnection CreateConnection() => new(_connectionString);

    private static void AddTaskParameters(SqliteCommand command, TaskItem task, bool includeId, bool includeCreatedAtUtc)
    {
        command.Parameters.AddWithValue("$title", task.Title);
        command.Parameters.AddWithValue("$priority", task.Priority.ToString());
        command.Parameters.AddWithValue("$dueDate", task.DueDate.ToString(DateFormat, CultureInfo.InvariantCulture));
        command.Parameters.AddWithValue("$isCompleted", task.IsCompleted ? "true" : "false");
        if (includeCreatedAtUtc)
        {
            command.Parameters.AddWithValue("$createdAtUtc", task.CreatedAtUtc.ToString("O", CultureInfo.InvariantCulture));
        }

        if (includeId)
        {
            command.Parameters.AddWithValue("$id", task.Id);
        }
    }

    private static TaskItem ReadTask(SqliteDataReader reader)
    {
        return new TaskItem
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Priority = Enum.Parse<TaskPriority>(reader.GetString(2)),
            DueDate = DateOnly.ParseExact(reader.GetString(3), DateFormat, CultureInfo.InvariantCulture),
            IsCompleted = string.Equals(reader.GetString(4), "true", StringComparison.OrdinalIgnoreCase),
            CreatedAtUtc = DateTime.Parse(reader.GetString(5), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind).ToUniversalTime()
        };
    }
}
