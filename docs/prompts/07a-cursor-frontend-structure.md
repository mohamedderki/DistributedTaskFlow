# Prompt 07a – Frontend-Grundstruktur mit Cursor

## Verwendeter Prompt

```text
Project context:

DistributedTaskFlow already contains:

- backend/
- docs/
- frontend/
- stitch/

The existing frontend directory is the intended location for the Next.js application.

This task is only for creating the initial frontend project and folder structure.

Do not analyze or use the Google Stitch files in this task.
Do not implement the real dashboard design yet.
Do not integrate the backend APIs yet.

Before making changes:

1. Inspect the repository root.
2. Inspect the current frontend directory.
3. Verify the installed Node.js and npm versions.
4. Confirm that no nested frontend/frontend directory will be created.
5. Do not delete existing files without reporting a conflict first.

Create the Next.js application inside the existing root-level frontend directory.

Use:

- Next.js
- React
- JavaScript
- JSX
- App Router
- npm
- ESLint
- plain CSS
- CSS Modules later
- import alias @/*

Do not use:

- TypeScript
- Tailwind CSS
- Sass
- styled-jsx
- CSS-in-JS libraries
- UI component libraries
- state-management libraries
- API clients
- automated tests
- mock dashboard implementation

Use create-next-app with equivalent explicit options to:

npx create-next-app@latest frontend --js --app --eslint --no-tailwind --no-src-dir --use-npm --import-alias "@/*" --empty --disable-git --no-agents-md --yes

Run the initialization from the repository root.

Do not create another Git repository inside frontend.

After create-next-app finishes:

1. Keep the App Router.
2. Rename app/layout.js to app/layout.jsx when necessary.
3. Rename app/page.js to app/page.jsx when necessary.
4. Move the generated global stylesheet from app/globals.css to styles/globals.css.
5. Update layout.jsx to import ../styles/globals.css.
6. Keep page.jsx minimal and free of dashboard design.
7. Use next.config.mjs, not a TypeScript configuration file.
8. Ensure jsconfig.json configures the @/* import alias.
9. Remove generated example assets and example styles that are not required.
10. Do not add application functionality.

Create this initial structure:

frontend/
├── app/
│   ├── layout.jsx
│   └── page.jsx
├── components/
│   ├── layout/
│   ├── dashboard/
│   ├── modal/
│   └── states/
├── styles/
│   ├── globals.css
│   ├── tokens.css
│   ├── pages/
│   ├── layout/
│   ├── dashboard/
│   ├── modal/
│   └── states/
├── public/
├── eslint.config.mjs
├── jsconfig.json
├── next.config.mjs
├── package.json
└── package-lock.json

Because Git does not track empty directories, add temporary .gitkeep files only to empty component and style directories.

The minimal page may display only:

TaskFlow frontend initialized.

Do not create real components such as Header, Sidebar, TaskCard, TaskModal, or StatisticsPanel in this task.

Do not modify:

- backend/
- stitch/
- existing screenshots
- previous prompts
- previous step documentation
- README.md

Verification:

1. Run npm install if it was not completed by create-next-app.
2. Run npm run build from the frontend directory.
3. The production build must finish successfully.
4. Do not run the development server permanently.
5. Stop all processes after verification.

At the end, report:

- Node.js version
- npm version
- Next.js version
- React version
- exact create-next-app command used
- build result
- complete frontend folder structure
- every created, moved, renamed, or deleted file

Stop after creating and verifying the initial structure.
Do not analyze Stitch.
Do not implement the dashboard.
Do not commit or push changes.
```