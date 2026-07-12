Design a clean, modern, and responsive browser-based web application named “TaskFlow”.

TaskFlow is a simple personal task-management web application for one user.

The application will later be implemented using:

- Next.js
- React
- JavaScript
- JSX
- Plain CSS

The design must represent a web application that runs inside a browser.

Do not design:

- A native Windows or macOS desktop application
- A native mobile application
- Login or registration pages
- User profiles
- Team collaboration
- Notifications
- Settings pages
- Complex charts
- Drag-and-drop functionality
- Social features

Keep the application focused, simple, professional, and easy to implement.

The main application should primarily use one responsive dashboard page.

--------------------------------------------------
1. WEB APPLICATION LAYOUT
--------------------------------------------------

Create a clean responsive web dashboard for desktop browsers, laptop browsers, tablets, and mobile browsers.

The main layout should contain:

- A simple top navigation bar
- The TaskFlow name and logo
- A short dashboard title
- A short welcome or summary text
- A search field
- A primary “Add Task” button
- A statistics section
- Task filters
- A task list

Do not create a large or complex sidebar.

A small navigation area is acceptable, but the interface should remain simple and compact.

--------------------------------------------------
2. HEADER
--------------------------------------------------

The header should contain:

- A simple TaskFlow logo
- The application name “TaskFlow”
- A page title such as “My Tasks”
- A short subtitle such as “Manage your daily tasks and track your progress”
- A search input
- A clearly visible “Add Task” button

The “Add Task” button should be the main primary action.

--------------------------------------------------
3. STATISTICS SECTION
--------------------------------------------------

Create a clean statistics section with simple cards.

Display the following information:

- Total Tasks
- Open Tasks
- Completed Tasks
- Overdue Tasks
- Completion Percentage

Use small reusable statistic cards.

Each card should contain:

- A short label
- A large number
- A simple minimal icon

The completion percentage may include a simple progress bar.

Do not use:

- Complex charts
- Pie charts
- Bar chart libraries
- Large visualizations
- Decorative graphs

The statistics section must be easy to reproduce using React components and plain CSS.

--------------------------------------------------
4. TASK FILTERS
--------------------------------------------------

Create three clear task filters:

- All
- Open
- Completed

The selected filter should be visually recognizable.

Use simple tabs, segmented buttons, or compact filter buttons.

Do not add advanced filtering or unnecessary options.

--------------------------------------------------
5. TASK LIST
--------------------------------------------------

Create a list of task cards.

Each task card should display:

- Task title
- Priority
- Due date
- Completion status
- A button to complete or reopen the task
- A delete button

Priority values are:

- Low
- Medium
- High

Use subtle visual indicators for priorities.

For example:

- Low priority: calm neutral indicator
- Medium priority: warm indicator
- High priority: stronger warning indicator

Do not use too many colors.

Completed tasks should look visually different from open tasks.

For example:

- A completed icon
- Slightly muted text
- A subtle line through the task title

The completed task must remain readable.

Use realistic example tasks such as:

- Prepare software architecture diagram
- Complete project documentation
- Review distributed service communication
- Design the TaskFlow dashboard
- Submit the final repository

--------------------------------------------------
6. ADD TASK FORM
--------------------------------------------------

Create an “Add Task” form inside a modal or a compact panel within the web application.

The form should contain only:

- Task title input
- Priority selection
- Due date input
- Save button
- Cancel button
- A small validation message area

Priority options:

- Low
- Medium
- High

The form should be simple and easy to understand.

Do not include:

- Task descriptions
- Attachments
- Tags
- Users
- Categories
- Notifications
- Recurring tasks
- Unnecessary form fields

The modal should visually match the dashboard.

--------------------------------------------------
7. NORMAL STATE
--------------------------------------------------

Create a normal dashboard state containing several example tasks.

Include:

- Open tasks
- Completed tasks
- Different priorities
- Different due dates
- Updated statistics cards

The screen should look like a realistic working task-management web application.

--------------------------------------------------
8. EMPTY STATE
--------------------------------------------------

Create a separate empty-state version of the dashboard.

When no tasks exist, display:

- A simple minimal icon
- A friendly title such as “No tasks yet”
- A short message such as “Create your first task to start organizing your work”
- A primary “Create Task” button

Avoid large illustrations that would be difficult to reproduce.

--------------------------------------------------
9. LOADING STATE
--------------------------------------------------

Create a simple loading state for the task list.

Use one of the following:

- Simple skeleton task cards
- A small loading indicator
- A short “Loading tasks...” message

Do not use complex animations.

--------------------------------------------------
10. STATISTICS SERVICE ERROR STATE
--------------------------------------------------

Create a separate dashboard state where the statistics service is temporarily unavailable.

The task list should remain visible and usable.

Show a clear but calm error message near the statistics section.

Use this message:

“Statistics are temporarily unavailable. Your tasks can still be managed.”

Include a small “Retry” button.

Do not display a full-page error.

The design should communicate that only the statistics service is unavailable, while task management still works.

--------------------------------------------------
11. RESPONSIVE WEB DESIGN
--------------------------------------------------

Create responsive layouts for:

- Large desktop browser viewport
- Laptop browser viewport
- Tablet browser viewport
- Mobile browser viewport

For mobile browsers:

- Stack statistic cards in a compact grid
- Keep task cards readable
- Keep buttons large enough to tap
- Avoid horizontal scrolling
- Move the search field below the header if necessary
- Keep the Add Task button clearly visible
- Stack task-card actions when space is limited
- Keep the interface simple

The mobile layout must still look like the same browser-based web application.

--------------------------------------------------
12. VISUAL STYLE
--------------------------------------------------

Use a professional and modern SaaS web-application style.

The visual design should include:

- White or very light neutral background
- One calm primary accent color
- Clean spacing
- Strong visual hierarchy
- Subtle borders
- Very subtle shadows
- Consistent rounded corners
- Accessible color contrast
- Minimal icons
- Clear button hierarchy
- Reusable card styles
- Consistent form elements
- Professional typography

The design should feel:

- Clean
- Modern
- Calm
- Professional
- Easy to understand
- Easy to implement

Avoid:

- Gradients
- Glassmorphism
- Excessive animations
- Excessively rounded components
- Too many colors
- Decorative illustrations
- Complex visual effects
- Visual clutter
- Oversized elements
- Complex navigation

--------------------------------------------------
13. TECHNICAL DESIGN CONSTRAINTS
--------------------------------------------------

The design must be practical to reproduce using:

- Next.js
- React
- JavaScript
- JSX
- Plain CSS

Design reusable visual components for:

- Statistic cards
- Task cards
- Filter buttons
- Form inputs
- Primary and secondary buttons
- Error messages
- Empty states

Do not create design elements that require:

- TypeScript
- UI component libraries
- Chart libraries
- Animation libraries
- Drag-and-drop libraries
- Advanced graphical effects

Keep the design implementation-friendly and suitable for a small software-engineering project.

--------------------------------------------------
14. SCREENS TO GENERATE
--------------------------------------------------

Generate the following browser-based web application screens:

1. Main TaskFlow dashboard with several example tasks
2. Add Task modal or form
3. Empty-state dashboard
4. Loading-state dashboard
5. Statistics-service error state
6. Mobile-responsive dashboard

Use the same visual design system across all screens.