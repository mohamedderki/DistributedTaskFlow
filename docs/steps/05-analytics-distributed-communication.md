# Schritt 05 – Analytics API und verteilte Kommunikation

## Ziel

In diesem Schritt wurde die separate **Analytics API** implementiert und über HTTP und JSON mit der bestehenden Task API verbunden.

Ziel war es, die Statistikberechnung bewusst aus der Task API auszulagern und als eigenständigen Prozess auszuführen.

Die Anwendung besteht damit auf Backend-Seite aus zwei unabhängig gestarteten Diensten:

| Dienst | Adresse | Verantwortung |
| --- | --- | --- |
| Task API | `http://localhost:5001` | Aufgabenverwaltung, SQLite-Persistenz und Koordination |
| Analytics API | `http://localhost:5002` | Berechnung der Aufgabenstatistiken |

Die Task API bleibt die zentrale Schnittstelle für das spätere Frontend.

Das Frontend soll die Analytics API nicht direkt aufrufen.

---

## Verwendete Werkzeuge und Technologien

- Codex CLI
- .NET CLI
- .NET 10
- C#
- ASP.NET Core Minimal API
- `HttpClient`
- JSON
- Dependency Injection
- Strategy Pattern
- Adapter beziehungsweise Gateway Pattern
- PowerShell
- HTTP-Request-Dateien

---

## Verwendeter Prompt

Der vollständige Prompt dieses Schritts ist im Repository gespeichert:

- [Prompt 05 – Analytics API und verteilte Kommunikation](../prompts/05-analytics-distributed-communication.md)

Der Prompt definierte unter anderem:

- die Statistikmodelle
- die Basic- und Weighted-Strategie
- die Strategy-Schnittstelle
- den internen Statistikendpunkt
- den Analytics Client der Task API
- die HTTP- und JSON-Kommunikation
- den Dashboard-Endpunkt
- das Verhalten bei Ausfall der Analytics API
- die manuelle Prüfung mit zwei getrennten Prozessen

---

## Ausgangslage

Vor diesem Schritt war die Task API bereits vollständig für die Aufgabenverwaltung vorbereitet.

Vorhanden waren:

- Aufgabenmodelle
- Request-Modelle
- zentrale Validierung
- Repository Pattern
- Service Layer
- SQLite-Persistenz
- Dependency Injection
- Aufgaben-CRUD
- Statusfilter
- Titelsuche

Die Analytics API bestand zu diesem Zeitpunkt lediglich aus einem einfachen Root-Endpunkt:

```text
GET /
```

Antwort:

```text
TaskFlow Analytics API is running.
```

Es existierten noch keine:

- Statistikmodelle
- Berechnungsstrategien
- Statistikendpunkte
- HTTP-Kommunikation zwischen den APIs
- Dashboard-Endpunkte
- kontrollierte Fehlerbehandlung bei einem Dienstausfall

---

## Verteilte Architektur

Nach Abschluss dieses Schritts erfolgt die Statistikberechnung über folgende Prozessgrenze:

```text
Task API
   |
   | HTTP / JSON
   v
Analytics API
```

Der vollständige Ablauf lautet:

```text
Frontend
   |
   v
Task API
   |
   | Aufgaben aus SQLite laden
   |
   v
Analytics API
   |
   | Statistik berechnen
   |
   v
Task API
   |
   v
Frontend
```

Task API und Analytics API sind:

- separate ASP.NET-Core-Projekte
- separat gestartete Prozesse
- über unterschiedliche Ports erreichbar
- unabhängig voneinander ausführbar
- ausschließlich über HTTP und JSON miteinander verbunden

Damit wurde die geplante verteilte Architektur technisch umgesetzt.

---

## Durchführung

### 1. Analytics-Modelle erstellen

In der Analytics API wurde eine gemeinsame Modelldatei für die Statistikkommunikation erstellt:

- [`StatisticsModels.cs`](../../backend/TaskFlow.AnalyticsApi/Models/StatisticsModels.cs)

Sie enthält kompakte Modelle für:

