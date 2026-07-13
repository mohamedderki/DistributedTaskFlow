# Prompt 08 – Frontend-API-Integration mit Codex CLI

## Verwendeter Prompt

```text
Project context:

DistributedTaskFlow is a distributed task-management application.

The repository contains:

- backend/
- frontend/
- docs/
- stitch/

The backend already contains:

- Task API
- Analytics API
- SQLite persistence
- distributed HTTP communication from Task API to Analytics API
- Swagger/OpenAPI documentation

The frontend already contains:

- Next.js
- React
- JavaScript and JSX
- App Router
- responsive dashboard components
- CSS Modules
- TaskModal
- EmptyState
- LoadingState
- StatisticsError
- static demonstration data

The completed frontend analysis is documented in:

docs/frontend/stitch-analysis.md

The completed frontend implementation is documented in:

docs/steps/07c-codex-frontend-implementation.md

This task connects the existing frontend to the real backend APIs.

Do not redesign the frontend.
Do not recreate the Next.js project.
Do not modify the Google Stitch source files.
Do not commit or push changes.

Before making changes:

1. Inspect the current Git status.
2. Preserve all existing uncommitted frontend implementation changes.
3. Read the backend API controllers, request models, response models, DTOs, and configuration completely.
4. Inspect the Swagger/OpenAPI definitions when useful.
5. Determine the exact JSON property names and request formats from the backend code.
6. Do not invent API contracts.
7. Read the current frontend components and understand their existing props.
8. Reuse the existing UI components and CSS Modules.
9. Confirm that the browser communicates only with the Task API.
10. The frontend must not call the Analytics API directly.

Distributed communication must remain:

Browser
→ Task API
→ Analytics API

The expected Task API endpoints are:

- GET /api/tasks?status=&search=
- POST /api/tasks
- PUT /api/tasks/{id}
- PATCH /api/tasks/{id}/toggle
- DELETE /api/tasks/{id}
- GET /api/dashboard?strategy=basic|weighted

Verify these endpoints and their exact contracts against the backend implementation before using them.

## 1. API configuration

Create:

frontend/.env.example

It must contain:

NEXT_PUBLIC_TASK_API_URL=http://localhost:5001

Do not commit a machine-specific .env.local file.

Create a compact API module such as:

frontend/lib/taskApi.js

Use the existing frontend structure when another suitable location already exists.

The API module must:

- use the native fetch API
- read NEXT_PUBLIC_TASK_API_URL
- use http://localhost:5001 as a development fallback
- centralize request handling
- parse successful JSON responses
- handle responses without a body
- extract useful backend error messages
- throw clear JavaScript errors
- avoid external API-client packages

Provide functions for:

- getTasks
- createTask
- updateTask
- toggleTask
- deleteTask
- getDashboard

Use encodeURIComponent or URLSearchParams for query values.

Do not call the Analytics API directly.

## 2. CORS verification

Inspect the Task API CORS configuration.

The Next.js development frontend runs at:

http://localhost:3000

If CORS is not already configured, add the smallest necessary development configuration to the Task API.

Allow:

- origin http://localhost:3000
- required HTTP methods
- required request headers

Do not use an unrestricted production CORS policy when a specific development origin is sufficient.

Do not change Analytics API CORS unless the browser actually communicates with it, which it should not.

Document every backend file changed for CORS.

## 3. Replace static task data

Update the frontend so that tasks are loaded from the Task API.

Remove the static demonstration tasks from the active application flow.

The page must support:

- initial task loading
- task-list refresh
- status filtering
- title search
- empty result state
- API error display
- loading state

Use the existing Header search field.

Use the existing TaskToolbar only for:

- All
- Open
- Completed

When search or status changes, load matching tasks using:

GET /api/tasks?status=&search=

Use a small debounce for the search field so that every keystroke does not immediately create an unnecessary request.

Keep the implementation simple.

## 4. Dashboard integration

Load dashboard information using:

GET /api/dashboard?strategy=basic

Support both strategies:

- basic
- weighted

Add a compact and accessible strategy selector to an appropriate existing dashboard area.

Preserve the current visual design as much as possible.

The selector may be placed near the progress or statistics area.

Changing the strategy must reload the dashboard data.

Map the actual backend dashboard response to:

- total tasks
- open tasks
- completed tasks
- overdue tasks
- completion percentage
- weighted score or strategy-specific value when returned

Use the exact backend response properties.

Do not calculate backend statistics again in the browser when they are already returned by the API.

## 5. Distributed statistics error handling

The dashboard endpoint depends on the Analytics API.

When the Task API returns a statistics-related failure, including HTTP 503:

- show the existing StatisticsError component
- use the message:

Statistics are temporarily unavailable. Your tasks can still be managed.

- keep the task list usable
- keep create, edit, toggle, delete, search, and filtering usable
- provide a Retry action that reloads only the dashboard statistics
- do not replace the entire application with an error page

A failure loading tasks and a failure loading statistics are separate states.

Do not treat them as the same error.

## 6. Task creation

Connect the existing Add Task actions to TaskModal.

The following actions must open the same modal:

- desktop Add Task button
- MobileAddButton
- EmptyState Create Task button

The page must own the modal open and close state.

TaskModal must:

- open in create mode
- collect the fields required by the actual POST request
- validate required fields
- submit to POST /api/tasks
- show a submitting state
- prevent duplicate submission
- show backend validation errors
- close after successful creation
- refresh tasks
- refresh dashboard statistics

The modal must also close through:

- close icon
- Cancel button
- Escape key
- overlay click outside the dialog

Clicking inside the dialog must not close it.

Do not change the modal design unnecessarily.

## 7. Task editing

Use the existing Edit action.

When Edit is clicked:

- open TaskModal in edit mode
- populate it with the selected task
- use the exact fields supported by PUT /api/tasks/{id}
- submit the update
- display validation errors
- close the modal after success
- refresh tasks
- refresh dashboard statistics

Reuse TaskModal for both create and edit.

Do not create a second edit modal.

Ensure create-mode state does not leak into edit mode and vice versa.

## 8. Task completion toggle

Connect the task completion control to:

PATCH /api/tasks/{id}/toggle

Required behavior:

- disable the control while the request is running
- prevent duplicate requests
- update or refresh the task list after success
- refresh dashboard statistics
- restore a usable state after failure
- display a concise error without breaking the page

Use the actual response returned by the endpoint when useful.

## 9. Task deletion

Connect the Delete action to:

DELETE /api/tasks/{id}

Before deletion, use a simple browser confirmation unless an existing confirmation UI already exists.

After successful deletion:

- refresh tasks
- refresh dashboard statistics
- show EmptyState when no matching tasks remain

Disable the delete action while its request is running.

Do not introduce an additional dialog library.

## 10. Frontend state management

Use React state and hooks only.

Do not install a state-management library.

Keep separate state for:

- tasks
- tasks loading
- tasks error
- dashboard data
- dashboard loading
- dashboard error
- selected strategy
- search text
- selected status
- modal open state
- modal mode
- selected task
- submitting or active mutation state

Avoid duplicated sources of truth.

Keep API logic outside visual components where practical.

Pass data and event handlers through props.

## 11. Existing components

Inspect and update only when required:

- frontend/app/page.jsx
- frontend/components/layout/Header.jsx
- frontend/components/dashboard/DashboardHeader.jsx
- frontend/components/dashboard/StatisticsPanel.jsx
- frontend/components/dashboard/StatisticCard.jsx
- frontend/components/dashboard/ProgressCard.jsx
- frontend/components/dashboard/TaskToolbar.jsx
- frontend/components/dashboard/TaskList.jsx
- frontend/components/dashboard/TaskCard.jsx
- frontend/components/dashboard/MobileAddButton.jsx
- frontend/components/modal/TaskModal.jsx
- frontend/components/states/EmptyState.jsx
- frontend/components/states/LoadingState.jsx
- frontend/components/states/StatisticsError.jsx

Preserve the existing component structure.

Do not merge the full dashboard into one large page component.

Do not duplicate modal, task-card, or statistics markup.

Update CSS Modules only when necessary for:

- loading states
- disabled controls
- API errors
- strategy selector
- submitting state

Do not perform a general visual redesign.

## 12. Client component requirements

Ensure that files using:

- useState
- useEffect
- event handlers
- browser APIs

are valid Next.js client components.

Add "use client" only where required.

Do not turn unrelated layout files into client components without a reason.

## 13. Date and priority mapping

Inspect the backend models before mapping values.

Ensure:

- API dates are displayed clearly
- date input values use the required YYYY-MM-DD format
- priorities match the exact backend-supported values
- completed tasks use the existing completed appearance
- missing optional dates do not crash the UI

Do not rely on locale-specific parsing of ambiguous date strings.

## 14. Error handling

Provide concise user-visible errors for:

- tasks could not be loaded
- task could not be created
- task could not be updated
- task could not be toggled
- task could not be deleted
- statistics are unavailable

Do not expose stack traces or raw technical exception details in the UI.

Keep useful technical details available in the browser console when appropriate.

Clear stale errors after a successful retry or mutation.

## 15. Documentation

Create:

docs/steps/08-frontend-api-integration.md

Write the documentation in German.

It must contain:

# Schritt 08 – Frontend-API-Integration

## Ziel

Explain that the previously implemented Next.js frontend was connected to the real Task API and indirectly to the Analytics API through the distributed backend architecture.

## Architektur

Document:

Browser
→ Task API
→ Analytics API

Clearly state that the browser does not communicate directly with the Analytics API.

## Verwendete Endpunkte

Document the exact verified endpoints and their responsibilities.

## Durchführung

Describe:

1. Reading and verifying backend API contracts.
2. Creating the frontend API module.
3. Configuring the Task API URL.
4. Verifying or adding CORS.
5. Loading tasks.
6. Loading dashboard statistics.
7. Integrating create, edit, toggle, and delete.
8. Integrating search and status filters.
9. Supporting basic and weighted strategies.
10. Handling Analytics API failure while keeping task management usable.

## Implementierte Dateien

Include clickable relative links to:

- the API module
- .env.example
- every modified frontend component
- any modified backend CORS configuration file

## Grundlage

Include clickable links to:

- [Prompt 08 – Frontend-API-Integration](../prompts/08-codex-frontend-api-integration.md)
- [Google-Stitch-Analyse](../frontend/stitch-analysis.md)
- [Schritt 07a – Frontend-Grundstruktur mit Cursor](07a-cursor-frontend-structure.md)
- [Schritt 07b – Stitch-Analyse mit Codex CLI](07b-codex-stitch-analysis.md)
- [Schritt 07c – Frontend-Implementierung mit Codex CLI](07c-codex-frontend-implementation.md)
- [Schritt 05 – Analytics API und verteilte Kommunikation](05-analytics-distributed-communication.md)
- [Schritt 06 – Swagger UI](06-swagger-ui.md)

## Fehlerbehandlung

Document separately:

- task-loading errors
- mutation errors
- statistics errors
- HTTP 503 behavior
- Retry behavior

## Ergebnis

State that:

- real API data replaced static demonstration data
- CRUD operations are connected
- search and filtering use the Task API
- dashboard strategies are connected
- Analytics API failure does not prevent task management

## Screenshot

Include:

- [Codex CLI – Ergebnis der Frontend-API-Integration](../screenshots/codex/codex-08-frontend-api-integration-result.png)

## Abgrenzung

State that final visual corrections, full manual system verification, final screenshots, and delivery review follow in the final step.

## 16. Verification

Run the frontend checks:

cd frontend
npm.cmd run lint
npm.cmd run build

Run or inspect the backend build using the existing solution structure.

Verify the complete local workflow using the correct project commands discovered in the repository.

Check at minimum:

1. Task API and Analytics API start successfully.
2. Frontend starts successfully.
3. Existing tasks load from SQLite.
4. Search works.
5. All, Open, and Completed filters work.
6. Add Task opens the modal and creates a task.
7. Edit loads the selected task and updates it.
8. Toggle changes completion state.
9. Delete removes a task after confirmation.
10. Dashboard basic strategy loads.
11. Dashboard weighted strategy loads.
12. Statistics refresh after task mutations.
13. Stopping Analytics API causes the statistics error state.
14. Task management remains usable while Analytics API is stopped.
15. Restarting Analytics API and pressing Retry restores statistics.
16. No frontend request is sent directly to Analytics API.
17. No Tailwind or TypeScript was introduced.
18. No development process remains running after verification.

Do not delete real project files or reset the database merely for verification.

At the end, report:

- exact API contracts discovered
- all created files
- all modified files
- backend files changed for CORS, if any
- lint result
- frontend build result
- backend build result
- manual integration verification result
- any remaining visual or functional issue
- confirmation that no commit or push was executed

Stop after integration and verification.

Do not perform the final visual cleanup step.
Do not commit or push changes.
```