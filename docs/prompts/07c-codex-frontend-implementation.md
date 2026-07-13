# Prompt 07c – Frontend-Implementierung mit Codex CLI

## Verwendeter Prompt

```text
Project context:

DistributedTaskFlow is a distributed task-management application.

The repository already contains:

- backend/
- docs/
- frontend/
- stitch/

The Next.js frontend structure was created previously with Cursor Agent.

The Google Stitch exports were analyzed previously with Codex CLI.

The complete analysis is available at:

docs/frontend/stitch-analysis.md

This task is for implementing the frontend user interface based on that analysis.

Do not integrate the backend APIs in this task.
Do not modify backend/.
Do not modify stitch/.
Do not commit or push changes.

Before changing application files:

1. Inspect the existing frontend structure.
2. Read docs/frontend/stitch-analysis.md completely.
3. Read the relevant Google Stitch code.html and DESIGN.md files as implementation references.
4. Inspect the available Stitch screenshots and logo.
5. Preserve the valid Next.js structure created by Cursor.
6. Do not recreate the Next.js project.
7. Do not create a nested frontend/frontend directory.

Use the main TaskFlow Dashboard export as the canonical source for the shared dashboard shell.

Use the other Stitch exports only for their unique state-specific areas:

- Add Task Modal
- Empty State Dashboard
- Loading State Dashboard
- Statistics Error State
- TaskFlow Logo

Treat all screens as states of one dashboard application.

Do not implement them as separate Next.js routes.

Technical requirements:

- Next.js
- React
- JavaScript
- JSX
- App Router
- plain CSS
- CSS Modules
- CSS custom properties
- semantic HTML
- accessible buttons and form fields
- responsive layout

Do not use:

- TypeScript
- Tailwind CSS
- Sass
- styled-jsx
- inline style objects
- CSS-in-JS libraries
- UI component libraries
- state-management libraries
- external API clients
- remote image URLs
- copied Stitch inline scripts
- copied Tailwind CDN configuration

Do not install additional npm packages unless the existing project cannot build without them.

Keep the implementation compact, readable, and free from duplicated markup.

## 1. Design foundations

Update:

- frontend/styles/globals.css
- frontend/styles/tokens.css

Use the Stitch analysis as the source for the design system.

At minimum, define and use CSS custom properties for:

- application background
- card background
- primary color
- strong primary color
- soft primary container
- text color
- muted text color
- border color
- success color
- warning color
- error color
- priority colors
- spacing
- radii
- shadows
- sidebar width
- header height
- transitions

Resolve the primary-color naming clearly:

- --color-primary: #3525cd
- --color-primary-strong: #4f46e5
- --color-primary-container: #e2dfff

Use a system font stack with Inter as the preferred font name.

Do not add remote font imports that could make the production build depend on an external request.

## 2. Logo asset

Copy the local logo from:

stitch/TaskFlow Logo/logo.png

to:

frontend/public/taskflow-logo.png

Use this local asset in the shared application layout.

Do not use remote avatars or remote decorative images.

## 3. Shared layout components

Create:

frontend/components/layout/DashboardLayout.jsx
frontend/components/layout/Header.jsx
frontend/components/layout/Sidebar.jsx

Create matching CSS Modules:

frontend/styles/layout/DashboardLayout.module.css
frontend/styles/layout/Header.module.css
frontend/styles/layout/Sidebar.module.css

Responsibilities:

DashboardLayout:

- shared page shell
- desktop sidebar
- top header
- responsive main content area
- children content

Header:

- compact mobile branding
- one search field
- notification or utility action
- simple user indicator
- accessible labels

Sidebar:

- TaskFlow branding
- navigation
- My Tasks as the active MVP entry
- hide below the desktop breakpoint

Important:

Use only one search field in the application.

The search field belongs in Header.

TaskToolbar must not create a second search field.

## 4. Dashboard components

Create:

frontend/components/dashboard/DashboardHeader.jsx
frontend/components/dashboard/StatisticsPanel.jsx
frontend/components/dashboard/StatisticCard.jsx
frontend/components/dashboard/ProgressCard.jsx
frontend/components/dashboard/TaskToolbar.jsx
frontend/components/dashboard/TaskList.jsx
frontend/components/dashboard/TaskCard.jsx
frontend/components/dashboard/MobileAddButton.jsx

Create matching CSS Modules:

frontend/styles/dashboard/DashboardHeader.module.css
frontend/styles/dashboard/StatisticsPanel.module.css
frontend/styles/dashboard/StatisticCard.module.css
frontend/styles/dashboard/ProgressCard.module.css
frontend/styles/dashboard/TaskToolbar.module.css
frontend/styles/dashboard/TaskList.module.css
frontend/styles/dashboard/TaskCard.module.css
frontend/styles/dashboard/MobileAddButton.module.css

Responsibilities:

DashboardHeader:

- page title
- current dashboard subtitle or date
- desktop Add Task button

StatisticsPanel:

- responsive statistics grid
- StatisticCard rendering
- loading support through props
- error support through props

StatisticCard:

- label
- value
- optional description
- optional visual tone
- loading and disabled appearance

ProgressCard:

- completion percentage
- progress bar
- concise explanatory text

TaskToolbar:

- status filters only
- All
- Open
- Completed

TaskList:

- render TaskCard components
- render EmptyState when no visible tasks exist
- support LoadingState through props

TaskCard:

- completion control
- task title
- priority badge
- due date
- edit action
- delete action
- completed appearance
- responsive mobile layout

MobileAddButton:

- visible below the desktop breakpoint
- opens the task modal
- accessible label

## 5. Modal component

Create:

frontend/components/modal/TaskModal.jsx

Create:

frontend/styles/modal/TaskModal.module.css

The modal must contain:

- overlay
- dialog
- title
- close action
- task title field
- priority selection
- due-date field
- Cancel button
- Save Task button
- accessible labels
- keyboard-friendly focus styles

Use the Add Task Modal Stitch export only for the modal-specific design.

Do not duplicate the full dashboard markup inside the modal component.

The modal may use local React state for displaying and editing form values.

Submitting the form in this step must not call an API.

It may validate the title and then close the modal as a UI demonstration.

Do not persist data.

## 6. State components

Create:

frontend/components/states/EmptyState.jsx
frontend/components/states/LoadingState.jsx
frontend/components/states/StatisticsError.jsx

Create matching CSS Modules:

frontend/styles/states/EmptyState.module.css
frontend/styles/states/LoadingState.module.css
frontend/styles/states/StatisticsError.module.css

EmptyState:

- no-tasks illustration or simple CSS/icon representation
- title
- explanatory text
- Create Task action
- do not implement Import Tasks

LoadingState:

- reusable skeleton rows
- subtle shimmer animation
- accessible loading text

StatisticsError:

- error message
- Retry button
- message that task management remains available

Use the exact core message from the Stitch error state:

Statistics are temporarily unavailable. Your tasks can still be managed.

Do not implement unsupported Stitch actions such as:

- Import Tasks
- Upgrade Plan
- Team management
- Calendar
- Focus Mode
- Pro Tip cards
- unrelated activity widgets

## 7. Main page composition

Update:

frontend/app/page.jsx

The page must use the created React components.

Use a small static sample-task collection only as visual demonstration data.

The default page must show:

- shared layout
- dashboard header
- statistics cards
- progress card
- status toolbar
- task list
- add-task modal
- mobile add button

The page may use lightweight local React state only for:

- opening and closing the modal
- search text
- status filtering
- temporary task completion display
- temporary statistics-error retry display

Do not make HTTP requests.

Do not create API service files.

Do not add persistence.

Do not implement backend integration.

Keep the application ready for API integration in the next step by passing data and event handlers through props.

Use search from Header to filter the static sample tasks.

Use TaskToolbar only for status filtering.

## 8. Root layout

Inspect and update when necessary:

frontend/app/layout.jsx

Requirements:

- correct metadata
- import frontend/styles/globals.css
- valid HTML structure
- no external font dependency
- no dashboard markup inside the root layout

## 9. Responsive behavior

Implement responsive behavior using plain CSS media queries.

Use these layout decisions:

Mobile below 768px:

- hidden sidebar
- compact header
- one-column statistics
- vertically arranged task cards
- compact toolbar
- visible MobileAddButton
- modal with 16px outer spacing

Tablet from 768px to 1023px:

- hidden sidebar
- two-column statistics
- flexible task layout
- compact content margins

Desktop from 1024px:

- visible 256px sidebar
- four-column statistics when space allows
- horizontal task rows
- desktop Add Task button
- hidden MobileAddButton

Large desktop from 1280px:

- centered content with a reasonable maximum width

Avoid horizontal page scrolling.

## 10. Accessibility and interaction

Implement:

- semantic buttons
- visible keyboard focus states
- form labels
- aria-label where visible text is unavailable
- aria-modal on the dialog
- Escape-key closing for the modal
- overlay-click closing where appropriate
- reduced-motion support for shimmer and transitions
- useful disabled and hover states

## 11. Cleanup

Remove temporary .gitkeep files from directories that now contain real files.

Remove generated example files only when they are no longer needed.

Do not modify:

- backend/
- stitch/
- previous screenshots
- previous prompts
- previous step documentation
- root README.md

## 12. Step documentation

Create:

docs/steps/07c-codex-frontend-implementation.md

Write the step documentation in German.

It must contain:

# Schritt 07c – Frontend-Implementierung mit Codex CLI

## Ziel

Explain that the analyzed Google Stitch design was converted into a reusable Next.js and React frontend.

## Verwendetes Werkzeug

Document:

- Codex CLI
- Next.js
- React
- JavaScript and JSX
- CSS Modules
- Google Stitch as design source

## Grundlage

Include clickable relative links to:

- [Prompt 07c – Frontend-Implementierung](../prompts/07c-codex-frontend-implementation.md)
- [Google-Stitch-Analyse](../frontend/stitch-analysis.md)
- [Schritt 07a – Next.js-Grundstruktur mit Cursor](07a-cursor-frontend-structure.md)
- [Schritt 07b – Stitch-Analyse mit Codex CLI](07b-codex-stitch-analysis.md)

## Durchführung

Describe:

1. Reading the completed Stitch analysis.
2. Creating design tokens and global styles.
3. Creating shared layout components.
4. Creating dashboard components.
5. Creating modal and state components.
6. Copying and using the local logo.
7. Implementing responsive behavior.
8. Verifying the production build.

## Implementierte Komponenten

Include clickable links to every created JSX component.

## Styles

Include clickable links to:

- globals.css
- tokens.css
- every created CSS Module

## Ergebnis

Explain:

- the dashboard was implemented as one responsive application
- duplicated Stitch shells were converted into shared React components
- the states were implemented as reusable components
- only static demonstration data is currently used
- no backend integration was performed yet

## Screenshot

Include this link:

- [Codex CLI – Ergebnis der Frontend-Implementierung](../screenshots/codex/codex-07c-frontend-implementation-result.png)

## Abgrenzung

Clearly state that this step does not include:

- Task API integration
- Analytics API integration
- persistent CRUD operations
- production data loading

## Nächster Schritt

State that the next step connects the frontend to:

- Task API
- Analytics API

## 13. Verification

From the frontend directory, run:

npm run lint

Then run:

npm run build

Both commands must finish successfully.

Do not leave the development server running.

Verify:

1. frontend/app/page.jsx uses the new components.
2. All imports resolve correctly.
3. No Tailwind classes remain.
4. No TypeScript files were created.
5. No remote images are used.
6. No backend API requests exist.
7. The application is responsive through CSS media queries.
8. frontend/ contains no unnecessary .gitkeep files in populated directories.
9. backend/ was not modified.
10. stitch/ was not modified.

At the end, report:

- all created files
- all modified files
- all removed .gitkeep files
- copied assets
- lint result
- build result
- confirmation that no API integration was implemented
- confirmation that no commit or push was executed

Stop after implementation and verification.

Do not start API integration.
Do not commit or push changes.
```