- Statistik-Request
- Task-Summary
- Statistik-Response
- Prioritätswerte

Die Analytics API benötigt nicht das vollständige Aufgabenmodell der Task API.

Für die Berechnung werden nur folgende Felder übertragen:

| Feld | Verwendung |
| --- | --- |
| `Priority` | Gewichtete Bewertung offener Aufgaben |
| `DueDate` | Erkennung überfälliger Aufgaben |
| `IsCompleted` | Unterscheidung zwischen offenen und erledigten Aufgaben |

Nicht übertragen werden beispielsweise:

- Aufgaben-ID
- Aufgabentitel
- Erstellungszeitpunkt
- SQLite-Details

Dadurch bleibt die Schnittstelle zwischen den Diensten kompakt und auf die Statistikberechnung beschränkt.

---

### 2. Statistik-Response definieren

Die Statistikantwort enthält folgende Werte:

| Feld | Bedeutung |
| --- | --- |
| `TotalTasks` | Gesamtanzahl der Aufgaben |
| `OpenTasks` | Anzahl offener Aufgaben |
| `CompletedTasks` | Anzahl erledigter Aufgaben |
| `OverdueTasks` | Anzahl offener und überfälliger Aufgaben |
| `CompletionPercentage` | Prozentualer Anteil erledigter Aufgaben |
| `WeightedOpenScore` | Gewichteter Score offener Aufgaben |
| `Strategy` | Verwendete Berechnungsstrategie |

Beispielantwort:

```json
{
  "totalTasks": 10,
  "openTasks": 7,
  "completedTasks": 3,
  "overdueTasks": 2,
  "completionPercentage": 30,
  "weightedOpenScore": 0,
  "strategy": "basic"
}
```

---

## Strategy Pattern

Die Berechnungslogik wurde mit dem Strategy Pattern umgesetzt.

Dafür wurden folgende Dateien erstellt:

- [`IStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/IStatisticsStrategy.cs)
- [`BasicStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/BasicStatisticsStrategy.cs)
- [`WeightedStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/WeightedStatisticsStrategy.cs)

Die gemeinsame Abstraktion ermöglicht es, unterschiedliche Statistikvarianten über dieselbe Schnittstelle auszuführen.

Vereinfachte Struktur:

```text
IStatisticsStrategy
├── BasicStatisticsStrategy
└── WeightedStatisticsStrategy
```

Die Strategie wird zur Laufzeit anhand des Query-Parameters ausgewählt.

---

## `IStatisticsStrategy`

Die Schnittstelle definiert:

- einen eindeutigen Strategienamen
- eine gemeinsame Methode zur Berechnung der Statistik

Dadurch muss der Statistikendpunkt keine konkreten Berechnungsdetails kennen.

Er sucht lediglich die passende registrierte Strategie und führt sie aus.

Zugehörige Datei:

- [`IStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/IStatisticsStrategy.cs)

---

## Basic Statistics Strategy

Die Basic-Strategie berechnet:

- Gesamtzahl der Aufgaben
- Anzahl offener Aufgaben
- Anzahl erledigter Aufgaben
- Anzahl überfälliger Aufgaben
- Abschlussquote

Zugehörige Datei:

- [`BasicStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/BasicStatisticsStrategy.cs)

Bei dieser Strategie ist der gewichtete Score:

```text
WeightedOpenScore = 0
```

Eine Aufgabe gilt als überfällig, wenn:

```text
IsCompleted = false
```

und:

```text
DueDate < aktuelles UTC-Datum
```

Erledigte Aufgaben werden nicht als überfällig gezählt.

---

## Weighted Statistics Strategy

Die Weighted-Strategie berechnet dieselben Basiswerte wie die Basic-Strategie.

Zusätzlich wird ein gewichteter Score für offene Aufgaben berechnet.

Zugehörige Datei:

- [`WeightedStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/WeightedStatisticsStrategy.cs)

Verwendete Gewichtung:

| Priorität | Gewicht |
| --- | ---: |
| `Low` | `1` |
| `Medium` | `2` |
| `High` | `3` |

