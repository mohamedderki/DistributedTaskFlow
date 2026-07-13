# Schritt 04 – Implementierung der Task API

## Ziel

In diesem Schritt wurde die bestehende ASP.NET-Core-Anwendung `TaskFlow.TaskApi` um die vollständige Aufgabenverwaltung erweitert.

Ziel war die Implementierung einer eigenständig nutzbaren Task API mit:

- Aufgabenmodellen
- Request-Modellen
- zentraler Validierung
- SQLite-Persistenz
- Repository Pattern
- Service Layer
- Dependency Injection
- REST-Endpunkten
- Statusfiltern
- Titelsuche
- manuellen HTTP-Requests

Die Analytics API wurde in diesem Schritt noch nicht verändert und noch nicht mit der Task API verbunden.

---

## Verwendete Werkzeuge und Technologien

- Codex CLI
- .NET CLI
- .NET 10
- C#
- ASP.NET Core Minimal API
- SQLite
- Microsoft.Data.Sqlite
- PowerShell
- HTTP-Request-Datei

---

## Verwendeter Prompt

Der vollständige Prompt dieses Schritts ist im Repository gespeichert:

- [Prompt 04 – Implementierung der Task API](../prompts/04-task-api.md)

Der Prompt definierte unter anderem:

- die benötigten Aufgabenmodelle
- die Validierungsregeln
- die Repository-Schnittstelle
- die SQLite-Implementierung
- den Service Layer
- die REST-Endpunkte
- die CORS-Konfiguration
- die manuelle Prüfung über eine `.http`-Datei
- die abschließende Build-Prüfung

---

## Ausgangslage

Vor diesem Schritt bestand die Task API nur aus einem einfachen Root-Endpunkt:

```text
GET /
```

Antwort:

```text
TaskFlow Task API is running.
```

Die Backend-Solution und das separate Projekt `TaskFlow.AnalyticsApi` waren bereits vorhanden.

Es existierten jedoch noch keine:

- Aufgabenmodelle
- Business-Endpunkte
- Validierungsregeln
- Repository-Schicht
- SQLite-Datenbank
- Aufgabenservices
- Such- oder Filterfunktionen

In Schritt 04 wurde ausschließlich das Projekt `TaskFlow.TaskApi` funktional erweitert.

---

## Durchführung

### 1. Aufgabenmodell erstellen

Als zentrales Domänenmodell wurde `TaskItem` erstellt.

Das Modell enthält folgende Eigenschaften:

| Eigenschaft | Bedeutung |
| --- | --- |
| `Id` | Eindeutige Aufgaben-ID |
| `Title` | Titel der Aufgabe |
| `Priority` | Priorität der Aufgabe |
| `DueDate` | Fälligkeitsdatum |
| `IsCompleted` | Aktueller Abschlussstatus |
| `CreatedAtUtc` | Erstellungszeitpunkt in UTC |

Zugehörige Datei:

- [`TaskItem.cs`](../../backend/TaskFlow.TaskApi/Models/TaskItem.cs)

---

### 2. Prioritätswerte definieren

Für die Aufgabenpriorität wurde das Enum `TaskPriority` verwendet.

Unterstützte Werte:

```text
Low
Medium
High
```

Die JSON-Konfiguration wurde so eingerichtet, dass die Prioritäten nicht als numerische Werte, sondern als lesbare Strings übertragen werden.

Beispiel:

```json
{
  "priority": "High"
}
```

Dadurch können Frontend und API mit klar verständlichen Werten kommunizieren.

---

### 3. Request-Modelle erstellen

Für das Erstellen und Bearbeiten einer Aufgabe wurden getrennte Request-Modelle definiert:

- `CreateTaskRequest`
- `UpdateTaskRequest`

Zugehörige Datei:

- [`TaskRequests.cs`](../../backend/TaskFlow.TaskApi/Models/TaskRequests.cs)

Beispiel für eine gültige Anfrage:

```json
{
  "title": "Implement Task API",
  "priority": "High",
  "dueDate": "2026-07-15"
}
```

Der Abschlussstatus und der Erstellungszeitpunkt werden nicht vom Client gesetzt.

Diese Werte werden von der Task API verwaltet.

---

## Validierung

Die fachliche Validierung wurde zentral im `TaskService` umgesetzt.

Dadurch befinden sich die Validierungsregeln nicht direkt in den HTTP-Endpunkten oder im SQLite-Repository.

### Titel

Für den Titel gelten folgende Regeln:

- `Title` ist erforderlich.
- Leere Titel sind nicht erlaubt.
- Der Titel wird vor dem Speichern getrimmt.
- Der Titel darf maximal 120 Zeichen enthalten.

### Priorität

Für die Priorität gelten folgende Regeln:

