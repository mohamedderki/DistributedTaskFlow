# Schritt 06 - Swagger UI für beide APIs

## Ziel

Ziel dieses Schritts ist die Ergänzung von OpenAPI-Dokumentation und Swagger UI für beide bestehenden ASP.NET-Core-Minimal-API-Projekte.

Die bestehenden API-Routen, die Aufgabenverwaltung, die SQLite-Persistenz, die Statistikberechnung und die verteilte Kommunikation zwischen Task API und Analytics API bleiben unverändert.

Relevante Dokumente und Dateien:

- [Prompt 06](../prompts/06-swagger-ui.md)
- [Schritt 05 - Analytics API und verteilte Kommunikation](05-analytics-distributed-communication.md)
- [Schritt 04 - Task API](04-task-api.md)
- [Schritt 03 - Backend-Solution-Struktur](03-backend-solution-structure.md)
- [Systemarchitektur](../diagrams/system-architecture.md)
- [Task API Program.cs](../../backend/TaskFlow.TaskApi/Program.cs)
- [Analytics API Program.cs](../../backend/TaskFlow.AnalyticsApi/Program.cs)
- [Task API HTTP-Beispiele](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)
- [Analytics API HTTP-Beispiele](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http)

## Ausgangssituation

Vor diesem Schritt waren beide APIs funktional umgesetzt:

- `TaskFlow.TaskApi` verwaltet Aufgaben, speichert sie in SQLite und stellt den Dashboard-Endpunkt bereit.
- `TaskFlow.AnalyticsApi` berechnet Basic- und Weighted-Statistiken.
- Die Task API ruft die Analytics API über HTTP und JSON auf.

Swagger UI und OpenAPI-Dokumentation waren noch nicht eingerichtet.

## Verwendete Swagger- und OpenAPI-Pakete

In beiden API-Projekten wurde das Paket installiert:

- `Swashbuckle.AspNetCore` Version `10.2.3`

Das Paket verwendet transitiv:

- `Microsoft.OpenApi` Version `2.7.5`

Für die OpenAPI-Konfiguration wird deshalb der aktuelle Namespace `Microsoft.OpenApi` verwendet.

Es wurden keine konkurrierenden OpenAPI-Generatoren wie NSwag, Scalar oder ReDoc hinzugefügt.

## Swagger-Konfiguration der Task API

In `backend/TaskFlow.TaskApi/Program.cs` wurden registriert:

- `AddEndpointsApiExplorer`
- `AddSwaggerGen`

Das OpenAPI-Dokument:

- Dokumentname: `v1`
- Titel: `TaskFlow Task API`
- Version: `v1`
- Beschreibung: `Manages tasks and provides distributed dashboard statistics through the Analytics API.`

Swagger wird nur in der `Development`-Umgebung aktiviert.

Verfügbare URLs:

- OpenAPI JSON: `http://localhost:5001/swagger/v1/swagger.json`
- Swagger UI: `http://localhost:5001/swagger`

Der Swagger-UI-Titel lautet:

```text
TaskFlow Task API
```

## Swagger-Konfiguration der Analytics API

In `backend/TaskFlow.AnalyticsApi/Program.cs` wurden registriert:

- `AddEndpointsApiExplorer`
- `AddSwaggerGen`

Das OpenAPI-Dokument:

- Dokumentname: `v1`
- Titel: `TaskFlow Analytics API`
- Version: `v1`
- Beschreibung: `Calculates basic and weighted statistics for task data received through HTTP and JSON.`

Swagger wird nur in der `Development`-Umgebung aktiviert.

Verfügbare URLs:

- OpenAPI JSON: `http://localhost:5002/swagger/v1/swagger.json`
- Swagger UI: `http://localhost:5002/swagger`

Der Swagger-UI-Titel lautet:

```text
TaskFlow Analytics API
```

## OpenAPI-Metadaten