Nur offene Aufgaben tragen zum Score bei.

Beispiel:

```text
2 offene High-Aufgaben   = 2 × 3 = 6
1 offene Medium-Aufgabe  = 1 × 2 = 2
3 offene Low-Aufgaben    = 3 × 1 = 3
-----------------------------------
WeightedOpenScore                  = 11
```

Erledigte Aufgaben verändern den gewichteten Score nicht.

Gemeinsame Basisberechnungen werden wiederverwendet, damit identische Statistiklogik nicht unnötig dupliziert wird.

---

## Dependency Injection in der Analytics API

Beide Strategien wurden in der Analytics API über Dependency Injection registriert.

Der Statistikendpunkt erzeugt keine Strategien manuell mit:

```csharp
new BasicStatisticsStrategy()
```

oder:

```csharp
new WeightedStatisticsStrategy()
```

Stattdessen erhält er die registrierten Implementierungen über den DI-Container.

Dadurch bleiben:

- Endpunktlogik und Berechnungslogik getrennt
- Strategien austauschbar
- Abhängigkeiten transparent
- konkrete Implementierungen außerhalb des Endpunkts

Zugehörige Datei:

- [`Program.cs`](../../backend/TaskFlow.AnalyticsApi/Program.cs)

---

## Statistikendpunkt der Analytics API

In der Analytics API wurde folgender Endpunkt implementiert:

```text
POST /api/statistics
```

Unterstützte Query-Parameter:

```text
strategy=basic
strategy=weighted
```

Beispiele:

```text
POST /api/statistics?strategy=basic
POST /api/statistics?strategy=weighted
```

Der Request Body enthält die kompakten Task-Summaries.

Beispiel:

```json
{
  "tasks": [
    {
      "priority": "High",
      "dueDate": "2026-07-10",
      "isCompleted": false
    },
    {
      "priority": "Medium",
      "dueDate": "2026-07-20",
      "isCompleted": true
    }
  ]
}
```

Der Endpunkt:

1. liest den gewünschten Strategienamen
2. sucht die passende DI-registrierte Strategie
3. übergibt die Task-Summaries an die Strategie
4. berechnet die Statistik
5. gibt das Ergebnis als JSON zurück

---

## Unbekannte Statistikstrategie

Wird eine unbekannte Strategie übergeben, liefert die Analytics API:

```text
HTTP 400 Bad Request
```

Beispiel:

```text
POST /api/statistics?strategy=unknown
```

Dadurch wird verhindert, dass nicht unterstützte Strategiewerte stillschweigend verarbeitet werden.

---

## Analytics-Modelle in der Task API

Die Task API benötigt eigene Kommunikationsmodelle für den Austausch mit der Analytics API.

Dafür wurde folgende Datei erstellt:

- [`AnalyticsModels.cs`](../../backend/TaskFlow.TaskApi/Models/AnalyticsModels.cs)

Diese Modelle dienen ausschließlich der HTTP-Kommunikation und trennen:

- das interne `TaskItem`-Modell
- das externe Analytics-Request-Modell
- die Statistikantwort

Die Task API gibt dadurch keine Datenbank- oder Repository-Details an die Analytics API weiter.

---

## Adapter beziehungsweise Gateway Pattern

Die HTTP-Kommunikation wurde in einem eigenen Client gekapselt.

Erstellt wurden:

- [`IAnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/IAnalyticsClient.cs)
- [`AnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/AnalyticsClient.cs)

Der Analytics Client bildet die technische Grenze zwischen Task API und Analytics API.

Andere Bestandteile der Task API müssen keine Details über:

- HTTP-Methoden
- Zieladressen
- JSON-Serialisierung
- Query-Parameter
- HTTP-Fehler
- Response-Deserialisierung

kennen.

---

## `IAnalyticsClient`

`IAnalyticsClient` definiert die Abstraktion für die Statistikberechnung.

Die Schnittstelle:

- nimmt Aufgaben der Task API entgegen
- erhält die gewünschte Strategie
- führt die externe Berechnung asynchron aus
- liefert das Statistikmodell zurück

