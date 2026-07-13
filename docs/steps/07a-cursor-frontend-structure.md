# Schritt 07a – Next.js-Frontend-Grundstruktur mit Cursor

## Ziel

In diesem Schritt wurde die technische Grundstruktur des Frontends mit **Cursor Agent** erstellt.

Cursor wurde als **Visual-Studio-Code-Clone** installiert und praktisch verwendet. Dadurch wird neben dem bereits verwendeten CLI-Werkzeug auch die zweite in der Aufgabenstellung genannte Werkzeugart nachgewiesen.

Der Schwerpunkt dieses Schritts lag ausschließlich auf:

- Initialisierung des Next.js-Projekts
- Vorbereitung der Frontend-Ordnerstruktur
- Einrichtung von React mit JavaScript und JSX
- Vorbereitung der CSS-Struktur
- Überprüfung des Projekts durch einen Production-Build

Das Google-Stitch-Design und die eigentlichen Dashboard-Komponenten werden erst in den folgenden Schritten mit Codex CLI analysiert und umgesetzt.

---

## Verwendetes Werkzeug

- Cursor Editor
- Cursor Agent
- Next.js
- React
- JavaScript
- JSX
- App Router
- npm
- ESLint
- Plain CSS
- CSS Modules als vorgesehene Styling-Struktur

---

## Durchführung

1. Das Repository wurde im Cursor Editor geöffnet.
2. Der vorhandene Projektordner wurde durch Cursor erkannt.
3. Der Cursor Agent erhielt einen abgegrenzten Prompt zur Erstellung der Frontend-Grundstruktur.
4. Das bereits vorhandene Verzeichnis `frontend/` wurde als Zielverzeichnis verwendet.
5. Innerhalb dieses Verzeichnisses wurde ein Next.js-Projekt initialisiert.
6. JavaScript und JSX wurden anstelle von TypeScript verwendet.
7. Tailwind CSS wurde nicht eingerichtet.
8. Die Next.js-Einstiegsdateien wurden als `.jsx`-Dateien vorbereitet.
9. Globale Styles und Design-Tokens wurden in `frontend/styles/` abgelegt.
10. Verzeichnisse für Layout, Dashboard, Modals und verschiedene UI-Zustände wurden erstellt.
11. Leere Verzeichnisse wurden durch `.gitkeep`-Dateien sichtbar gemacht.
12. Die erzeugte Anwendung wurde mit einem Production-Build überprüft.

---

## Erstellte Frontend-Struktur

```text
frontend/
├── app/
│   ├── layout.jsx
│   └── page.jsx
├── components/
│   ├── dashboard/
│   │   └── .gitkeep
│   ├── layout/
│   │   └── .gitkeep
│   ├── modal/
│   │   └── .gitkeep
│   └── states/
│       └── .gitkeep
├── public/
├── styles/
│   ├── dashboard/
│   │   └── .gitkeep
│   ├── layout/
│   │   └── .gitkeep
│   ├── modal/
│   │   └── .gitkeep
│   ├── pages/
│   │   └── .gitkeep
│   ├── states/
│   │   └── .gitkeep
│   ├── globals.css
│   └── tokens.css
├── eslint.config.mjs
├── jsconfig.json
├── next.config.mjs
├── package.json
└── package-lock.json
```

---

## Ergebnis

Die Next.js-Grundstruktur wurde erfolgreich mit **Cursor Agent** erstellt.

Die Startseite enthält in diesem Schritt nur einen einfachen Initialisierungstext:

```text
TaskFlow frontend initialized.
```

Die Anwendung besitzt damit eine funktionsfähige technische Basis für die spätere Umsetzung des Google-Stitch-Designs.

Die eigentlichen React-Komponenten, UI-Zustände und API-Verbindungen wurden in diesem Schritt noch nicht implementiert.

---

## Prompt

Der vollständige Prompt, der dem Cursor Agent übergeben wurde, ist hier dokumentiert:

- [Prompt 07a – Frontend-Grundstruktur mit Cursor](../prompts/07a-cursor-frontend-structure.md)

---

## Screenshots

Die folgenden Screenshots dokumentieren die Installation und praktische Verwendung von Cursor:

- [DistributedTaskFlow im Cursor Editor geöffnet](../screenshots/cursor/cursor-01-project-open.png)
- [Cursor Agent – Ergebnis der Next.js-Initialisierung](../screenshots/cursor/cursor-07a-frontend-structure-result.png)

---

## Erstellte Projektdateien

### Next.js App Router

- [Root Layout](../../frontend/app/layout.jsx)
- [Startseite](../../frontend/app/page.jsx)

### Styles

- [Globale Styles](../../frontend/styles/globals.css)
- [Design Tokens](../../frontend/styles/tokens.css)

### Projektkonfiguration

- [Projekt- und Abhängigkeitskonfiguration](../../frontend/package.json)
- [Abhängigkeitssperrdatei](../../frontend/package-lock.json)
- [JavaScript- und Import-Alias-Konfiguration](../../frontend/jsconfig.json)
- [Next.js-Konfiguration](../../frontend/next.config.mjs)
- [ESLint-Konfiguration](../../frontend/eslint.config.mjs)

---

## Zugehörige Projektdokumentation

### Hauptdokumentation

- [Projektübersicht im README](../../README.md)

### Vorherige Entwicklungsschritte

- [Schritt 01 – Google-Stitch-Design](01-google-stitch.md)
- [Schritt 02 – Projektplanung](02-project-planning.md)
- [Schritt 03 – Backend-Solution-Struktur](03-backend-solution-structure.md)
- [Schritt 04 – Task API](04-task-api.md)
- [Schritt 05 – Analytics API und verteilte Kommunikation](05-analytics-distributed-communication.md)
- [Schritt 06 – Swagger UI](06-swagger-ui.md)

---

## Abgrenzung dieses Schritts

Folgende Funktionen wurden bewusst noch nicht umgesetzt:

- Analyse der Google-Stitch-Dateien
- Umsetzung des Dashboard-Designs
- Header und Sidebar
- Statistik-Karten
- Fortschrittsanzeige
- Aufgabenliste
- Aufgaben-Karten
- Add-Task-Modal
- Empty State
- Loading State
- Statistics Error State
- Verbindung zur Task API
- Verbindung zur Analytics API

Diese Aufgaben werden in den nächsten Schritten getrennt mit **Codex CLI** durchgeführt.

---

## Werkzeugzuordnung

| Werkzeug | Aufgabe |
|---|---|
| Google Stitch | Erstellung und Export des UI-Designs |
| Cursor Agent | Erstellung der Next.js-Grundstruktur |
| Codex CLI | Analyse der Stitch-Dateien und spätere Frontend-Implementierung |

Damit ist die praktische Nutzung eines **Visual-Studio-Code-Clones** durch Cursor dokumentiert.