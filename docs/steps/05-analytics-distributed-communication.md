# Schritt 05 - Analytics API und verteilte Kommunikation

## Ziel

Ziel dieses Schritts ist die Implementierung der Analytics API und die Verbindung der bestehenden Task API mit der Analytics API über HTTP und JSON.

Die beiden APIs bleiben unabhängig ausführbare Prozesse:

- `TaskFlow.TaskApi`: `http://localhost:5001`
- `TaskFlow.AnalyticsApi`: `http://localhost:5002`

Relevante Dokumente:

- [docs/prompts/05-analytics-distributed-communication.md](../prompts/05-analytics-distributed-communication.md)
- [docs/steps/04-task-api.md](04-task-api.md)
- [docs/steps/03-backend-solution-structure.md](03-backend-solution-structure.md)
- [docs/diagrams/system-architecture.md](../diagrams/system-architecture.md)
- [backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)
- [backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http)

## Ausgangssituation

Vor diesem Schritt war die Task API bereits mit Aufgabenverwaltung, SQLite-Persistenz, Repository Pattern, Service Layer und Minimal-API-Endpunkten umgesetzt.

Die Analytics API enthielt nur den Root-Endpunkt. Eine Statistikberechnung und eine Verbindung zwischen Task API und Analytics API existierten noch nicht.

## Erstellte Analytics-Modelle

In `TaskFlow.AnalyticsApi` wurde die Datei `Models/StatisticsModels.cs` erstellt.

Sie enthält kompakte Modelle für:

- Statistik-Request
- Task-Summary für die Statistikberechnung
- Statistik-Response
- Prioritätswerte

Die Task-Summary enthält nur:

- `Priority`
- `DueDate`
- `IsCompleted`

Es wurden keine Persistenz- oder Datenbankdetails in die Analytics-Modelle aufgenommen.

## Strategy Pattern

Das Strategy Pattern wurde in `TaskFlow.AnalyticsApi/Strategies/` umgesetzt.

Erstellte Dateien:

- `IStatisticsStrategy.cs`
- `BasicStatisticsStrategy.cs`
- `WeightedStatisticsStrategy.cs`

`IStatisticsStrategy` definiert einen lesbaren Strategienamen und eine Methode zur Berechnung der Statistik.

## BasicStatisticsStrategy

`BasicStatisticsStrategy` berechnet:

- Gesamtzahl der Aufgaben
- offene Aufgaben
- erledigte Aufgaben
- überfällige Aufgaben
- Abschlussquote

`WeightedOpenScore` ist bei dieser Strategie `0`.

Eine Aufgabe gilt als überfällig, wenn sie nicht erledigt ist und ihr Fälligkeitsdatum vor dem aktuellen UTC-Datum liegt.

## WeightedStatisticsStrategy

`WeightedStatisticsStrategy` berechnet dieselben Basiswerte und zusätzlich einen gewichteten Score für offene Aufgaben.

Gewichte:

- `Low` = 1
- `Medium` = 2
- `High` = 3

Nur offene Aufgaben tragen zum `WeightedOpenScore` bei.

Die Basisberechnung wird wiederverwendet, damit die Statistiklogik nicht unnötig dupliziert wird.

## Dependency Injection

In der Analytics API werden beide Strategien über Dependency Injection registriert.

Der Endpoint `POST /api/statistics` erhält die verfügbaren Strategien über DI und erzeugt keine Strategieinstanzen manuell.

In der Task API wird `IAnalyticsClient` über `AddHttpClient` registriert. Die Anwendung hängt von der Abstraktion `IAnalyticsClient` ab und nicht direkt von `AnalyticsClient`.

## Adapter beziehungsweise Gateway Pattern

Die HTTP-Kommunikation zwischen Task API und Analytics API wurde in einem dedizierten Client gekapselt.

Erstellte Dateien in `TaskFlow.TaskApi`:

- `Services/IAnalyticsClient.cs`
- `Services/AnalyticsClient.cs`

Dadurch bleibt die HTTP-Kommunikation außerhalb von `TaskService` und außerhalb der bestehenden Task-Management-Logik.

## IAnalyticsClient

`IAnalyticsClient` definiert eine asynchrone Operation zur Berechnung von Statistiken.

Die Schnittstelle nimmt Aufgaben aus der Task API entgegen und liefert das Statistikmodell zurück, das die Task API an den Dashboard-Endpunkt weitergeben kann.

## AnalyticsClient

`AnalyticsClient` verwendet `HttpClient` und übernimmt:

- Mapping von `TaskItem` auf kompakte Analytics-Task-Summaries
- Versand des Requests als JSON an die Analytics API
- Übergabe der ausgewählten Strategie
- Auslesen der Statistikantwort
- klare Ausnahme bei unerwarteten Antworten der Analytics API