- Es muss ein gültiger Enum-Wert übergeben werden.
- Erlaubt sind ausschließlich `Low`, `Medium` und `High`.
- Ungültige String- oder Zahlenwerte werden abgelehnt.

### Fälligkeitsdatum

Für das Fälligkeitsdatum gelten folgende Regeln:

- `DueDate` ist erforderlich.
- Das Datum wird im Format `YYYY-MM-DD` übertragen.
- Vergangene Fälligkeitsdaten sind erlaubt.

Vergangene Daten wurden bewusst zugelassen, damit überfällige Aufgaben dargestellt und später statistisch ausgewertet werden können.

### Fehlerantworten

Ungültige Anfragen liefern:

```text
HTTP 400 Bad Request
```

Zusätzlich wird eine verständliche Fehlermeldung zurückgegeben.

---

## Repository Pattern

Der Datenbankzugriff wurde mit dem Repository Pattern von der Geschäftslogik getrennt.

### Repository-Schnittstelle

Die Schnittstelle `ITaskRepository` definiert die benötigten asynchronen Datenoperationen.

Dazu gehören:

- alle Aufgaben laden
- einzelne Aufgabe nach ID laden
- Aufgabe hinzufügen
- Aufgabe aktualisieren
- Aufgabe löschen

Zugehörige Datei:

- [`ITaskRepository.cs`](../../backend/TaskFlow.TaskApi/Repositories/ITaskRepository.cs)

Die restliche Anwendung verwendet diese Abstraktion und kennt keine SQLite-Details.

---

### SQLite-Repository

Die konkrete Datenbankimplementierung befindet sich in `SqliteTaskRepository`.

Zugehörige Datei:

- [`SqliteTaskRepository.cs`](../../backend/TaskFlow.TaskApi/Repositories/SqliteTaskRepository.cs)

Das Repository übernimmt:

- Öffnen der SQLite-Verbindung
- Erstellen der Datenbanktabelle
- Ausführen der SQL-Befehle
- Lesen der Datenbankzeilen
- Umwandeln der Datenbankwerte in `TaskItem`
- Speichern neuer Aufgaben
- Aktualisieren bestehender Aufgaben
- Löschen von Aufgaben

SQL-Code befindet sich ausschließlich im Repository.

Andere Schichten enthalten keine direkten SQLite-Befehle.

---

## SQLite-Persistenz

Für die Persistenz wurde `Microsoft.Data.Sqlite` direkt verwendet.

Nicht verwendet wurden:

- Entity Framework Core
- Migrationen
- externe Datenbankserver

Die Connection String-Konfiguration befindet sich in:

- [`appsettings.json`](../../backend/TaskFlow.TaskApi/appsettings.json)

Konfiguration:

```json
{
  "ConnectionStrings": {
    "TaskDatabase": "Data Source=taskflow.db"
  }
}
```

Die Datenbankdatei wird lokal unter dem Task-API-Projekt verwendet:

```text
backend/TaskFlow.TaskApi/taskflow.db
```

Die Tabelle `Tasks` wird automatisch erstellt, falls sie noch nicht existiert.

Gespeichert werden:

- ID
- Titel
- Priorität
- Fälligkeitsdatum
- Abschlussstatus
- Erstellungszeitpunkt

Datumswerte werden in einem ISO-kompatiblen Format gespeichert.

Der boolesche Abschlussstatus wird in einer SQLite-kompatiblen numerischen Darstellung gespeichert.

Die lokale Laufzeitdatenbank ist durch die Git-Konfiguration ausgeschlossen und wird nicht als Quellcodedatei behandelt.

---

## Service Layer

Die zentrale Aufgabenlogik wurde in `TaskService` umgesetzt.

Zugehörige Datei:

- [`TaskService.cs`](../../backend/TaskFlow.TaskApi/Services/TaskService.cs)

Der Service übernimmt:

- Eingaben validieren
- Titel normalisieren
- Aufgaben laden
- Aufgaben filtern
- Aufgaben durchsuchen
- Aufgaben erstellen
- Aufgaben bearbeiten
- Abschlussstatus umschalten
- Aufgaben löschen
- nicht vorhandene Aufgaben erkennen
- Repository-Aufrufe koordinieren

Dadurch bleiben die Minimal-API-Endpunkte in `Program.cs` klein und übersichtlich.

---

## Statusfilter

Die Task API unterstützt folgende Statusfilter:

```text
all
open
completed
```

Bedeutung:

| Filter | Ergebnis |
| --- | --- |
| `all` | Alle Aufgaben |
| `open` | Nur nicht erledigte Aufgaben |
| `completed` | Nur erledigte Aufgaben |

Beispiele:

```text
GET /api/tasks?status=all
GET /api/tasks?status=open
GET /api/tasks?status=completed
```

