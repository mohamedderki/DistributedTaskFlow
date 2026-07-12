# Prompt 04 – Task API Implementation

## Verwendeter Prompt

```text
Project guidelines:

- Keep the work focused on this development step.
- Follow the existing backend architecture and repository structure.
- Write clean and self-documenting code with clear names.
- Keep classes and methods small and focused.
- Avoid unnecessary abstractions and duplicated logic.
- Use asynchronous database operations where appropriate.
- Preserve all existing documentation, screenshots, and Google Stitch outputs.
- Do not modify files outside the requested scope.
- Before editing, briefly describe the planned changes.
- After finishing, list every created or modified file.
- Report the final build result.
- Stop after completing this step.

Task:

Implement the complete task-management functionality inside the existing ASP.NET Core project:

backend/TaskFlow.TaskApi

Use ASP.NET Core Minimal APIs and SQLite.

Keep the existing TaskFlow.AnalyticsApi project unchanged in this step.

The Task API must use:

- Repository Pattern
- Service Layer
- Dependency Injection
- SQLite persistence
- Minimal API endpoints
- clear request models
- clear validation
- HTTP and JSON responses

Use Microsoft.Data.Sqlite directly.

Do not use Entity Framework Core or generated database migrations.

Create only the following structure:

backend/TaskFlow.TaskApi/
├── Models/
│   ├── TaskItem.cs
│   └── TaskRequests.cs
├── Repositories/
│   ├── ITaskRepository.cs
│   └── SqliteTaskRepository.cs
├── Services/
│   └── TaskService.cs
├── Program.cs
├── appsettings.json
└── TaskFlow.TaskApi.http

1. Task model

Create a TaskItem model containing:

- Id
- Title
- Priority
- DueDate
- IsCompleted
- CreatedAtUtc

Create a TaskPriority enum containing:

- Low
- Medium
- High

Configure JSON serialization so enum values are represented as readable strings.

2. Request models

Create compact request models for:

- creating a task
- updating a task

The create and update requests must contain:

- Title
- Priority
- DueDate

Do not expose database-specific details through request models.

3. Validation

Apply the following validation rules:

- Title is required.
- Title is trimmed before storage.
- Title may contain at most 120 characters.
- A due date must be provided.
- Past due dates are allowed because overdue tasks must be supported.
- Invalid requests must return HTTP 400 with a clear message.

Keep the validation logic in the TaskService instead of duplicating it in multiple endpoints.

4. Repository Pattern

Create the interface:

ITaskRepository

It must provide asynchronous operations for:

- retrieving all tasks
- retrieving one task by ID
- adding a task
- updating a task
- deleting a task

Create the implementation:

SqliteTaskRepository

Requirements:

- Use Microsoft.Data.Sqlite.
- Read the connection string from appsettings.json.
- Open a new SQLite connection for each repository operation.
- Create the Tasks table automatically when the repository is initialized.
- Do not create migration files.
- Store dates in a consistent ISO format.
- Store completion status in SQLite in a simple and readable way.
- Keep all SQL access inside the repository implementation.

5. Service Layer

Create:

TaskService

Inject ITaskRepository through constructor injection.

The TaskService must provide operations for:

- retrieving tasks
- filtering tasks by status
- searching tasks by title
- creating a task
- updating a task
- toggling completion status
- deleting a task

Supported status filters:

- all
- open
- completed

Search and status filtering may be combined.

The TaskService must not contain SQL code.

6. Dependency Injection

Register the repository and service in Program.cs.

The TaskService must depend on ITaskRepository and not directly on SqliteTaskRepository.

Configure CORS for the planned Next.js frontend:

http://localhost:3000

Allow the required HTTP methods and headers.

7. API endpoints

Keep the existing root endpoint:

GET /

Expected response:

"TaskFlow Task API is running."

Create a route group under:

/api/tasks

Add the following endpoints:

GET /api/tasks

Optional query parameters:

- status
- search

Examples:

GET /api/tasks
GET /api/tasks?status=open
GET /api/tasks?status=completed
GET /api/tasks?search=architecture
GET /api/tasks?status=open&search=documentation

POST /api/tasks

Creates a task and returns:

- HTTP 201 when successful
- HTTP 400 for invalid input

PUT /api/tasks/{id}

Updates title, priority, and due date.

Return:

- HTTP 200 when successful
- HTTP 400 for invalid input
- HTTP 404 when the task does not exist

PATCH /api/tasks/{id}/toggle

Changes the completion status.

Return:

- HTTP 200 when successful
- HTTP 404 when the task does not exist

DELETE /api/tasks/{id}

Deletes the task.

Return:

- HTTP 204 when successful
- HTTP 404 when the task does not exist

Use clear response behavior and avoid unnecessary endpoint classes.

8. SQLite configuration

Add a connection string to appsettings.json.

Use a local SQLite database file named:

taskflow.db

The runtime database file must remain excluded through the existing .gitignore.

9. Manual request file

Update or create:

backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http

Include example requests for:

- checking the root endpoint
- creating a task
- retrieving all tasks
- filtering open tasks
- searching tasks
- updating a task
- toggling completion
- deleting a task

Use:

http://localhost:5001

10. Documentation

Create the following German documentation file:

docs/steps/04-task-api.md

The documentation must include:

- Ziel
- Ausgangssituation
- erstellte Modelle
- Validierungsregeln
- Repository Pattern
- Service Layer
- Dependency Injection
- SQLite-Persistenz
- konfigurierte CORS-Regel
- implementierte Endpunkte
- HTTP-Statuscodes
- manuelle Überprüfung
- erstellte oder aktualisierte Dateien
- Ergebnis

Include clickable relative Markdown links to:

- docs/prompts/04-task-api.md
- docs/steps/03-backend-solution-structure.md
- docs/diagrams/system-architecture.md
- backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http

11. README

Update only the project-status section of README.md.

The updated status must state that:

- the Task API models were created
- SQLite persistence was implemented
- the Repository Pattern was implemented
- the TaskService was implemented
- Dependency Injection was configured
- the task-management endpoints were implemented
- the Analytics API is not yet connected

12. Build verification

Restore the required NuGet package and build:

backend/TaskFlow.sln

The complete solution must build successfully without errors.

Important:

- Do not modify the TaskFlow.AnalyticsApi project.
- Do not implement statistics calculations.
- Do not create an Analytics HTTP client.
- Do not create a dashboard endpoint.
- Do not modify the frontend directory.
- Do not modify the stitch directory.
- Do not modify existing screenshots.
- Do not modify previous prompt or step files.
- Do not add unrelated functionality.
- Do not create additional projects.
```