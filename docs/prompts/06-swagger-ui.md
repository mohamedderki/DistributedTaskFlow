# Prompt 06 – Swagger UI für beide APIs

## Verwendeter Prompt

```text
Project guidelines:

- Keep the work focused on Swagger and OpenAPI documentation.
- Preserve all existing API routes and application behavior.
- Keep the existing distributed communication unchanged.
- Write compact, clean, and self-documenting code.
- Avoid unnecessary helper classes or abstractions.
- Preserve all previous documentation, prompts, screenshots, and design files.
- Do not modify files outside the requested scope.
- Before editing, briefly describe the planned changes.
- After finishing, list every created or modified file.
- Report the package versions, build result, vulnerability check, and manual verification result.
- Stop after completing this step.

Task:

Add OpenAPI documentation and Swagger UI to both existing ASP.NET Core Minimal API projects:

- TaskFlow.TaskApi on http://localhost:5001
- TaskFlow.AnalyticsApi on http://localhost:5002

Swagger must provide interactive documentation and allow the existing endpoints to be executed directly from the browser.

1. Swagger package setup

Add the stable Swashbuckle.AspNetCore package compatible with net10.0 to both projects:

- backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj
- backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.csproj

Use the package API that matches the installed stable version.

For current Swashbuckle versions, use the correct Microsoft.OpenApi namespace.

Do not add multiple competing OpenAPI generators.

Do not add NSwag, Scalar, ReDoc, or unrelated packages.

2. Task API Swagger configuration

Update:

backend/TaskFlow.TaskApi/Program.cs

Register:

- AddEndpointsApiExplorer
- AddSwaggerGen

Create one OpenAPI document:

- document name: v1
- title: TaskFlow Task API
- version: v1
- description: Manages tasks and provides distributed dashboard statistics through the Analytics API.

Enable Swagger middleware only when the application environment is Development.

Expose:

- OpenAPI JSON: /swagger/v1/swagger.json
- Swagger UI: /swagger

Set the Swagger UI document title to:

TaskFlow Task API

Do not change the existing CORS configuration, dependency injection registrations, SQLite repository, routes, response behavior, or Analytics Client.

3. Analytics API Swagger configuration

Update:

backend/TaskFlow.AnalyticsApi/Program.cs

Register:

- AddEndpointsApiExplorer
- AddSwaggerGen

Create one OpenAPI document:

- document name: v1
- title: TaskFlow Analytics API
- version: v1
- description: Calculates basic and weighted statistics for task data received through HTTP and JSON.

Enable Swagger middleware only when the application environment is Development.

Expose:

- OpenAPI JSON: /swagger/v1/swagger.json
- Swagger UI: /swagger

Set the Swagger UI document title to:

TaskFlow Analytics API

Do not change the Strategy Pattern, statistics calculations, dependency injection registrations, existing routes, or response behavior.

4. Task API endpoint documentation

Document the existing Task API endpoints without changing their route handlers.

Use concise OpenAPI metadata such as:

- WithName
- WithTags
- WithSummary
- WithDescription
- Produces
- ProducesProblem where appropriate

Use the following tags:

- System
- Tasks
- Dashboard

Document these endpoints:

GET /

Summary:
Check Task API availability

GET /api/tasks

Summary:
Retrieve tasks

Description:
Returns tasks and optionally filters them by status or searches their titles.

Document the query parameters:

- status: all, open, or completed
- search: optional title search text

Document successful and validation responses.

POST /api/tasks

Summary:
Create a task

Document:

- request body
- HTTP 201
- HTTP 400

PUT /api/tasks/{id}

Summary:
Update a task

Document:

- path parameter id
- request body
- HTTP 200
- HTTP 400
- HTTP 404

PATCH /api/tasks/{id}/toggle

Summary:
Toggle task completion

Document:

- path parameter id
- HTTP 200
- HTTP 404

DELETE /api/tasks/{id}

Summary:
Delete a task

Document:

- path parameter id
- HTTP 204
- HTTP 404

GET /api/dashboard

Summary:
Retrieve distributed dashboard statistics

Description:
Loads tasks from SQLite and requests calculated statistics from the separately running Analytics API.

Document the optional query parameter:

- strategy: basic or weighted

Document:

- HTTP 200
- HTTP 400
- HTTP 503

Keep the existing HTTP 503 response message unchanged.

5. Analytics API endpoint documentation

Document the existing Analytics API endpoints without changing their route handlers.

Use the following tags:

- System
- Statistics

Document:

GET /

Summary:
Check Analytics API availability

POST /api/statistics

Summary:
Calculate task statistics

Description:
Calculates statistics from task summaries using the selected Strategy Pattern implementation.

Document the optional query parameter:

- strategy: basic or weighted
- default: basic

Document:

- request body
- HTTP 200
- HTTP 400

Ensure Swagger displays the request and response schemas for:

- task summaries
- statistics request
- statistics result
- priority enum values

6. Swagger usability

Configure Swagger UI so that:

- both APIs clearly display their own title
- all endpoints are grouped using tags
- endpoint summaries are readable
- request bodies can be edited
- query parameters can be entered
- the Try it out feature works
- response status codes and response bodies are visible
- models and schemas are visible

Keep the configuration small and avoid cosmetic customization that does not improve functionality.

7. Package security verification

After restoring packages, check both projects for vulnerable direct and transitive packages.

Use commands equivalent to:

dotnet package list --project backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj --include-transitive --vulnerable

dotnet package list --project backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.csproj --include-transitive --vulnerable

Do not suppress package vulnerability warnings.

If a vulnerable package is detected, determine the dependency path and apply a compatible package update before continuing.

8. Build verification

Restore and build:

backend/TaskFlow.sln

The complete solution must build successfully with:

- 0 errors
- 0 warnings

9. Manual Swagger verification

Run TaskFlow.TaskApi and TaskFlow.AnalyticsApi as two separate processes.

Verify these pages:

http://localhost:5001/swagger
http://localhost:5002/swagger

From the Task API Swagger UI, verify:

- GET /
- POST /api/tasks
- GET /api/tasks
- GET /api/tasks with status and search
- PUT /api/tasks/{id}
- PATCH /api/tasks/{id}/toggle
- GET /api/dashboard?strategy=basic
- GET /api/dashboard?strategy=weighted
- DELETE /api/tasks/{id}

From the Analytics API Swagger UI, verify:

- GET /
- POST /api/statistics
- POST /api/statistics?strategy=basic
- POST /api/statistics?strategy=weighted
- an unknown strategy returns HTTP 400

Then stop only the Analytics API and verify through the Task API Swagger UI:

- GET /api/dashboard returns HTTP 503
- GET /api/tasks remains usable

Restart the Analytics API and confirm the dashboard endpoint returns HTTP 200 again.

Stop all API processes after verification.

10. Documentation

Create:

docs/steps/06-swagger-ui.md

Write the documentation in German.

Include these sections:

- Ziel
- Ausgangssituation
- verwendete Swagger- und OpenAPI-Pakete
- Swagger-Konfiguration der Task API
- Swagger-Konfiguration der Analytics API
- OpenAPI-Metadaten
- dokumentierte Task-Endpunkte
- dokumentierte Analytics-Endpunkte
- Swagger UI und Try it out
- Überprüfung der verteilten Kommunikation
- Verhalten bei nicht verfügbarer Analytics API
- Sicherheitsprüfung der NuGet-Pakete
- konfigurierte URLs
- manuelle Überprüfung
- Build-Ergebnis
- erstellte oder aktualisierte Dateien
- Ergebnis

Include clickable relative Markdown links to:

- ../prompts/06-swagger-ui.md
- 05-analytics-distributed-communication.md
- 04-task-api.md
- 03-backend-solution-structure.md
- ../diagrams/system-architecture.md
- ../../backend/TaskFlow.TaskApi/Program.cs
- ../../backend/TaskFlow.AnalyticsApi/Program.cs
- ../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http
- ../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http

Add a screenshot section with links to:

- ../screenshots/swagger/swagger-01-task-api.png
- ../screenshots/swagger/swagger-02-analytics-api.png
- ../screenshots/swagger/swagger-03-dashboard-weighted.png

Do not create fake screenshot files.

11. README

Update only the project-status and documentation sections of README.md.

The project status must state that:

- Swagger UI is available for both APIs
- both APIs expose OpenAPI documentation
- all important endpoints are interactively testable
- Task API Swagger is available at http://localhost:5001/swagger
- Analytics API Swagger is available at http://localhost:5002/swagger
- package vulnerability checks were completed
- the complete backend solution builds successfully
- the backend implementation is complete
- frontend implementation is the next project step

Add a link to:

docs/steps/06-swagger-ui.md

12. Scope restrictions

Important:

- Do not change any existing route path.
- Do not change task-management behavior.
- Do not change SQLite persistence.
- Do not change statistics calculations.
- Do not change the Strategy Pattern.
- Do not change Analytics Client communication.
- Do not add new application projects.
- Do not add controllers.
- Do not add authentication.
- Do not modify the frontend directory.
- Do not modify the stitch directory.
- Do not modify existing screenshots.
- Do not modify previous prompt or step documentation.
- Do not create Postman collections.
- Do not add unrelated functionality.
```