Ein unbekannter Statusfilter liefert:

```text
HTTP 400 Bad Request
```

---

## Titelsuche

Über den Query-Parameter `search` können Aufgaben nach ihrem Titel durchsucht werden.

Beispiel:

```text
GET /api/tasks?search=architecture
```

Statusfilter und Suche können kombiniert werden:

```text
GET /api/tasks?status=open&search=documentation
```

Die Filterung und Suche werden im `TaskService` koordiniert.

---

## Dependency Injection

Repository und Service wurden in `Program.cs` über Dependency Injection registriert.

Die zentrale Abhängigkeit lautet:

```text
TaskService
    |
    v
ITaskRepository
    |
    v
SqliteTaskRepository
```

Der `TaskService` hängt damit von der Repository-Abstraktion und nicht direkt von der SQLite-Implementierung ab.

Das unterstützt:

- geringe Kopplung
- klar getrennte Verantwortlichkeiten
- einfachere Austauschbarkeit
- übersichtliche Konstruktorabhängigkeiten

Zugehörige Datei:

- [`Program.cs`](../../backend/TaskFlow.TaskApi/Program.cs)

---

## JSON-Konfiguration

Die API wurde so konfiguriert, dass Enum-Werte als Strings serialisiert und deserialisiert werden.

Beispielantwort:

```json
{
  "id": 1,
  "title": "Implement Task API",
  "priority": "High",
  "dueDate": "2026-07-15",
  "isCompleted": false,
  "createdAtUtc": "2026-07-13T10:30:00Z"
}
```

Dadurch sind die JSON-Daten für Menschen und Frontend-Code leichter verständlich.

---

## CORS-Konfiguration

Für das später geplante Next.js-Frontend wurde eine erste CORS-Regel eingerichtet.

Erlaubte Origin:

```text
http://localhost:3000
```

Erlaubte Methoden:

```text
GET
POST
PUT
PATCH
DELETE
OPTIONS
```

Zusätzlich wurden die für JSON-Anfragen benötigten Request-Header zugelassen.

In diesem Schritt existierte das Frontend noch nicht. Die CORS-Konfiguration bereitete lediglich die spätere Integration vor.

---

## Implementierte Endpunkte

Der bestehende Root-Endpunkt blieb erhalten.

| Methode | Route | Beschreibung |
| --- | --- | --- |
| `GET` | `/` | Status der Task API prüfen |

Neue Aufgabenendpunkte:

| Methode | Route | Beschreibung |
| --- | --- | --- |
| `GET` | `/api/tasks` | Aufgaben laden, filtern und durchsuchen |
| `POST` | `/api/tasks` | Neue Aufgabe erstellen |
| `PUT` | `/api/tasks/{id}` | Bestehende Aufgabe bearbeiten |
| `PATCH` | `/api/tasks/{id}/toggle` | Abschlussstatus umschalten |
| `DELETE` | `/api/tasks/{id}` | Aufgabe löschen |

---

## Endpunktdetails

### Aufgaben laden

```text
GET /api/tasks
```

Optionale Query-Parameter:

```text
status
search
```

Beispiele:

```text
GET /api/tasks
GET /api/tasks?status=open
GET /api/tasks?status=completed
GET /api/tasks?search=architecture
GET /api/tasks?status=open&search=documentation
```

Erfolgreiche Antwort:

```text
HTTP 200 OK
```

---

### Aufgabe erstellen

```text
POST /api/tasks
```

Beispielbody:

```json
{
  "title": "Create SQLite repository",
  "priority": "High",
  "dueDate": "2026-07-15"
}
```

Erfolgreiche Antwort:

```text
HTTP 201 Created
```

Die neu erstellte Aufgabe wird als JSON zurückgegeben.

---

### Aufgabe aktualisieren

```text
PUT /api/tasks/{id}
```

Beispielbody:

```json
{
  "title": "Update SQLite repository",
  "priority": "Medium",
  "dueDate": "2026-07-16"
}
```

Erfolgreiche Antwort:

```text
HTTP 200 OK
```

Der vorhandene Abschlussstatus bleibt beim Bearbeiten der Aufgabendaten erhalten.

---

### Abschlussstatus umschalten

```text
PATCH /api/tasks/{id}/toggle
```

Der Endpunkt wechselt den Status:

```text
open → completed
completed → open
```

Erfolgreiche Antwort:

```text
HTTP 200 OK
```

Die aktualisierte Aufgabe wird zurückgegeben.

---

### Aufgabe löschen

```text
DELETE /api/tasks/{id}
```

Erfolgreiche Antwort:

```text
HTTP 204 No Content
```

---

## HTTP-Statuscodes