`HttpClient` wird nicht manuell mit `new HttpClient()` erstellt, sondern über `AddHttpClient` verwaltet.

## HTTP- und JSON-Kommunikation

Die Task API liest Aufgaben weiterhin aus SQLite. Für Statistiken sendet sie nur die notwendigen Daten an die Analytics API:

- Priorität
- Fälligkeitsdatum
- Abschlussstatus

Die Kommunikation erfolgt über HTTP und JSON:

```text
TaskFlow.TaskApi
-> HTTP/JSON
-> TaskFlow.AnalyticsApi
```

Das Frontend kommuniziert nicht direkt mit der Analytics API.

## Dashboard-Endpunkt

In der Task API wurde folgender Endpunkt ergänzt:

```text
GET /api/dashboard
```

Optionaler Query-Parameter:

```text
strategy=basic
strategy=weighted
```

Ohne Query-Parameter wird `basic` verwendet.

Der Endpunkt:

1. lädt alle Aufgaben über den bestehenden `TaskService`
2. übergibt die benötigten Task-Daten an `IAnalyticsClient`
3. ruft die Analytics API auf
4. gibt die Statistikantwort als JSON zurück

Die bestehenden Task-Management-Endpunkte unter `/api/tasks` bleiben unverändert.

## Fehlerbehandlung bei nicht verfügbarer Analytics API

Wenn die Analytics API nicht erreichbar ist oder temporär nicht verfügbar ist, liefert der Dashboard-Endpunkt:

```text
HTTP 503
```

Antwortnachricht:

```text
Statistics are temporarily unavailable. Your tasks can still be managed.
```

Die Task-Management-Endpunkte bleiben unabhängig davon nutzbar.

Ein unbekannter Strategiewert im Dashboard-Endpunkt liefert:

```text
HTTP 400
```

## Konfigurierte Adressen und Ports

| Modul | Adresse |
| --- | --- |
| Task API | `http://localhost:5001` |
| Analytics API | `http://localhost:5002` |

Die Basisadresse der Analytics API wurde in `backend/TaskFlow.TaskApi/appsettings.json` konfiguriert:

```text
http://localhost:5002
```

## Manuelle Überprüfung

Die manuelle Überprüfung wurde mit zwei getrennt laufenden API-Prozessen durchgeführt.

Geprüft wurde:

- Root-Endpunkt der Task API antwortet.
- Root-Endpunkt der Analytics API antwortet.
- Analytics API berechnet Basic-Statistiken.
- Analytics API berechnet Weighted-Statistiken.
- Analytics API liefert HTTP 400 für eine unbekannte Strategie.
- Task API ruft die Analytics API über `/api/dashboard` erfolgreich auf.
- Task API liefert HTTP 503, wenn die Analytics API gestoppt ist.
- `/api/tasks` bleibt nutzbar, während die Analytics API nicht verfügbar ist.

Nach der Überprüfung wurden alle gestarteten API-Prozesse gestoppt.

## Build-Ergebnis

Die komplette Backend-Solution wurde gebaut mit:

```text
dotnet build TaskFlow.sln
```

Build-Ergebnis:

```text
Der Buildvorgang wurde erfolgreich ausgeführt.
0 Warnung(en)
0 Fehler
```

## Erstellte oder aktualisierte Dateien

- `backend/TaskFlow.AnalyticsApi/Models/StatisticsModels.cs`
- `backend/TaskFlow.AnalyticsApi/Strategies/IStatisticsStrategy.cs`
- `backend/TaskFlow.AnalyticsApi/Strategies/BasicStatisticsStrategy.cs`
- `backend/TaskFlow.AnalyticsApi/Strategies/WeightedStatisticsStrategy.cs`
- `backend/TaskFlow.AnalyticsApi/Program.cs`
- `backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http`
- `backend/TaskFlow.TaskApi/Models/AnalyticsModels.cs`
- `backend/TaskFlow.TaskApi/Services/IAnalyticsClient.cs`
- `backend/TaskFlow.TaskApi/Services/AnalyticsClient.cs`
- `backend/TaskFlow.TaskApi/Program.cs`
- `backend/TaskFlow.TaskApi/appsettings.json`
- `backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http`
- `docs/steps/05-analytics-distributed-communication.md`
- `README.md`

## Ergebnis

Die Analytics API ist implementiert und berechnet Statistiken mit zwei Strategien. Die Task API kommuniziert über einen dedizierten Analytics Client per HTTP und JSON mit der Analytics API.

Der neue Dashboard-Endpunkt demonstriert die verteilte Kommunikation zwischen zwei separaten API-Prozessen. Die Aufgabenverwaltung bleibt nutzbar, auch wenn die Analytics API nicht verfügbar ist.

Swagger UI wurde in diesem Schritt nicht hinzugefügt.