Die Task API hängt damit nicht direkt von der konkreten `AnalyticsClient`-Implementierung ab.

Zugehörige Datei:

- [`IAnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/IAnalyticsClient.cs)

---

## `AnalyticsClient`

Die konkrete HTTP-Kommunikation wurde in `AnalyticsClient` implementiert.

Zugehörige Datei:

- [`AnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/AnalyticsClient.cs)

Der Client übernimmt:

1. das Mapping von `TaskItem` auf kompakte Task-Summaries
2. das Erstellen des Statistik-Requests
3. das Übergeben der ausgewählten Strategie
4. das Senden des Requests als JSON
5. das Auslesen der HTTP-Antwort
6. das Deserialisieren der Statistikantwort
7. das Erkennen unerwarteter oder fehlerhafter Antworten

Der `HttpClient` wird nicht manuell mit folgendem Code erzeugt:

```csharp
new HttpClient()
```

Stattdessen wird er zentral über `AddHttpClient` konfiguriert und vom DI-Container verwaltet.

---

## Konfiguration des Analytics Clients

Die Basisadresse der Analytics API wurde in der Task API konfiguriert.

Zugehörige Datei:

- [`appsettings.json`](../../backend/TaskFlow.TaskApi/appsettings.json)

Konfigurierte Adresse:

```text
http://localhost:5002
```

Die Task API liest diese Adresse aus der Konfiguration und verwendet sie für den registrierten `HttpClient`.

Dadurch ist die Zieladresse nicht fest in der Geschäftslogik eingebaut.

---

## Dependency Injection in der Task API

In der Task API wurde `IAnalyticsClient` über `AddHttpClient` registriert.

Vereinfachte Abhängigkeitsstruktur:

```text
Dashboard-Endpunkt
       |
       v
IAnalyticsClient
       |
       v
AnalyticsClient
       |
       v
HttpClient
       |
       v
Analytics API
```

Der Dashboard-Endpunkt kennt damit nur die Client-Abstraktion und keine Details der HTTP-Implementierung.

Zugehörige Datei:

- [`Program.cs`](../../backend/TaskFlow.TaskApi/Program.cs)

---

## Dashboard-Endpunkt der Task API

In der Task API wurde folgender Endpunkt ergänzt:

```text
GET /api/dashboard
```

Unterstützte Varianten:

```text
GET /api/dashboard?strategy=basic
GET /api/dashboard?strategy=weighted
```

Wenn kein Query-Parameter übergeben wird, wird die Basic-Strategie verwendet.

Der Ablauf des Endpunkts lautet:

1. alle Aufgaben über den vorhandenen `TaskService` laden
2. die Aufgaben an `IAnalyticsClient` übergeben
3. nur die benötigten Felder auf Analytics-Summaries abbilden
4. die Analytics API über HTTP aufrufen
5. die Statistikantwort empfangen
6. die Antwort an den aufrufenden Client zurückgeben

Die bestehenden Endpunkte unter:

```text
/api/tasks
```

wurden dadurch nicht ersetzt oder in ihrer Verantwortung verändert.

---

## HTTP- und JSON-Kommunikation

Die Kommunikation zwischen den beiden Diensten erfolgt über:

- HTTP
- JSON
- asynchrone Requests
- getrennte Request- und Response-Modelle

Kommunikationsweg:

```text
TaskFlow.TaskApi
        |
        | POST /api/statistics?strategy=...
        | Content-Type: application/json
        v
TaskFlow.AnalyticsApi
```

Die Task API bleibt Eigentümerin der vollständigen Aufgabendaten.

Die Analytics API erhält nur die für die Berechnung benötigten Informationen.

---

## Fehlerbehandlung bei nicht verfügbarer Analytics API

Ein wichtiger Bestandteil dieses Schritts war das kontrollierte Verhalten bei einem Ausfall des Statistikdienstes.

Wenn die Analytics API:

