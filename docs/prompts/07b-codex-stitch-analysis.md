# Prompt 07b – Analyse der Google-Stitch-Dateien mit Codex CLI

## Verwendeter Prompt

```text
Project context:

DistributedTaskFlow is a distributed task-management application.

The repository already contains:

- backend/
- docs/
- frontend/
- stitch/

The frontend Next.js structure was created previously with Cursor Agent.

This task is analysis only.

Do not implement the frontend in this task.
Do not modify any file inside frontend/.
Do not modify backend/.
Do not modify the Google Stitch source files.

Your only implementation output must be:

docs/frontend/stitch-analysis.md

Before writing the analysis, inspect the repository structure and read all relevant Google Stitch files completely.

Analyze these directories:

1. stitch/TaskFlow Dashboard/
2. stitch/Add Task Modal/
3. stitch/Empty State Dashboard/
4. stitch/Loading State Dashboard/
5. stitch/Statistics Error State/
6. stitch/TaskFlow Logo/

For every available screen directory:

1. Read the complete code.html file.
2. Read the complete DESIGN.md file.
3. Inspect screen.png.
4. Inspect any additional assets.

For the logo directory:

1. Read DESIGN.md.
2. Inspect logo.png.
3. Document how the logo should be used in the frontend.

Do not skip DESIGN.md files.
Do not analyze only screenshots.
Do not analyze only the generated HTML.
The analysis must compare HTML, design documentation, and visual output.

Important:

The Stitch exports may contain duplicated dashboard structures.

Identify:

- the shared application shell
- the canonical dashboard structure
- state-specific differences
- duplicated markup
- duplicated styles
- inconsistencies between screens
- reusable components
- styles that should become shared design tokens

Treat the screens as states of one dashboard application, not as independent Next.js pages, unless the source files clearly prove otherwise.

Create the report in German.

Create:

docs/frontend/stitch-analysis.md

The report must contain the following sections.

# Google-Stitch-Analyse

## 1. Analysierte Dateien

List every inspected file separately.

Use clickable relative Markdown links to:

- every code.html
- every DESIGN.md
- every screen.png
- logo.png
- relevant additional assets

## 2. Übersicht der Screens

For each Stitch screen, explain:

- purpose
- visible sections
- important UI elements
- state represented by the screen
- unique elements compared with the main dashboard
- duplicated elements from other screens

Analyze separately:

- TaskFlow Dashboard
- Add Task Modal
- Empty State Dashboard
- Loading State Dashboard
- Statistics Error State
- TaskFlow Logo

## 3. Gemeinsame Anwendungsstruktur

Identify the shared application shell, including where applicable:

- header
- logo
- sidebar
- navigation
- mobile navigation
- main content
- dashboard header
- statistics area
- progress area
- task toolbar
- task list
- mobile add button

Explain which Stitch screen should be used as the canonical source for the common dashboard structure and why.

## 4. Zustandsabhängige Bereiche

Describe which elements belong only to specific states:

- default dashboard
- add-task modal
- empty state
- loading state
- statistics error state

Explain which shared areas remain visible in each state.

## 5. Komponentenplan

Propose reusable React components.

At minimum, evaluate these components:

Layout:

- DashboardLayout
- Header
- Sidebar

Dashboard:

- DashboardHeader
- StatisticsPanel
- StatisticCard
- ProgressCard
- TaskToolbar
- TaskList
- TaskCard
- MobileAddButton

Modal:

- TaskModal

States:

- EmptyState
- LoadingState
- StatisticsError

For every proposed component, document:

- responsibility
- Stitch source
- reusable or state-specific
- expected child components
- expected props
- related stylesheet

Do not implement these components.

## 6. Design-System und CSS-Tokens

Extract the visual system from code.html, DESIGN.md, and screenshots.

Document:

- colors
- background colors
- text colors
- accent colors
- success colors
- warning colors
- error colors
- borders
- border radii
- shadows
- typography
- font families
- font sizes
- font weights
- line heights
- spacing
- container widths
- sidebar width
- header height
- transitions
- hover states
- focus states

Propose CSS custom properties suitable for:

frontend/styles/tokens.css

Show a proposed token list in a CSS code block, but do not modify tokens.css.

## 7. Responsive Verhalten

Analyze responsive behavior from the Stitch exports.

Document:

- desktop layout
- tablet behavior
- mobile behavior
- sidebar behavior
- task-card behavior
- statistics-card stacking
- toolbar behavior
- modal behavior
- mobile add button
- relevant breakpoints found in the source

Convert Tailwind breakpoint concepts into proposed plain CSS media queries.

## 8. Interaktionen und Animationen

Document all visible or implied interactions:

- add-task action
- modal opening and closing
- task completion toggle
- task editing
- task deletion
- search
- filtering
- strategy selection
- retry action
- focus styles
- hover styles
- loading animations
- transitions

Separate interactions that are present in Stitch from interactions required later by the application.

## 9. Tailwind- und HTML-Konvertierung

Document:

- Tailwind CDN usage
- Tailwind configuration
- utility classes
- embedded style blocks
- external fonts
- icon libraries
- scripts
- duplicated dashboard markup

Explain how each category should be converted into:

- semantic JSX
- React components
- CSS Modules
- globals.css
- tokens.css
- local or framework-supported assets

Do not use Tailwind in the proposed implementation.

## 10. Externe Abhängigkeiten und Assets

List:

- fonts
- icons
- external scripts
- remote images
- local images
- logo assets

Explain which dependencies should be retained, replaced, or avoided.

Do not install packages.

## 11. Inkonsistenzen und Bereinigungen

Compare all Stitch exports and document differences such as:

- inconsistent spacing
- different sidebar or header markup
- differing button styles
- different typography
- repeated IDs
- duplicated full-page shells
- unsupported actions
- conflicting responsive behavior
- inconsistent colors or tokens

For each inconsistency, propose one clear frontend decision.

## 12. Empfohlene Frontend-Struktur

Propose the target structure under frontend/.

Include:

- app/
- components/layout/
- components/dashboard/
- components/modal/
- components/states/
- styles/layout/
- styles/dashboard/
- styles/modal/
- styles/states/
- styles/pages/
- public/

Map each proposed React component to its expected CSS Module.

Do not create or modify these files.

## 13. Zuordnung zwischen Stitch und React

Create a table with:

- React component
- Stitch source directory
- source element or section
- shared or state-specific
- target CSS Module

## 14. Implementierungsreihenfolge

Propose a practical implementation order for the next step.

The order should begin with shared foundations and continue through state-specific components.

Do not implement anything.

## 15. Abgrenzung zum nächsten Schritt

Clearly state that this task produced only the analysis.

The following are not part of this task:

- React implementation
- CSS implementation
- backend integration
- API calls
- application state management
- CRUD functionality

Verification before finishing:

1. Confirm that every code.html was read completely.
2. Confirm that every DESIGN.md was read completely.
3. Confirm that every screen.png and logo.png was inspected.
4. Confirm that frontend/ was not modified.
5. Confirm that backend/ was not modified.
6. Confirm that stitch/ was not modified.
7. Confirm that only docs/frontend/stitch-analysis.md was created or updated.

At the end, report:

- all inspected files
- created documentation file
- any unavailable or unreadable asset
- confirmation that no application code was modified

Stop after completing the analysis.
Do not start frontend implementation.
Do not commit or push changes.
```