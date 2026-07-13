# Prompt 05 – Analytics API und verteilte Kommunikation

## Verwendeter Prompt

```text
Project guidelines:

- Keep the work focused on this specific development step.
- Follow the existing backend architecture and repository structure.
- Preserve the existing behavior of the Task API.
- Write clean and self-documenting code with clear names.
- Keep classes and methods small and focused.
- Avoid duplicated logic and unnecessary abstractions.
- Use constructor injection for service dependencies.
- Keep HTTP communication inside a dedicated client.
- Preserve all existing documentation, screenshots, and Google Stitch outputs.
- Do not modify files outside the requested scope.
- Before editing, briefly describe the planned changes.
- After finishing, list every created or modified file.
- Report the build and manual verification results.
- Stop after completing this step.

Task:

Implement the Analytics API and connect it to the existing Task API through HTTP and JSON.

The two APIs must remain independently executable processes:

- TaskFlow.TaskApi: http://localhost:5001
- TaskFlow.AnalyticsApi: http://localhost:5002

The Task API must retrieve tasks from SQLite and send the required task data to the Analytics API.

The Analytics API must calculate statistics and return the result to the Task API.

Do not add Swagger UI in this step. Swagger will be configured separately in a later step.

1. Analytics models

Create the following file inside TaskFlow.AnalyticsApi:

Models/StatisticsModels.cs

Create compact request and response models.

The analytics request must contain a collection of task summaries.

Each task summary must contain only the information required for statistics:

- Priority
- DueDate
- IsCompleted

The statistics result must contain:

- TotalTasks
- OpenTasks
- CompletedTasks
- OverdueTasks
- CompletionPercentage
- WeightedOpenScore
- Strategy

Do not add persistence or database-specific properties to the Analytics API models.

2. Strategy Pattern

Create this structure:

backend/TaskFlow.AnalyticsApi/
├── Models/
│   └── StatisticsModels.cs
└── Strategies/
    ├── IStatisticsStrategy.cs
    ├── BasicStatisticsStrategy.cs
    └── WeightedStatisticsStrategy.cs

Create the interface:

IStatisticsStrategy

It must define:

- a readable strategy name
- one method for calculating task statistics

Implement two strategies:

BasicStatisticsStrategy

This strategy must calculate:

- total tasks
- open tasks
- completed tasks
- overdue tasks
- completion percentage

Its WeightedOpenScore may be zero.

WeightedStatisticsStrategy

This strategy must calculate the same basic values and additionally calculate a weighted score for open tasks.

Use the following priority weights:

- Low = 1
- Medium = 2
- High = 3

Only open tasks contribute to WeightedOpenScore.

A task is overdue when:

- it is not completed
- its due date is earlier than the current UTC date

Avoid duplicating the basic statistics calculation unnecessarily.

3. Dependency Injection in Analytics API

Register both statistics strategies through dependency injection.

The API endpoint must receive the available strategies through dependency injection.

Do not manually create strategy instances inside the endpoint.

4. Analytics API endpoints

Keep the existing root endpoint:

GET /

Expected response:

"TaskFlow Analytics API is running."

Create:

POST /api/statistics

Add an optional query parameter:

strategy

Supported values:

- basic
- weighted

When no strategy is provided, use:

basic

Examples:

POST /api/statistics
POST /api/statistics?strategy=basic
POST /api/statistics?strategy=weighted

Return:

- HTTP 200 with the calculated statistics
- HTTP 400 when the requested strategy is unknown
- HTTP 400 when the request body is invalid

Do not add unrelated analytics endpoints.

5. Task API analytics models

Inside TaskFlow.TaskApi, create one compact model file for communication with the Analytics API:

Models/AnalyticsModels.cs

Include only the request and response models required by the Task API.

The frontend must not communicate directly with the Analytics API.

6. Adapter or Gateway Pattern

Inside TaskFlow.TaskApi, create:

Services/IAnalyticsClient.cs
Services/AnalyticsClient.cs

IAnalyticsClient must define an asynchronous operation for calculating statistics.

AnalyticsClient must:

- use HttpClient
- map TaskItem objects to compact analytics task summaries
- send the request as JSON to the Analytics API
- support the selected strategy
- read and return the statistics response
- keep all HTTP communication details outside TaskService and Program.cs
- throw a clear exception when the Analytics API returns an unexpected response

Do not create HttpClient manually with new HttpClient().

7. Analytics API configuration

Add the Analytics API base address to:

backend/TaskFlow.TaskApi/appsettings.json

Use:

http://localhost:5002

Read the address from configuration.

Do not hardcode the complete Analytics API URL inside AnalyticsClient.

8. Dependency Injection in Task API

Register IAnalyticsClient and AnalyticsClient using AddHttpClient.

Configure the HttpClient base address through appsettings.json.

The application logic must depend on IAnalyticsClient and not directly on AnalyticsClient.

9. Distributed dashboard endpoint

Add this endpoint to TaskFlow.TaskApi:

GET /api/dashboard

Support the optional query parameter:

strategy

Supported values:

- basic
- weighted

Default value:

basic

The endpoint must:

1. retrieve all tasks through the existing TaskService
2. send the required task data to the Analytics API through IAnalyticsClient
3. return the statistics result as JSON

Return:

- HTTP 200 when the Analytics API responds successfully
- HTTP 400 when an unknown strategy is requested
- HTTP 503 when the Analytics API cannot be reached or is temporarily unavailable

Use a clear HTTP 503 response message:

"Statistics are temporarily unavailable. Your tasks can still be managed."

The existing task-management endpoints must remain usable when the Analytics API is unavailable.

Do not include HTTP communication logic directly inside the endpoint.

10. Analytics manual request file

Create or update:

backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http

Include example requests for:

- the Analytics API root endpoint
- basic statistics
- weighted statistics
- an unknown strategy

Use:

http://localhost:5002

Keep the existing TaskFlow.TaskApi.http file.

Add requests to the Task API request file for:

- GET /api/dashboard
- GET /api/dashboard?strategy=basic
- GET /api/dashboard?strategy=weighted

11. Manual verification

Build the complete backend solution.

Run TaskFlow.TaskApi and TaskFlow.AnalyticsApi as two separate processes.

Verify:

- both root endpoints respond
- the Analytics API calculates basic statistics
- the Analytics API calculates weighted statistics
- the Task API dashboard endpoint calls the Analytics API successfully
- the Task API returns HTTP 503 when the Analytics API is stopped
- the existing task-management endpoints remain usable while the Analytics API is unavailable

Stop all running API processes after verification.

12. Documentation

Create this German documentation file:

docs/steps/05-analytics-distributed-communication.md

The documentation must include:

- Ziel
- Ausgangssituation
- erstellte Analytics-Modelle
- Strategy Pattern
- BasicStatisticsStrategy
- WeightedStatisticsStrategy
- Dependency Injection
- Adapter beziehungsweise Gateway Pattern
- IAnalyticsClient
- AnalyticsClient
- HTTP- und JSON-Kommunikation
- Dashboard-Endpunkt
- Fehlerbehandlung bei nicht verfügbarer Analytics API
- konfigurierte Adressen und Ports
- manuelle Überprüfung
- Build-Ergebnis
- erstellte oder aktualisierte Dateien
- Ergebnis

Include clickable relative Markdown links to:

- docs/prompts/05-analytics-distributed-communication.md
- docs/steps/04-task-api.md
- docs/steps/03-backend-solution-structure.md
- docs/diagrams/system-architecture.md
- backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http
- backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http

13. README

Update only the project-status and documentation sections of README.md.

The updated status must state that:

- the Analytics API was implemented
- the Strategy Pattern was implemented
- basic and weighted statistics are supported
- the Task API communicates with the Analytics API through HTTP and JSON
- the Analytics Client was registered through Dependency Injection
- the distributed dashboard endpoint was implemented
- the Task API remains usable when the Analytics API is unavailable
- the complete backend solution builds successfully
- Swagger UI will be added in the next backend step

Add a link to:

docs/steps/05-analytics-distributed-communication.md

14. Build verification

Build:

backend/TaskFlow.sln

The complete solution must build successfully without errors.

Important:

- Do not change existing Task API route paths.
- Do not change existing task-management behavior.
- Do not modify the SQLite repository behavior.
- Do not add direct database access to the Analytics API.
- Do not let the frontend communicate directly with the Analytics API.
- Do not add Swagger UI in this step.
- Do not create new application projects.
- Do not modify the frontend directory.
- Do not modify the stitch directory.
- Do not modify existing screenshots.
- Do not modify previous prompt or step documentation.
- Do not add unrelated functionality.
```