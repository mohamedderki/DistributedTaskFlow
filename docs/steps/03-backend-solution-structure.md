# Schritt 03 - Backend-Solution-Struktur

## Ziel

Ziel dieses Schritts ist der Aufbau der initialen ASP.NET-Core-Backend-Struktur für DistributedTaskFlow. Dabei werden eine klassische Solution-Datei und zwei minimale Web-API-Projekte erstellt.

In diesem Schritt werden noch keine Anwendungsfunktionen implementiert.

## Ausgangssituation

Vor diesem Schritt waren bereits die Projektplanung, die Systemarchitektur und die Google-Stitch-Designreferenzen dokumentiert.

Relevante Dokumente:

- [Prompt 03](../prompts/03-backend-solution-structure.md)
- [Systemarchitektur](../diagrams/system-architecture.md)
- [Schritt 02 - Projektplanung und Architektur](02-project-planning.md)

Der vorhandene Ordner `backend/` war für die Backend-Struktur vorgesehen.

## Verwendete Technologie

- .NET SDK
- ASP.NET Core Web API
- C#
- klassische `.sln`-Solution-Datei

## Installierte .NET-SDK-Version

Die aktive .NET-SDK-Version ist:

```text
10.0.301
```

Zusätzlich wurden folgende SDKs gefunden:

```text
8.0.408
10.0.203
10.0.301
```

## Erstellte Solution

Die folgende klassische Solution-Datei wurde erstellt:

- `backend/TaskFlow.sln`

Die Solution enthält die beiden Backend-API-Projekte.

## Erstellte API-Projekte

Es wurden zwei minimale ASP.NET-Core-Web-API-Projekte erstellt:

- `backend/TaskFlow.TaskApi/`
- `backend/TaskFlow.AnalyticsApi/`

Beide Projekte verwenden `net10.0` als Zielframework.

## Geplante Verantwortung der Task API

Die Task API soll später für folgende Aufgaben verantwortlich sein:

- Aufgabenverwaltung
- Eingabevalidierung
- SQLite-Persistenz
- Kommunikation mit der Analytics API

Diese Funktionen wurden in diesem Schritt noch nicht implementiert.

Der aktuelle Root-Endpunkt gibt nur folgende Antwort zurück:

```text
TaskFlow Task API is running.
```

## Geplante Verantwortung der Analytics API

Die Analytics API soll später Aufgabenstatistiken berechnen.

Diese Statistiklogik wurde in diesem Schritt noch nicht implementiert.

Der aktuelle Root-Endpunkt gibt nur folgende Antwort zurück:

```text
TaskFlow Analytics API is running.
```

## Konfigurierte Ports

Die lokalen HTTP-Entwicklungsports wurden über die jeweiligen `launchSettings.json`-Dateien konfiguriert:

| Projekt | Port | URL |
| --- | --- | --- |
| TaskFlow.TaskApi | 5001 | `http://localhost:5001` |
| TaskFlow.AnalyticsApi | 5002 | `http://localhost:5002` |

HTTPS wurde in diesem Schritt nicht konfiguriert.

## Entfernte Template-Inhalte

Aus beiden Web-API-Projekten wurden die generierten Beispielinhalte entfernt:

- `WeatherForecast`-Endpoint
- `WeatherForecast`-Record
- Beispiel-Array für Wetterdaten
- generierte `.http`-Beispielrequest-Dateien

Die Projekte enthalten aktuell keine Business-Endpunkte.

## Ausgeführte Build-Prüfung

Die vollständige Backend-Solution wurde mit folgendem Befehl gebaut:

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

- `backend/TaskFlow.sln`
- `backend/TaskFlow.TaskApi/TaskFlow.TaskApi.csproj`
- `backend/TaskFlow.TaskApi/Program.cs`
- `backend/TaskFlow.TaskApi/appsettings.json`
- `backend/TaskFlow.TaskApi/appsettings.Development.json`
- `backend/TaskFlow.TaskApi/Properties/launchSettings.json`
- `backend/TaskFlow.AnalyticsApi/TaskFlow.AnalyticsApi.csproj`
- `backend/TaskFlow.AnalyticsApi/Program.cs`
- `backend/TaskFlow.AnalyticsApi/appsettings.json`
- `backend/TaskFlow.AnalyticsApi/appsettings.Development.json`
- `backend/TaskFlow.AnalyticsApi/Properties/launchSettings.json`
- `docs/steps/03-backend-solution-structure.md`
- `README.md`

## Ergebnis

Die initiale Backend-Solution-Struktur wurde erstellt. Beide ASP.NET-Core-Web-API-Projekte sind in der Solution registriert, starten über jeweils einen einfachen Root-Endpunkt und verwenden die konfigurierten lokalen HTTP-Ports.

Die komplette Backend-Solution wurde erfolgreich gebaut. Es wurden keine Aufgabenverwaltung, keine Statistiklogik, keine SQLite-Anbindung, keine HTTP-Clients und keine zusätzlichen Projekte implementiert.
