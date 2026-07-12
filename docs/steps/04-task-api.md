# Schritt 04 - Task API

## Ziel

Ziel dieses Schritts ist die Implementierung der Aufgabenverwaltung in der bestehenden ASP.NET-Core-Minimal-API `TaskFlow.TaskApi`.

Die Task API erhält Modelle, Request-Modelle, Validierung, SQLite-Persistenz, Repository Pattern, Service Layer, Dependency Injection und HTTP-Endpunkte für Aufgaben.

Relevante Dokumente:

- [docs/prompts/04-task-api.md](../prompts/04-task-api.md)
- [docs/steps/03-backend-solution-structure.md](03-backend-solution-structure.md)
- [docs/diagrams/system-architecture.md](../diagrams/system-architecture.md)
- [backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

## Ausgangssituation

Vor diesem Schritt bestand `TaskFlow.TaskApi` nur aus einem minimalen Root-Endpunkt. Die Backend-Solution und das separate Projekt `TaskFlow.AnalyticsApi` waren bereits vorhanden.

In diesem Schritt wurde ausschließlich das Projekt `TaskFlow.TaskApi` funktional erweitert. Die Analytics API wurde nicht geändert und noch nicht angebunden.

## Erstellte Modelle

Es wurden folgende Modelle erstellt:

- `TaskItem`
- `TaskPriority`
- `CreateTaskRequest`
- `UpdateTaskRequest`

`TaskItem` enthält:

- `Id`
- `Title`
- `Priority`
- `DueDate`
- `IsCompleted`
- `CreatedAtUtc`

`TaskPriority` enthält die Werte:

- `Low`
- `Medium`
- `High`

Die JSON-Serialisierung wurde so konfiguriert, dass Enum-Werte als lesbare Strings ausgegeben und angenommen werden.

## Validierungsregeln

Die Validierung erfolgt zentral im `TaskService`.

Regeln:

- `Title` ist erforderlich.
- `Title` wird vor dem Speichern getrimmt.
- `Title` darf maximal 120 Zeichen enthalten.
- `Priority` muss einen gültigen Wert enthalten.
- `DueDate` ist erforderlich.
- Vergangene Fälligkeitsdaten sind erlaubt, damit überfällige Aufgaben unterstützt werden.
- Ungültige Anfragen liefern HTTP 400 mit einer klaren Fehlermeldung.

## Repository Pattern

Das Repository Pattern wurde mit folgenden Dateien umgesetzt:

- `Repositories/ITaskRepository.cs`
- `Repositories/SqliteTaskRepository.cs`

`ITaskRepository` definiert asynchrone Operationen für:

- alle Aufgaben abrufen
- eine Aufgabe nach ID abrufen
- Aufgabe hinzufügen
- Aufgabe aktualisieren
- Aufgabe löschen

`SqliteTaskRepository` enthält den vollständigen SQL-Zugriff. Andere Schichten enthalten keinen SQL-Code.

## Service Layer

Der `TaskService` kapselt die fachliche Aufgabenlogik.

Er stellt Operationen bereit für:

- Aufgaben abrufen
- Aufgaben nach Status filtern
- Aufgaben nach Titel durchsuchen
- Aufgaben erstellen
- Aufgaben aktualisieren
- Abschlussstatus umschalten
- Aufgaben löschen

Unterstützte Statusfilter:

- `all`
- `open`
- `completed`

Statusfilter und Suche können kombiniert werden.

## Dependency Injection

In `Program.cs` wurden Repository und Service registriert:

- `ITaskRepository` wird durch `SqliteTaskRepository` implementiert.
- `TaskService` erhält `ITaskRepository` per Constructor Injection.

Der `TaskService` hängt damit von der Repository-Abstraktion ab und nicht direkt von der SQLite-Implementierung.

## SQLite-Persistenz

Die Task API verwendet `Microsoft.Data.Sqlite` direkt. Entity Framework Core und Migrationen wurden nicht verwendet.

Die Connection String-Konfiguration befindet sich in `appsettings.json`:

```text
Data Source=taskflow.db
```

Die Tabelle `Tasks` wird beim Initialisieren des Repositories automatisch erstellt, falls sie noch nicht existiert.

Datumswerte werden in einem konsistenten ISO-Format gespeichert. Der Abschlussstatus wird als lesbarer Textwert gespeichert.

Die Laufzeitdatenbank `taskflow.db` bleibt durch die bestehende `.gitignore` ausgeschlossen.

## Konfigurierte CORS-Regel

Für das geplante Next.js-Frontend wurde CORS für folgende Origin konfiguriert:

```text
http://localhost:3000
```

Erlaubt sind die benötigten Methoden:

- `GET`
- `POST`
- `PUT`
- `PATCH`
- `DELETE`

Header werden für API-Anfragen zugelassen.

## Implementierte Endpunkte

Der bestehende Root-Endpunkt bleibt erhalten:

| Methode | Route | Zweck |
| --- | --- | --- |
| GET | `/` | Service-Status der Task API |

Neue Task-Endpunkte:

| Methode | Route | Zweck |
| --- | --- | --- |
| GET | `/api/tasks` | Aufgaben abrufen, optional mit `status` und `search` |
| POST | `/api/tasks` | Aufgabe erstellen |
| PUT | `/api/tasks/{id}` | Aufgabe aktualisieren |
| PATCH | `/api/tasks/{id}/toggle` | Abschlussstatus umschalten |
| DELETE | `/api/tasks/{id}` | Aufgabe löschen |

Beispiele für Abfragen:

```text
GET /api/tasks
GET /api/tasks?status=open
GET /api/tasks?status=completed
GET /api/tasks?search=architecture
GET /api/tasks?status=open&search=documentation
```

## HTTP-Statuscodes

| Aktion | Erfolgsfall | Fehlerfälle |
| --- | --- | --- |
| Aufgaben abrufen | 200 | 400 bei ungültigem Statusfilter |
| Aufgabe erstellen | 201 | 400 bei ungültiger Eingabe |
| Aufgabe aktualisieren | 200 | 400 bei ungültiger Eingabe, 404 bei fehlender Aufgabe |
| Abschlussstatus umschalten | 200 | 404 bei fehlender Aufgabe |
| Aufgabe löschen | 204 | 404 bei fehlender Aufgabe |

## Manuelle Überprüfung

Für die manuelle Überprüfung wurde folgende Request-Datei erstellt:

- [backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

Sie enthält Beispielrequests für:

- Root-Endpunkt prüfen
- Aufgabe erstellen
- alle Aufgaben abrufen
- offene Aufgaben filtern
- Aufgaben suchen
- Aufgabe aktualisieren
- Abschlussstatus umschalten
- Aufgabe löschen

Alle Beispiele verwenden:

```text
http://localhost:5001
```

## Erstellte oder aktualisierte Dateien

- `backend/TaskFlow.TaskApi/Models/TaskItem.cs`
- `backend/TaskFlow.TaskApi/Models/TaskRequests.cs`
- `backend/TaskFlow.TaskApi/Repositories/ITaskRepository.cs`
- `backend/TaskFlow.TaskApi/Repositories/SqliteTaskRepository.cs`
- `backend/TaskFlow.TaskApi/Services/TaskService.cs`
- `backend/TaskFlow.TaskApi/Program.cs`
- `backend/TaskFlow.TaskApi/appsettings.json`
- `backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj`
- `backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http`
- `docs/steps/04-task-api.md`
- `README.md`

## Build-Prüfung

Die komplette Backend-Solution wurde mit folgendem Befehl gebaut:

```text
dotnet build TaskFlow.sln
```

Build-Ergebnis:

```text
Der Buildvorgang wurde erfolgreich ausgeführt.
2 Warnung(en)
0 Fehler
```

Die Warnungen stammen aus `NU1903` für eine transitive Abhängigkeit `SQLitePCLRaw.lib.e_sqlite3` im Paketgraphen von `Microsoft.Data.Sqlite`.

## Ergebnis

Die Task API verfügt jetzt über eine vollständige Aufgabenverwaltung mit SQLite-Persistenz, Repository Pattern, Service Layer, Dependency Injection, zentraler Validierung und Minimal-API-Endpunkten.

Die Analytics API wurde in diesem Schritt nicht verändert und ist noch nicht mit der Task API verbunden. Es wurden keine Statistikberechnungen, kein Analytics-HTTP-Client, kein Dashboard-Endpunkt und keine Frontend-Dateien erstellt.
