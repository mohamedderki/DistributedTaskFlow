# DistributedTaskFlow

## Projektübersicht

DistributedTaskFlow ist eine geplante browserbasierte Aufgabenverwaltung für eine einzelne Person. Das Projekt kombiniert ein Next.js-Frontend mit zwei separaten ASP.NET-Core-APIs und SQLite-Persistenz.

Der Fokus liegt auf einer klar getrennten, nachvollziehbaren Architektur für eine kleine verteilte Webanwendung.

## Ziel des Projekts

Ziel ist die Umsetzung einer einfachen TaskFlow-Anwendung, die Aufgaben verwaltet, Aufgabenstatistiken berechnet und die Kommunikation zwischen getrennten Prozessen über HTTP und JSON demonstriert.

Das Projekt dient außerdem zur praktischen Darstellung von Designprinzipien und Design Patterns in einer modularen Webanwendung.

## Hauptfunktionen

- Aufgaben anzeigen
- Aufgaben erstellen
- Aufgaben als erledigt oder offen markieren
- Aufgaben löschen
- Aufgaben nach Status filtern
- Aufgaben durchsuchen
- Aufgabenstatistiken anzeigen
- Aufgaben dauerhaft in SQLite speichern
- Statistikfehler anzeigen, ohne die Aufgabenverwaltung zu blockieren

## Verwendete Technologien

- Next.js
- React
- JavaScript
- JSX
- Plain CSS
- ASP.NET Core
- SQLite
- HTTP
- JSON
- Mermaid für Architekturdokumentation

## Systemmodule

| Modul | Verantwortung |
| --- | --- |
| Browser | Stellt die Benutzeroberfläche für den Benutzer dar. |
| Next.js Frontend | Rendert die TaskFlow-Oberfläche und kommuniziert mit der Task API. |
| Task API | Verwaltet Aufgaben, stellt Task-Endpunkte bereit und übernimmt die SQLite-Persistenz. |
| Analytics API | Berechnet Aufgabenstatistiken auf Basis der von der Task API bereitgestellten Daten. |
| SQLite | Speichert die Aufgabendaten lokal und dauerhaft. |

## Kurze Architekturübersicht

Die Anwendung ist als verteiltes System aus mehreren unabhängigen Modulen geplant:

```text
Browser
-> Next.js Frontend
-> Task API
-> Analytics API
```

Die Task API kommuniziert zusätzlich mit SQLite, um Aufgaben zu speichern und zu lesen. Die Kommunikation zwischen Frontend, Task API und Analytics API erfolgt über HTTP und JSON.

## Designprinzipien und Design Patterns

Das Projekt demonstriert folgende Prinzipien und Patterns:

- Separation of Concerns
- Single Responsibility Principle
- Dependency Inversion Principle
- Dependency Injection
- Repository Pattern für den Zugriff auf Aufgabendaten
- Strategy Pattern für austauschbare Statistikberechnungen
- Adapter oder Gateway Pattern für HTTP-Kommunikation zwischen Services
- verteilte Kommunikation zwischen separaten Prozessen

## Aktueller Projektstatus

- Schritt 01: Google-Stitch-UI-Design wurde erstellt und dokumentiert.
- Schritt 02: Projektplanung und Systemarchitektur wurden dokumentiert.
- Schritt 03: Die Backend-Solution `backend/TaskFlow.sln` wurde erstellt.
- Das Projekt `TaskFlow.TaskApi` wurde erstellt.
- Das Projekt `TaskFlow.AnalyticsApi` wurde erstellt.
- Beide API-Projekte wurden der Solution hinzugefügt.
- Die lokalen Entwicklungsports wurden konfiguriert: Task API auf `http://localhost:5001`, Analytics API auf `http://localhost:5002`.
- Die vollständige Backend-Solution wurde erfolgreich gebaut.
- Schritt 04: Die Task-API-Modelle wurden erstellt.
- Die SQLite-Persistenz wurde in der Task API mit `Microsoft.Data.Sqlite` implementiert.
- Das Repository Pattern wurde für den Zugriff auf Aufgaben umgesetzt.
- Der `TaskService` wurde als Service Layer mit zentraler Validierung implementiert.
- Dependency Injection wurde für Repository und Service konfiguriert.
- Die Task-Management-Endpunkte unter `/api/tasks` wurden implementiert.
- Schritt 05: Die Analytics API wurde implementiert.
- Das Strategy Pattern wurde für die Statistikberechnung umgesetzt.
- Basic- und Weighted-Statistiken werden unterstützt.
- Die Task API kommuniziert mit der Analytics API über HTTP und JSON.
- Der Analytics Client wurde über Dependency Injection registriert.
- Der verteilte Dashboard-Endpunkt `/api/dashboard` wurde implementiert.
- Die Task API bleibt nutzbar, wenn die Analytics API nicht verfügbar ist.
- Die vollständige Backend-Solution wurde erfolgreich gebaut.
- Schritt 06: Swagger UI ist für beide APIs verfügbar.
- Beide APIs stellen OpenAPI-Dokumentation bereit.
- Alle wichtigen Endpunkte sind interaktiv über Swagger UI testbar.
- Task API Swagger ist unter `http://localhost:5001/swagger` verfügbar.
- Analytics API Swagger ist unter `http://localhost:5002/swagger` verfügbar.
- Die NuGet-Paketprüfung auf bekannte Sicherheitslücken wurde abgeschlossen.
- Die vollständige Backend-Solution wurde erfolgreich mit `0 Warnung(en)` und `0 Fehler` gebaut.
- Die Backend-Implementierung ist abgeschlossen.
- Die Frontend-Implementierung ist der nächste Projektschritt.
- Die Next.js-Anwendung wurde noch nicht erstellt.

## Dokumentation

- [Google-Stitch-Prompt](docs/prompts/01-google-stitch.md)
- [Google-Stitch-Dokumentation](docs/steps/01-google-stitch-design.md)
- [Google-Stitch-Ausgaben](stitch/)
- [Systemarchitektur](docs/diagrams/system-architecture.md)
- [Entwicklungsschritt 01: UI-Design mit Google Stitch](docs/steps/01-google-stitch-design.md)
- [Entwicklungsschritt 02: Projektplanung und Architektur](docs/steps/02-project-planning.md)
- [Entwicklungsschritt 05: Analytics API und verteilte Kommunikation](docs/steps/05-analytics-distributed-communication.md)
- [Entwicklungsschritt 06: Swagger UI für beide APIs](docs/steps/06-swagger-ui.md)