Die vorhandenen Minimal-API-Endpunkte wurden mit kompakten OpenAPI-Metadaten dokumentiert.

Verwendet wurden unter anderem:

- `WithName`
- `WithTags`
- `WithSummary`
- `WithDescription`
- `Accepts`
- `Produces`

Die Endpunkte sind in Swagger UI nach Tags gruppiert:

- `System`
- `Tasks`
- `Dashboard`
- `Statistics`

Request-Body-Schemas, Query-Parameter, Pfadparameter, Response-Statuscodes und Response-Schemas werden in der OpenAPI-Dokumentation angezeigt.

## Dokumentierte Task-Endpunkte

Die Task API dokumentiert:

| Methode | Route | Zweck |
| --- | --- | --- |
| GET | `/` | Verfügbarkeit der Task API prüfen |
| GET | `/api/tasks` | Aufgaben abrufen, optional mit `status` und `search` |
| POST | `/api/tasks` | Aufgabe erstellen |
| PUT | `/api/tasks/{id}` | Aufgabe aktualisieren |
| PATCH | `/api/tasks/{id}/toggle` | Abschlussstatus umschalten |
| DELETE | `/api/tasks/{id}` | Aufgabe löschen |
| GET | `/api/dashboard` | Verteilte Dashboard-Statistiken abrufen |

Für `/api/tasks` werden die Query-Parameter `status` und `search` dokumentiert.

Für `/api/dashboard` wird der optionale Query-Parameter `strategy` dokumentiert. Unterstützt werden:

- `basic`
- `weighted`

## Dokumentierte Analytics-Endpunkte

Die Analytics API dokumentiert:

| Methode | Route | Zweck |
| --- | --- | --- |
| GET | `/` | Verfügbarkeit der Analytics API prüfen |
| POST | `/api/statistics` | Aufgabenstatistiken berechnen |

Für `/api/statistics` wird der optionale Query-Parameter `strategy` dokumentiert. Unterstützt werden:

- `basic`
- `weighted`

Ohne Query-Parameter wird `basic` verwendet.

Die Swagger-Schemas zeigen:

- Task-Summaries
- Statistik-Request
- Statistik-Result
- Prioritäts-Enumwerte

## Swagger UI und Try it out

Swagger UI ist für beide APIs verfügbar und zeigt:

- API-Titel
- gruppierte Endpunkte
- lesbare Zusammenfassungen
- editierbare Request-Bodies
- eintragbare Query-Parameter
- Statuscodes
- Response-Bodies
- Modelle und Schemas

Die Browser-Automation der Codex-Sitzung war nicht verfügbar. Deshalb wurde die UI-Erreichbarkeit über die Swagger-HTML-Seiten und die OpenAPI-JSON-Dokumente geprüft. Die Endpunkte, die über `Try it out` ausgeführt würden, wurden gegen die laufenden APIs per HTTP verifiziert.

## Überprüfung der verteilten Kommunikation

Die verteilte Kommunikation wurde über den Task-API-Endpunkt geprüft:

```text
GET /api/dashboard?strategy=basic
GET /api/dashboard?strategy=weighted
```

Dabei lädt die Task API Aufgaben aus SQLite und ruft die separat laufende Analytics API über HTTP und JSON auf.

Beide Varianten lieferten erfolgreich HTTP 200.

## Verhalten bei nicht verfügbarer Analytics API

Nach dem Stoppen der Analytics API wurde geprüft:

```text
GET /api/dashboard
```

Die Task API lieferte erwartungsgemäß:

```text
HTTP 503
```

Die Antwortnachricht blieb unverändert:

```text
Statistics are temporarily unavailable. Your tasks can still be managed.
```

Zusätzlich wurde geprüft, dass `GET /api/tasks` weiterhin HTTP 200 liefert.

Nach dem Neustart der Analytics API lieferte der Dashboard-Endpunkt wieder HTTP 200.

## Sicherheitsprüfung der NuGet-Pakete

