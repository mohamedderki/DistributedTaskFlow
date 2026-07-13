# DistributedTaskFlow

## Projektübersicht

DistributedTaskFlow ist eine browserbasierte Aufgabenverwaltung für eine einzelne Person.

Das Projekt kombiniert ein geplantes Next.js-Frontend mit zwei separat ausführbaren ASP.NET-Core-APIs und einer lokalen SQLite-Datenbank.

Der Fokus liegt auf einer klar getrennten und nachvollziehbaren Architektur für eine kleine verteilte Webanwendung.

## Ziel des Projekts

Ziel ist die Umsetzung einer einfachen TaskFlow-Anwendung, die Aufgaben verwaltet, Aufgabenstatistiken berechnet und die Kommunikation zwischen getrennten Prozessen über HTTP und JSON demonstriert.

Das Projekt dient außerdem zur praktischen Darstellung von Designprinzipien und Design Patterns innerhalb einer modularen Webanwendung.

## Hauptfunktionen

- Aufgaben anzeigen
- Aufgaben erstellen
- Aufgaben bearbeiten
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
- C#
- SQLite
- HTTP
- JSON
- Swagger UI
- OpenAPI
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

Die Anwendung ist als verteiltes System aus mehreren unabhängigen Modulen aufgebaut:

```text
Browser
    |
    v
Next.js Frontend
    |
    v
Task API
    |
    +----> SQLite
    |
    v
Analytics API
```

Die Task API kommuniziert mit SQLite, um Aufgaben dauerhaft zu speichern und zu lesen.

Für die Statistikberechnung sendet die Task API die benötigten Aufgabendaten über HTTP und JSON an die separat laufende Analytics API.

Das Frontend kommuniziert ausschließlich mit der Task API und greift weder direkt auf SQLite noch direkt auf die Analytics API zu.

## Designprinzipien und Design Patterns

Das Projekt demonstriert folgende Prinzipien und Patterns:

- Separation of Concerns
- Single Responsibility Principle
- Dependency Inversion Principle
- Dependency Injection
- Repository Pattern für den Zugriff auf Aufgabendaten
- Service Layer für zentrale Aufgabenlogik und Validierung
- Strategy Pattern für austauschbare Statistikberechnungen
- Adapter beziehungsweise Gateway Pattern für die HTTP-Kommunikation
- verteilte Kommunikation zwischen separaten Prozessen

## Lokale Entwicklungsadressen

| Anwendung | Adresse |
| --- | --- |
| Next.js Frontend | `http://localhost:3000` |
| Task API | `http://localhost:5001` |
| Task API Swagger UI | `http://localhost:5001/swagger` |
| Analytics API | `http://localhost:5002` |
| Analytics API Swagger UI | `http://localhost:5002/swagger` |

## Aktueller Projektstatus

- Schritt 01: Das UI-Design wurde mit Google Stitch erstellt und dokumentiert.
- Schritt 02: Die Projektplanung und die Systemarchitektur wurden dokumentiert.
- Schritt 03: Die Backend-Solution mit zwei getrennten API-Projekten wurde erstellt.
- Schritt 04: Die vollständige Aufgabenverwaltung wurde in der Task API implementiert.
- Schritt 05: Die Analytics API und die verteilte Kommunikation wurden implementiert.
- Schritt 06: Swagger UI und OpenAPI-Dokumentation wurden für beide APIs eingerichtet.
- Die Task API speichert Aufgaben dauerhaft in SQLite.
- Das Repository Pattern wurde für den Datenbankzugriff umgesetzt.
- Der TaskService enthält die zentrale Aufgabenlogik und Validierung.
- Das Strategy Pattern unterstützt Basic- und Weighted-Statistiken.
- Die Task API kommuniziert über einen dedizierten Analytics Client mit der Analytics API.
- Der verteilte Dashboard-Endpunkt `/api/dashboard` wurde implementiert.
- Die Aufgabenverwaltung bleibt nutzbar, wenn die Analytics API nicht erreichbar ist.
- Beide APIs stellen interaktive Swagger-UI-Seiten bereit.
- Die direkten und transitiven NuGet-Pakete wurden auf bekannte Sicherheitslücken geprüft.
- Die vollständige Backend-Solution wurde erfolgreich mit `0 Warnung(en)` und `0 Fehler` gebaut.
- Die Backend-Implementierung ist abgeschlossen.
- Die Frontend-Implementierung ist der nächste Projektschritt.
- Die Next.js-Anwendung wurde noch nicht implementiert.

## Entwicklungsschritte

- [Schritt 01 – UI-Design mit Google Stitch](docs/steps/01-google-stitch-design.md)
- [Schritt 02 – Projektplanung und Architektur](docs/steps/02-project-planning.md)
- [Schritt 03 – Backend-Solution-Struktur](docs/steps/03-backend-solution-structure.md)
- [Schritt 04 – Task API](docs/steps/04-task-api.md)
- [Schritt 05 – Analytics API und verteilte Kommunikation](docs/steps/05-analytics-distributed-communication.md)
- [Schritt 06 – Swagger UI für beide APIs](docs/steps/06-swagger-ui.md)

## Weitere Dokumentation

- [Systemarchitektur](docs/diagrams/system-architecture.md)
- [Gespeicherte Prompts](docs/prompts/)
- [Screenshots](docs/screenshots/)
- [Google-Stitch-Ausgaben](stitch/)