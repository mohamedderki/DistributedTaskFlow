# Schritt 03 – Backend-Solution-Struktur

## Ziel

In diesem Schritt wurde die technische Grundstruktur des Backends für **DistributedTaskFlow** erstellt.

Ziel war es, eine klassische .NET-Solution mit zwei voneinander getrennten ASP.NET-Core-Web-API-Projekten aufzubauen:

- Task API
- Analytics API

Beide APIs sollten bereits als eigenständige Prozesse gestartet und über unterschiedliche lokale Ports erreicht werden können.

In diesem Schritt wurden noch keine fachlichen Funktionen wie Aufgabenverwaltung, SQLite-Persistenz oder Statistikberechnung implementiert.

---

## Verwendete Werkzeuge und Technologien

- Codex CLI
- .NET CLI
- .NET 10
- C#
- ASP.NET Core Web API
- klassische `.sln`-Solution-Datei
- PowerShell

---

## Verwendeter Prompt

Der vollständige Prompt dieses Schritts ist im Repository gespeichert:

- [Prompt 03 – Backend-Solution-Struktur](../prompts/03-backend-solution-structure.md)

Der Prompt definierte unter anderem:

- die Erstellung einer klassischen Solution-Datei
- die Erstellung von zwei getrennten Web-API-Projekten
- die vorgesehenen Projektnamen
- die lokalen Ports
- die Entfernung nicht benötigter Template-Endpunkte
- die Durchführung einer vollständigen Build-Prüfung

---

## Verwendete .NET-SDK-Version

Zum Zeitpunkt der Durchführung war folgende .NET-SDK-Version aktiv:

```text
10.0.301
```

Zusätzlich waren auf dem Entwicklungssystem folgende SDK-Versionen installiert:

```text
8.0.408
10.0.203
10.0.301
```

Die aktive Version wurde mit folgendem Befehl geprüft:

```powershell
dotnet --version
```

Alle Backend-Projekte verwenden:

```text
net10.0
```

als Zielframework.

---

## Durchführung

### 1. Backend-Verzeichnis verwenden

Die Backend-Projekte wurden im bereits vorgesehenen Verzeichnis erstellt:

```text
backend/
```

Die resultierende Struktur sollte alle serverseitigen Projekte gemeinsam enthalten.

---

### 2. Klassische Solution-Datei erstellen

Im Verzeichnis `backend/` wurde eine klassische .NET-Solution erstellt:

```powershell
cd backend

dotnet new sln `
  --name TaskFlow `
  --format sln
```

Dadurch entstand folgende Datei:

```text
backend/TaskFlow.sln
```

Die Solution dient als gemeinsame Verwaltungseinheit für beide API-Projekte.

---

### 3. Task-API-Projekt erstellen

Für die zentrale Aufgabenverwaltung wurde ein ASP.NET-Core-Web-API-Projekt erstellt:

```powershell
dotnet new webapi `
  --name TaskFlow.TaskApi `
  --framework net10.0
```

Das Projekt befindet sich unter:

```text
backend/TaskFlow.TaskApi/
```

Die Task API wurde als eigenständiges Webprojekt angelegt und sollte später folgende Verantwortlichkeiten übernehmen:

- Aufgaben laden
- Aufgaben erstellen
- Aufgaben bearbeiten
- Aufgaben löschen
- Eingaben validieren
- SQLite-Persistenz
- Kommunikation mit der Analytics API

Diese fachlichen Funktionen wurden in Schritt 03 noch nicht umgesetzt.

---

### 4. Analytics-API-Projekt erstellen

Für die spätere Statistikberechnung wurde ein zweites ASP.NET-Core-Web-API-Projekt erstellt:

```powershell
dotnet new webapi `
  --name TaskFlow.AnalyticsApi `
  --framework net10.0
```

Das Projekt befindet sich unter:

```text
backend/TaskFlow.AnalyticsApi/
```

Die Analytics API sollte später folgende Aufgaben übernehmen:

- Aufgabendaten von der Task API empfangen
- Basic-Statistiken berechnen
- gewichtete Statistiken berechnen
- Ergebnisse als JSON zurückgeben