- nicht gestartet ist
- nicht erreichbar ist
- die Verbindung ablehnt
- vorübergehend keine gültige Antwort liefert

antwortet der Dashboard-Endpunkt der Task API mit:

```text
HTTP 503 Service Unavailable
```

Verwendete Nachricht:

```text
Statistics are temporarily unavailable. Your tasks can still be managed.
```

Dadurch wird der Fehler klar auf die Statistikfunktion begrenzt.

---

## Aufgabenverwaltung während des Ausfalls

Der Ausfall der Analytics API beeinträchtigt nicht die SQLite-basierte Aufgabenverwaltung.

Folgende Endpunkte bleiben nutzbar:

```text
GET    /api/tasks
POST   /api/tasks
PUT    /api/tasks/{id}
PATCH  /api/tasks/{id}/toggle
DELETE /api/tasks/{id}
```

Das bedeutet:

- Aufgaben können weiterhin geladen werden.
- Aufgaben können weiterhin erstellt werden.
- Aufgaben können weiterhin bearbeitet werden.
- Aufgaben können weiterhin abgeschlossen oder wieder geöffnet werden.
- Aufgaben können weiterhin gelöscht werden.
- Suche und Statusfilter bleiben verfügbar.
- ausschließlich die Statistikberechnung ist vorübergehend nicht verfügbar.

Dieses Verhalten demonstriert die kontrollierte Degradierung einer verteilten Anwendung.

---

## Ungültige Strategie am Dashboard-Endpunkt

Wird am Dashboard-Endpunkt eine unbekannte Strategie übergeben, liefert die Task API:

```text
HTTP 400 Bad Request
```

Beispiel:

```text
GET /api/dashboard?strategy=unknown
```

Damit werden ungültige Benutzer- oder Clientanfragen von echten Netzwerkfehlern unterschieden.

---

## Konfigurierte Adressen

| Modul | Adresse |
| --- | --- |
| Task API | `http://localhost:5001` |
| Analytics API | `http://localhost:5002` |

Die APIs laufen gleichzeitig, aber als getrennte Prozesse.

---

## APIs lokal starten

Für die manuelle Prüfung wurden zwei Terminalfenster verwendet.

### Analytics API starten

Aus dem Verzeichnis `backend/`:

```powershell
dotnet run `
  --project TaskFlow.AnalyticsApi\TaskFlow.AnalyticsApi.csproj `
  --urls "http://localhost:5002"
```

---

### Task API starten

In einem zweiten Terminal, ebenfalls aus `backend/`:

```powershell
dotnet run `
  --project TaskFlow.TaskApi\TaskFlow.TaskApi.csproj `
  --urls "http://localhost:5001"
```

Danach waren beide Root-Endpunkte erreichbar:

```text
http://localhost:5001
http://localhost:5002
```

---

## Manuelle Prüfung

Für die manuelle Prüfung wurden die Request-Dateien beider APIs verwendet:

- [`TaskFlow.TaskApi.http`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)
- [`TaskFlow.AnalyticsApi.http`](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http)

Geprüft wurden folgende Abläufe:

### Analytics API

- Root-Endpunkt aufrufen
- Basic-Statistik berechnen
- Weighted-Statistik berechnen
- leere Aufgabenliste berechnen
- unbekannte Strategie senden
- HTTP-Status `400` für ungültige Strategie prüfen

### Task API

- Root-Endpunkt aufrufen
- Aufgaben laden
- Dashboard mit Basic-Strategie laden
- Dashboard mit Weighted-Strategie laden
- unbekannte Strategie senden
- Weiterleitung der Statistikantwort prüfen

### Ausfallprüfung

Zusätzlich wurde die Analytics API während der Prüfung gestoppt.

Danach wurde geprüft:

- Dashboard-Endpunkt liefert `503`
- Fehlermeldung ist verständlich
- `/api/tasks` bleibt erreichbar
- Aufgaben-CRUD bleibt funktionsfähig
- nach dem Neustart der Analytics API funktionieren die Statistiken wieder

Nach Abschluss der Prüfung wurden die gestarteten Prozesse gestoppt.

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