| Aktion | Erfolgsfall | Fehlerfälle |
| --- | --- | --- |
| Aufgaben laden | `200 OK` | `400 Bad Request` bei ungültigem Status |
| Aufgabe erstellen | `201 Created` | `400 Bad Request` bei ungültigen Eingaben |
| Aufgabe aktualisieren | `200 OK` | `400 Bad Request`, `404 Not Found` |
| Status umschalten | `200 OK` | `404 Not Found` |
| Aufgabe löschen | `204 No Content` | `404 Not Found` |

---

## Manuelle Prüfung

Die vorhandene Request-Datei wurde für die manuelle Prüfung der Task API erweitert:

- [`TaskFlow.TaskApi.http`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

Sie enthält Beispielanfragen für:

- Root-Endpunkt
- Aufgabe erstellen
- alle Aufgaben laden
- offene Aufgaben laden
- erledigte Aufgaben laden
- Aufgaben durchsuchen
- Aufgabe bearbeiten
- Status umschalten
- Aufgabe löschen
- fehlerhafte Eingaben prüfen

Alle Requests verwenden als Basisadresse:

```text
http://localhost:5001
```

Die Datei kann beispielsweise in Visual Studio, Visual Studio Code oder einem kompatiblen HTTP-Client ausgeführt werden.

---

## Zugehörige Dateien

### Modelle

- [`TaskItem.cs`](../../backend/TaskFlow.TaskApi/Models/TaskItem.cs)
- [`TaskRequests.cs`](../../backend/TaskFlow.TaskApi/Models/TaskRequests.cs)

### Repository

- [`ITaskRepository.cs`](../../backend/TaskFlow.TaskApi/Repositories/ITaskRepository.cs)
- [`SqliteTaskRepository.cs`](../../backend/TaskFlow.TaskApi/Repositories/SqliteTaskRepository.cs)

### Service

- [`TaskService.cs`](../../backend/TaskFlow.TaskApi/Services/TaskService.cs)

### API und Konfiguration

- [`Program.cs`](../../backend/TaskFlow.TaskApi/Program.cs)
- [`appsettings.json`](../../backend/TaskFlow.TaskApi/appsettings.json)
- [`TaskFlow.TaskApi.csproj`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj)
- [`TaskFlow.TaskApi.http`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

---

## Build-Prüfung

Nach der Implementierung wurde die vollständige Backend-Solution gebaut.

Ausgeführt im Verzeichnis:

```text
backend/
```

Befehl:

```powershell
dotnet build TaskFlow.sln
```

Ergebnis zu diesem Zeitpunkt:

```text
Der Buildvorgang wurde erfolgreich ausgeführt.
2 Warnung(en)
0 Fehler
```

Die beiden Warnungen stammten aus:

```text
NU1903
```

Betroffen war eine transitive Abhängigkeit:

```text
SQLitePCLRaw.lib.e_sqlite3
```

Die Warnung blockierte den Build nicht.

Die technische Bereinigung dieser Paketwarnung war nicht Bestandteil von Schritt 04 und wurde für einen späteren Entwicklungsschritt festgehalten.

---

## Screenshots und Nachweise

Für diesen Schritt wurde nach der vorhandenen Projektstruktur kein eigener Screenshot gespeichert.

Der technische Nachweis erfolgt durch:

- den gespeicherten Prompt
- die implementierten Modelle
- die Repository-Schnittstelle
- das SQLite-Repository
- den Task Service
- die Minimal-API-Endpunkte
- die `.http`-Request-Datei
- die lokale SQLite-Datenbank
- den erfolgreichen Build

---

## Nicht Bestandteil dieses Schritts

Folgende Funktionen wurden bewusst noch nicht umgesetzt:

- Analytics-Request- und Response-Modelle
- Analytics HTTP Client
- Kommunikation zwischen Task API und Analytics API
- Statistikstrategien
- Dashboard-Endpunkt
- Swagger UI
- Next.js-Frontend
- Frontend-API-Integration

Die Analytics API blieb in diesem Schritt unverändert.

---

## Ergebnis

Am Ende dieses Schritts verfügte die Task API über eine vollständige Aufgabenverwaltung.

Umgesetzt wurden:

- Aufgabenmodell
- Prioritäts-Enum
- Create- und Update-Requests
- zentrale Validierung
- Repository Pattern
- SQLite-Repository
- Service Layer
- Dependency Injection
- SQLite-Persistenz
- Statusfilter
- Titelsuche
- Aufgaben-CRUD
- CORS-Vorbereitung
- manuelle HTTP-Prüfung

Die vollständige Backend-Solution konnte erfolgreich gebaut werden:

```text
0 Fehler
```

Damit war die Aufgabenverwaltung funktionsfähig und die Grundlage für die anschließende verteilte Kommunikation mit der Analytics API geschaffen.