Auch diese Statistiklogik war noch nicht Bestandteil dieses Schritts.

---

### 5. Projekte zur Solution hinzufügen

Beide Web-API-Projekte wurden in die gemeinsame Solution aufgenommen:

```powershell
dotnet sln TaskFlow.sln add `
  TaskFlow.TaskApi\TaskFlow.TaskApi.csproj

dotnet sln TaskFlow.sln add `
  TaskFlow.AnalyticsApi\TaskFlow.AnalyticsApi.csproj
```

Die Solution enthielt anschließend:

```text
TaskFlow.sln
├── TaskFlow.TaskApi
└── TaskFlow.AnalyticsApi
```

Dadurch konnten beide Projekte gemeinsam gebaut werden.

---

## Vereinfachung der Template-Projekte

Die vom Web-API-Template erzeugten Beispielinhalte wurden nicht als Grundlage für die spätere Anwendung verwendet.

Insbesondere wurden die standardmäßigen WeatherForecast-Inhalte aus den Einstiegspunkten entfernt:

- WeatherForecast-Endpunkt
- WeatherForecast-Datenmodell
- Beispielarray mit Wetterdaten
- nicht benötigte Beispielausgaben

Anstelle der Template-Endpunkte erhielt jede API zunächst nur einen einfachen Root-Endpunkt.

Die vorhandenen `.http`-Dateien blieben als lokale Request-Dateien im jeweiligen Projekt erhalten und konnten später für manuelle HTTP-Anfragen verwendet werden.

---

## Root-Endpunkt der Task API

In der Task API wurde ein einfacher Root-Endpunkt eingerichtet.

Aufruf:

```text
GET /
```

Antwort:

```text
TaskFlow Task API is running.
```

Dieser Endpunkt diente ausschließlich dazu zu prüfen, ob der Task-API-Prozess korrekt gestartet wurde.

Zu diesem Zeitpunkt existierten noch keine Business-Endpunkte wie:

```text
GET /api/tasks
POST /api/tasks
PUT /api/tasks/{id}
PATCH /api/tasks/{id}/toggle
DELETE /api/tasks/{id}
```

Diese wurden erst in einem späteren Schritt implementiert.

---

## Root-Endpunkt der Analytics API

Auch die Analytics API erhielt zunächst nur einen einfachen Root-Endpunkt.

Aufruf:

```text
GET /
```

Antwort:

```text
TaskFlow Analytics API is running.
```

Der Root-Endpunkt bestätigte, dass die Analytics API unabhängig von der Task API gestartet werden konnte.

Eine Statistikberechnung war zu diesem Zeitpunkt noch nicht vorhanden.

---

## Konfiguration der lokalen Ports

Für beide APIs wurden feste lokale HTTP-Ports über die jeweiligen `launchSettings.json`-Dateien konfiguriert.

| Projekt | Port | Lokale Adresse |
| --- | ---: | --- |
| TaskFlow.TaskApi | `5001` | `http://localhost:5001` |
| TaskFlow.AnalyticsApi | `5002` | `http://localhost:5002` |

Die Trennung der Ports war wichtig, weil beide APIs später gleichzeitig als unabhängige Prozesse laufen sollten.

In diesem Schritt wurde bewusst nur HTTP verwendet.

HTTPS war nicht Bestandteil der lokalen Projektkonfiguration.

---

## APIs lokal starten

### Task API

Die Task API konnte mit folgendem Befehl gestartet werden:

```powershell
dotnet run `
  --project TaskFlow.TaskApi\TaskFlow.TaskApi.csproj
```

Anschließend war der Root-Endpunkt unter folgender Adresse erreichbar:

```text
http://localhost:5001
```

---

### Analytics API

Die Analytics API konnte in einem zweiten Terminal gestartet werden:

```powershell
dotnet run `
  --project TaskFlow.AnalyticsApi\TaskFlow.AnalyticsApi.csproj
```

Anschließend war der Root-Endpunkt unter folgender Adresse erreichbar:

```text
http://localhost:5002
```

Die beiden Prozesse liefen unabhängig voneinander.

---

## Erstellte Projektstruktur

Nach Abschluss dieses Schritts bestand die Backend-Struktur aus:

