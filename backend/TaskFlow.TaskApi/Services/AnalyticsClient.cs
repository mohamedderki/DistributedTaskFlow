using System.Net;
using System.Net.Http.Json;
using TaskFlow.TaskApi.Models;

namespace TaskFlow.TaskApi.Services;

public sealed class AnalyticsClient : IAnalyticsClient
{
    private readonly HttpClient _httpClient;

    public AnalyticsClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AnalyticsStatisticsResult> CalculateStatisticsAsync(
        IReadOnlyCollection<TaskItem> tasks,
        string strategy,
        CancellationToken cancellationToken = default)
    {
        var request = new AnalyticsStatisticsRequest(
            tasks.Select(task => new AnalyticsTaskSummary(
                    task.Priority.ToString(),
                    task.DueDate,
                    task.IsCompleted))
                .ToList());

        var requestUri = $"api/statistics?strategy={Uri.EscapeDataString(strategy)}";
        using var response = await _httpClient.PostAsJsonAsync(requestUri, request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new AnalyticsClientException(
                response.StatusCode,
                $"Analytics API returned unexpected status code {(int)response.StatusCode}. Response body: {responseBody}");
        }

        var statistics = await response.Content.ReadFromJsonAsync<AnalyticsStatisticsResult>(cancellationToken);
        return statistics ?? throw new AnalyticsClientException(
            response.StatusCode,
            "Analytics API returned an empty statistics response.");
    }
}

public sealed class AnalyticsClientException : Exception
{
    public AnalyticsClientException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpStatusCode StatusCode { get; }
}
