# Prompt 02 – Project Planning and Architecture

## Verwendeter Prompt

```text
Project guidelines:

- Keep the work focused on this specific step.
- Write clear, concise, and professional documentation.
- Follow the existing repository structure.
- Preserve all existing Google Stitch outputs and screenshots.
- Do not create application source code or install packages in this step.
- Do not create files outside the requested scope.
- Use relative Markdown links where appropriate.
- Before editing, briefly describe the planned changes.
- After finishing, list every created or modified file.
- Stop after completing this step.

Task:

Inspect the existing DistributedTaskFlow repository and create the project-planning and system-architecture documentation.

The planned application consists of the following independent modules:

1. A browser-based frontend built with Next.js, React, JavaScript, JSX, and plain CSS.
2. An ASP.NET Core Task API responsible for task management and SQLite persistence.
3. A separate ASP.NET Core Analytics API responsible for calculating task statistics.
4. HTTP and JSON communication between the modules.

The system flow is:

Browser
→ Next.js Frontend
→ Task API
→ Analytics API

The Task API also communicates with SQLite for task persistence.

The project will demonstrate:

- Separation of Concerns
- Single Responsibility Principle
- Dependency Inversion Principle
- Dependency Injection
- Repository Pattern
- Strategy Pattern
- Adapter or Gateway Pattern for HTTP service communication
- Distributed communication between separate processes

Create or update only these files:

1. README.md

Write the README in German and include:

- Projektübersicht
- Ziel des Projekts
- Hauptfunktionen
- verwendete Technologien
- Systemmodule
- kurze Architekturübersicht
- verwendete Designprinzipien und Design Patterns
- aktueller Projektstatus
- links to the Google Stitch prompt, Stitch documentation, architecture diagram, and development steps

Do not describe features that are not planned.

2. docs/diagrams/system-architecture.md

Write the document in German.

Include:

- a short explanation of the distributed architecture
- a Mermaid flowchart showing:
  - Benutzer
  - Browser
  - Next.js Frontend
  - Task API
  - Analytics API
  - SQLite
- HTTP/JSON communication labels
- database communication
- a short explanation of why the application is considered distributed

3. docs/steps/02-project-planning.md

Write the document in German and include:

- Ziel
- Ausgangssituation
- geplante Module
- Verantwortlichkeit jedes Moduls
- Kommunikationswege
- Designprinzipien
- Design Patterns
- Architekturentscheidung
- erstellte Dateien
- Ergebnis

Important:

- Do not modify the stitch directory.
- Do not modify existing screenshots.
- Do not modify the Google Stitch prompt or Step 01 documentation.
- Do not create the Next.js project.
- Do not create the ASP.NET Core projects.
- Do not add implementation code.
```