Ergebnis:

```text
Der Buildvorgang wurde erfolgreich ausgeführt.
0 Warnung(en)
0 Fehler
```

Damit wurde bestätigt, dass:

- beide API-Projekte korrekt kompilieren
- die Statistikstrategien registriert sind
- die Analytics-Modelle gültig sind
- der Analytics Client korrekt eingebunden ist
- die HTTP-Kommunikation technisch vorbereitet ist
- keine Buildfehler vorhanden sind

---

## Zugehörige Dateien

### Analytics API – Modelle

- [`StatisticsModels.cs`](../../backend/TaskFlow.AnalyticsApi/Models/StatisticsModels.cs)

### Analytics API – Strategien

- [`IStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/IStatisticsStrategy.cs)
- [`BasicStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/BasicStatisticsStrategy.cs)
- [`WeightedStatisticsStrategy.cs`](../../backend/TaskFlow.AnalyticsApi/Strategies/WeightedStatisticsStrategy.cs)

### Analytics API – Einstiegspunkt und manuelle Requests

- [`Program.cs`](../../backend/TaskFlow.AnalyticsApi/Program.cs)
- [`TaskFlow.AnalyticsApi.http`](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http)

### Task API – Analytics-Modelle

- [`AnalyticsModels.cs`](../../backend/TaskFlow.TaskApi/Models/AnalyticsModels.cs)

### Task API – Analytics Client

- [`IAnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/IAnalyticsClient.cs)
- [`AnalyticsClient.cs`](../../backend/TaskFlow.TaskApi/Services/AnalyticsClient.cs)

### Task API – Konfiguration und Endpunkt

- [`Program.cs`](../../backend/TaskFlow.TaskApi/Program.cs)
- [`appsettings.json`](../../backend/TaskFlow.TaskApi/appsettings.json)
- [`TaskFlow.TaskApi.http`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

---

## Screenshots und Nachweise

Für diesen Schritt wurde nach der bekannten Projektstruktur kein eigener Screenshot gespeichert.

Der technische Nachweis erfolgt durch:

- den gespeicherten Prompt
- die Analytics-Modelle
- die Strategy-Schnittstelle
- die Basic-Strategie
- die Weighted-Strategie
- den Analytics Client
- die konfigurierten HTTP-Adressen
- die Request-Dateien beider APIs
- die getrennt laufenden Prozesse
- den Dashboard-Endpunkt
- die kontrollierte `503`-Fehlerantwort
- den erfolgreichen Build

---

## Nicht Bestandteil dieses Schritts

Folgende Funktionen wurden bewusst noch nicht umgesetzt:

- Swagger UI
- OpenAPI-Dokumentation
- Next.js-Frontend
- React-Komponenten
- Frontend-API-Schicht
- Browserintegration
- Frontend-Loading-State
- Frontend-Statistics-Error-State
- Retry-Button im Frontend

Diese Funktionen wurden erst in späteren Schritten ergänzt.

---

## Ergebnis

Am Ende dieses Schritts war die verteilte Backend-Kommunikation vollständig umgesetzt.

Implementiert wurden:

- Analytics-Request- und Response-Modelle
- kompakte Task-Summaries
- `IStatisticsStrategy`
- Basic-Statistikstrategie
- Weighted-Statistikstrategie
- Dependency Injection für beide Strategien
- interner Statistikendpunkt
- `IAnalyticsClient`
- konkreter `AnalyticsClient`
- verwalteter `HttpClient`
- HTTP- und JSON-Kommunikation
- Dashboard-Endpunkt der Task API
- Strategieauswahl
- kontrollierte `400`-Antworten
- kontrollierte `503`-Antwort bei Ausfall
- weiterhin nutzbare Aufgabenverwaltung
- manuelle Prüfung mit zwei getrennten Prozessen

Die vollständige Backend-Solution wurde erfolgreich gebaut:

```text
0 Warnung(en)
0 Fehler
```

Damit wurde der zentrale verteilte Teil von DistributedTaskFlow erfolgreich realisiert.