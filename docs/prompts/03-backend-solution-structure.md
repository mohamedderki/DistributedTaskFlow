# Prompt 03 – Backend Solution Structure

## Verwendeter Prompt

```text
Project guidelines:

- Keep the work focused on this specific development step.
- Follow the existing repository structure.
- Use clear, concise, and self-documenting names.
- Keep the backend structure minimal and understandable.
- Preserve all existing documentation, screenshots, and Google Stitch outputs.
- Do not modify files outside the requested scope.
- Do not implement application features in this step.
- Before making changes, briefly describe the planned actions.
- After finishing, list every created or modified file.
- Report the final build result.
- Stop after completing this step.

Task:

Inspect the existing DistributedTaskFlow repository and create the initial ASP.NET Core backend solution structure.

Create the following structure inside the existing backend directory:

backend/
├── TaskFlow.sln
├── TaskFlow.TaskApi/
└── TaskFlow.AnalyticsApi/

Requirements:

1. Check the installed .NET SDK version before creating the projects.

2. Create a classic solution file named:

TaskFlow.sln

3. Create a minimal ASP.NET Core Web API project named:

TaskFlow.TaskApi

The Task API will later be responsible for:

- task management
- input validation
- SQLite persistence
- communication with the Analytics API

Do not implement these responsibilities in this step.

4. Create a second minimal ASP.NET Core Web API project named:

TaskFlow.AnalyticsApi

The Analytics API will later be responsible for calculating task statistics.

Do not implement statistics logic in this step.

5. Add both API projects to TaskFlow.sln.

6. Remove generated template functionality such as:

- WeatherForecast models
- WeatherForecast endpoints
- example API code

7. Keep both Program.cs files minimal.

Each API should currently provide only a simple root endpoint that identifies the running service.

Expected responses:

TaskFlow.TaskApi:
"TaskFlow Task API is running."

TaskFlow.AnalyticsApi:
"TaskFlow Analytics API is running."

Do not add business endpoints.

8. Configure the local HTTP development ports:

- TaskFlow.TaskApi: http://localhost:5001
- TaskFlow.AnalyticsApi: http://localhost:5002

Use the launchSettings.json files for the local port configuration.

HTTPS configuration is not required for this project step.

9. Do not create:

- task models
- request models
- repositories
- services
- SQLite database access
- Entity Framework configuration
- analytics models
- statistics strategies
- HTTP clients
- task-management endpoints
- dashboard endpoints
- frontend files
- additional projects

10. Build the complete backend solution.

Use the solution file and verify that the build completes successfully without errors.

11. Create the following German documentation file:

docs/steps/03-backend-solution-structure.md

The documentation must include:

- Ziel
- Ausgangssituation
- verwendete Technologie
- installierte .NET-SDK-Version
- erstellte Solution
- erstellte API-Projekte
- geplante Verantwortung der Task API
- geplante Verantwortung der Analytics API
- konfigurierte Ports
- entfernte Template-Inhalte
- ausgeführte Build-Prüfung
- erstellte oder aktualisierte Dateien
- Ergebnis

Include clickable relative Markdown links to:

- docs/prompts/03-backend-solution-structure.md
- docs/diagrams/system-architecture.md
- docs/steps/02-project-planning.md

12. Update only the project-status section of README.md.

The updated status must state that:

- the backend solution was created
- the Task API project was created
- the Analytics API project was created
- both projects were added to the solution
- the local development ports were configured
- the complete solution builds successfully
- no application features have been implemented yet

Important:

- Do not modify the stitch directory.
- Do not modify existing screenshots.
- Do not modify previous prompt files.
- Do not modify Step 01 or Step 02 documentation.
- Do not create or modify the Next.js frontend.
- Do not add task-management functionality.
- Do not add statistics functionality.
- Do not create files outside the requested scope.
```