Die Paketprüfung wurde für direkte und transitive Abhängigkeiten ausgeführt:

```text
dotnet package list --project backend\TaskFlow.TaskApi\TaskFlow.TaskApi.csproj --include-transitive --vulnerable
dotnet package list --project backend\TaskFlow.AnalyticsApi\TaskFlow.AnalyticsApi.csproj --include-transitive --vulnerable
```

Ergebnis:

- Für `TaskFlow.TaskApi` wurden keine anfälligen Pakete gefunden.
- Für `TaskFlow.AnalyticsApi` wurden keine anfälligen Pakete gefunden.

Es wurden keine Paketwarnungen unterdrückt.

## Konfigurierte URLs

| API | Swagger UI | OpenAPI JSON |
| --- | --- | --- |
| Task API | `http://localhost:5001/swagger` | `http://localhost:5001/swagger/v1/swagger.json` |
| Analytics API | `http://localhost:5002/swagger` | `http://localhost:5002/swagger/v1/swagger.json` |

## Manuelle Überprüfung

Die manuelle Überprüfung wurde mit zwei getrennt laufenden API-Prozessen durchgeführt.

Geprüft wurde für die Task API:

- `GET /`
- `POST /api/tasks`
- `GET /api/tasks`
- `GET /api/tasks?status=open&search=Swagger`
- `PUT /api/tasks/{id}`
- `PATCH /api/tasks/{id}/toggle`
- `GET /api/dashboard?strategy=basic`
- `GET /api/dashboard?strategy=weighted`
- `DELETE /api/tasks/{id}`

Geprüft wurde für die Analytics API:

- `GET /`
- `POST /api/statistics`
- `POST /api/statistics?strategy=basic`
- `POST /api/statistics?strategy=weighted`
- unbekannte Strategie mit HTTP 400

Zusätzlich wurde geprüft:

- Task API Swagger UI ist erreichbar.
- Analytics API Swagger UI ist erreichbar.
- beide OpenAPI-JSON-Dokumente sind erreichbar.
- Dashboard liefert HTTP 503, wenn die Analytics API gestoppt ist.
- Aufgabenverwaltung bleibt nutzbar, wenn die Analytics API gestoppt ist.
- Dashboard liefert nach Neustart der Analytics API wieder HTTP 200.

Nach der Überprüfung wurden alle gestarteten API-Prozesse gestoppt.

## Build-Ergebnis

Die komplette Backend-Solution wurde gebaut:

```text
dotnet build TaskFlow.sln
```

Build-Ergebnis:

```text
Der Buildvorgang wurde erfolgreich ausgeführt.
0 Warnung(en)
0 Fehler
```

## Screenshots

Die folgenden Screenshot-Pfade sind für die spätere visuelle Dokumentation vorgesehen:

- [Task API Swagger UI](../screenshots/swagger/swagger-01-task-api.png)
- [Analytics API Swagger UI](../screenshots/swagger/swagger-02-analytics-api.png)
- [Dashboard Weighted Swagger Request](../screenshots/swagger/swagger-03-dashboard-weighted.png)

In diesem Schritt wurden keine Screenshot-Dateien künstlich erstellt.

## Erstellte oder aktualisierte Dateien

- `backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj`
- `backend/TaskFlow.TaskApi/Program.cs`
- `backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.csproj`
- `backend/TaskFlow.AnalyticsApi/Program.cs`
- `docs/steps/06-swagger-ui.md`
- `README.md`

## Ergebnis

Swagger UI und OpenAPI-Dokumentation sind für beide APIs eingerichtet. Beide APIs zeigen ihre eigenen Titel, dokumentieren ihre Endpunkte mit Tags und Metadaten und stellen interaktive Dokumentation unter den lokalen Swagger-URLs bereit.

Die Backend-Implementierung ist damit abgeschlossen. Der nächste Projektschritt ist die Frontend-Implementierung.
