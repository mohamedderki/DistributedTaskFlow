# Schritt 01 – UI-Design mit Google Stitch

## Ziel

Vor Beginn der Frontend-Implementierung wurde mit Google Stitch ein visuelles Design für die TaskFlow-Webanwendung erstellt.

In dieser Phase wurden folgende Punkte festgelegt:

- Seitenaufbau
- visuelles Erscheinungsbild
- wiederverwendbare UI-Komponenten
- responsives Verhalten
- verschiedene Zustände der Anwendung

## Verwendetes Werkzeug

Für die Erstellung des Designs wurde **Google Stitch** verwendet.

## Verwendeter Prompt

Der vollständige Prompt befindet sich in der folgenden Datei:

[Google-Stitch-Prompt öffnen](../prompts/01-google-stitch.md)

## Generierte Ansichten

Google Stitch hat folgende Ansichten für die Webanwendung generiert:

- TaskFlow-Hauptdashboard
- Dialog zum Hinzufügen einer Aufgabe
- Leerer Dashboard-Zustand
- Ladezustand
- Fehlerzustand des Statistikdienstes
- TaskFlow-Logo

## Generierte Dateien

Die ursprünglichen Ausgaben von Google Stitch befinden sich im folgenden Ordner:

[Google-Stitch-Ausgaben öffnen](../../stitch/)

Für jede generierte Ansicht wurden normalerweise folgende Dateien erstellt:

- `code.html`
- `DESIGN.md`
- `screen.png`

Für das TaskFlow-Logo wurden folgende Dateien erstellt:

- `DESIGN.md`
- `logo.png`

## Aufbau der Prototypen

Jede Datei mit dem Namen `code.html` stellt einen eigenständigen Webprototyp dar.

Die Datei enthält:

- die HTML-Struktur
- eingebettete CSS-Styles
- eine eingebettete Tailwind-Konfiguration
- den vollständigen visuellen Inhalt der jeweiligen Ansicht

Die generierten HTML-Prototypen können direkt in einem Browser geöffnet und überprüft werden.

## Design-Dokumentation

Für jede generierte Ansicht wurde zusätzlich eine Datei mit dem Namen `DESIGN.md` erstellt.

Diese Dateien dokumentieren unter anderem:

- Farben
- Typografie
- Abstände
- Komponentenstile
- Layout-Struktur
- visuelle Hierarchie
- responsives Verhalten

## Designentscheidung

Die generierten Dateien werden als visuelle und technische Referenz für die spätere Frontend-Implementierung verwendet.

Die Dateien werden nicht unverändert in die produktive Anwendung übernommen.

Das endgültige Frontend wird separat mit folgenden Technologien implementiert:

- Next.js
- React
- JavaScript
- JSX
- Plain CSS

Die HTML-Struktur, Farben, Abstände und Komponentenstile aus den Stitch-Prototypen werden in kleine und wiederverwendbare React-Komponenten übertragen.

Nicht benötigte Styles, generierter Beispielcode und doppelte CSS-Regeln werden nicht in die endgültige Anwendung übernommen.

## Geplante React-Komponenten

Das generierte Design wird später in folgende React-Komponenten aufgeteilt:

- `TaskForm.jsx`
- `TaskList.jsx`
- `TaskCard.jsx`
- `StatisticsPanel.jsx`

Das Hauptdashboard wird in folgender Datei zusammengesetzt:

- `page.jsx`

Die gemeinsamen visuellen Styles werden in folgender Datei definiert:

- `globals.css`

Diese Dateien werden erst während der Frontend-Implementierung erstellt und sind deshalb an dieser Stelle noch nicht verlinkt.

## Screenshots

Die ausgewählten Screenshots befinden sich im folgenden Ordner:

[Alle Stitch-Screenshots öffnen](../screenshots/stitch/)

### Verwendeter Prompt

[Prompt-Screenshot öffnen](../screenshots/stitch/stitch-01-prompt.png)

![Google-Stitch-Prompt](../screenshots/stitch/stitch-01-prompt.png)

### Hauptdashboard

[Dashboard-Screenshot öffnen](../screenshots/stitch/stitch-02-main-dashboard.png)

![TaskFlow-Hauptdashboard](../screenshots/stitch/stitch-02-main-dashboard.png)

### Dialog zum Hinzufügen einer Aufgabe

[Add-Task-Dialog öffnen](../screenshots/stitch/stitch-03-add-task-modal.png)

![Dialog zum Hinzufügen einer Aufgabe](../screenshots/stitch/stitch-03-add-task-modal.png)

### Leerer Zustand

[Empty-State-Screenshot öffnen](../screenshots/stitch/stitch-04-empty-state.png)

![Leerer Dashboard-Zustand](../screenshots/stitch/stitch-04-empty-state.png)

### Ladezustand

[Loading-State-Screenshot öffnen](../screenshots/stitch/stitch-05-loading-state.png)

![Ladezustand](../screenshots/stitch/stitch-05-loading-state.png)

### Fehlerzustand des Statistikdienstes

[Fehlerzustand öffnen](../screenshots/stitch/stitch-06-error-state.png)

![Fehlerzustand des Statistikdienstes](../screenshots/stitch/stitch-06-error-state.png)

### TaskFlow-Logo

[TaskFlow-Logo öffnen](../screenshots/stitch/stitch-07-logo.png)

![TaskFlow-Logo](../screenshots/stitch/stitch-07-logo.png)

## Ergebnis

Das UI-Design wurde vor Beginn der eigentlichen Anwendungsimplementierung erstellt.

Die generierten Prototypen dienen als einheitliche visuelle Referenz für das spätere Next.js-Frontend.

Zusätzlich dokumentieren der gespeicherte Prompt, die HTML-Prototypen, die Design-Dateien und die Screenshots den KI-unterstützten Designprozess nachvollziehbar.