```text
backend/
├── TaskFlow.sln
├── TaskFlow.TaskApi/
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Program.cs
│   ├── TaskFlow.TaskApi.csproj
│   └── TaskFlow.TaskApi.http
└── TaskFlow.AnalyticsApi/
    ├── Properties/
    │   └── launchSettings.json
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── Program.cs
    ├── TaskFlow.AnalyticsApi.csproj
    └── TaskFlow.AnalyticsApi.http
```

Ordner wie `bin/` und `obj/` wurden nicht dokumentiert, da sie automatisch während des Builds erzeugt werden.

---

## Zugehörige Dateien

### Solution

- [`TaskFlow.sln`](../../backend/TaskFlow.sln)

### Task API

- [`TaskFlow.TaskApi.csproj`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj)
- [`Program.cs`](../../backend/TaskFlow.TaskApi/Program.cs)
- [`appsettings.json`](../../backend/TaskFlow.TaskApi/appsettings.json)
- [`appsettings.Development.json`](../../backend/TaskFlow.TaskApi/appsettings.Development.json)
- [`launchSettings.json`](../../backend/TaskFlow.TaskApi/Properties/launchSettings.json)
- [`TaskFlow.TaskApi.http`](../../backend/TaskFlow.TaskApi/TaskFlow.TaskApi.http)

### Analytics API

- [`TaskFlow.AnalyticsApi.csproj`](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.csproj)
- [`Program.cs`](../../backend/TaskFlow.AnalyticsApi/Program.cs)
- [`appsettings.json`](../../backend/TaskFlow.AnalyticsApi/appsettings.json)
- [`appsettings.Development.json`](../../backend/TaskFlow.AnalyticsApi/appsettings.Development.json)
- [`launchSettings.json`](../../backend/TaskFlow.AnalyticsApi/Properties/launchSettings.json)
- [`TaskFlow.AnalyticsApi.http`](../../backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.http)

---

## Build-Prüfung

Nach Erstellung und Konfiguration der beiden Projekte wurde die vollständige Solution gebaut.

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

- die Solution-Datei gültig ist
- beide Projekte korrekt registriert sind
- beide Projekte mit `net10.0` kompiliert werden können
- keine fehlenden Abhängigkeiten vorhanden sind
- die Grundstruktur technisch funktionsfähig ist

---

## Screenshots und Nachweise

Für diesen Schritt wurde nach der bekannten Projektstruktur kein eigener Screenshot gespeichert.

Der technische Nachweis erfolgt durch:

- den gespeicherten Prompt
- die klassische Solution-Datei
- die beiden Web-API-Projekte
- die konfigurierten Ports
- die Root-Endpunkte
- den erfolgreichen Build mit null Warnungen und null Fehlern

---

## Nicht Bestandteil dieses Schritts

Folgende Funktionen wurden bewusst noch nicht implementiert:

- Aufgabenmodelle
- Aufgaben-CRUD
- Eingabevalidierung
- Repository Pattern
- SQLite-Datenbank
- Task Service
- Analytics-Modelle
- Statistikstrategien
- HTTP-Kommunikation zwischen den APIs
- Dashboard-Endpunkt
- Swagger UI
- CORS-Konfiguration für das Frontend
- Next.js-Frontend

Schritt 03 diente ausschließlich dem Aufbau einer sauberen und startfähigen Backend-Grundstruktur.

---

## Ergebnis

Am Ende dieses Schritts war die initiale Backend-Struktur vollständig vorhanden.

Erstellt wurden:

- eine klassische `TaskFlow.sln`
- ein eigenständiges Task-API-Projekt
- ein eigenständiges Analytics-API-Projekt
- zwei getrennte lokale HTTP-Ports
- ein Root-Endpunkt pro API
- eine gemeinsame Build-Struktur

Beide APIs konnten unabhängig gestartet werden.

Die vollständige Backend-Solution wurde erfolgreich mit folgendem Ergebnis gebaut:

```text
0 Warnung(en)
0 Fehler
```

Damit war die technische Grundlage für die anschließende Implementierung der Task API und der verteilten Analytics-Kommunikation geschaffen.