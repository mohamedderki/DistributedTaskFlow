# Schritt 02 – Projektplanung und Architektur

## Ziel

Ziel dieses Schritts ist es, die geplante Systemarchitektur von DistributedTaskFlow zu beschreiben und die Verantwortlichkeiten der einzelnen Module festzulegen.

Die Anwendung wird als kleine verteilte Webanwendung aufgebaut. Frontend, Aufgabenverwaltung, Statistikberechnung und Datenhaltung werden klar voneinander getrennt.

In diesem Schritt wird noch kein Anwendungscode implementiert.

## Zugehörige Dokumente

Zu diesem Planungsschritt gehören zwei zusätzliche Dokumente:

- [Verwendeten Codex-Prompt öffnen](../prompts/02-project-planning.md)
- [Systemarchitektur und Architekturdiagramm öffnen](../diagrams/system-architecture.md)

Der Prompt dokumentiert die Anweisung, die für diesen Schritt an Codex übergeben wurde.

Das Architekturdokument enthält das vollständige Systemdiagramm und stellt die Kommunikation zwischen den geplanten Modulen grafisch dar.

## Ausgangssituation

Im vorherigen Schritt wurde die Benutzeroberfläche mit Google Stitch entworfen.

Dabei wurden folgende Inhalte erstellt:

- Google-Stitch-Prompt
- HTML-Prototypen
- eingebettete Styles
- Design-Dokumentationen
- Screenshots
- TaskFlow-Logo

Diese Dateien bleiben unverändert und dienen später als visuelle und technische Referenz für die Umsetzung des Next.js-Frontends.

Die Dokumentation des vorherigen Schritts befindet sich hier:

[Schritt 01 – UI-Design mit Google Stitch öffnen](./01-google-stitch-design.md)

Die eigentlichen Next.js- und ASP.NET-Core-Projekte werden erst in den folgenden Entwicklungsschritten erstellt.

## Geplante Module

| Modul | Technologie | Verantwortung |
| --- | --- | --- |
| Browser | Webbrowser | Darstellung und Bedienung der Webanwendung |
| Next.js Frontend | Next.js, React, JavaScript, JSX, Plain CSS | Benutzeroberfläche und Kommunikation mit der Task API |
| Task API | ASP.NET Core | Aufgabenverwaltung, Validierung und SQLite-Persistenz |
| Analytics API | ASP.NET Core | Berechnung der Aufgabenstatistiken |
| Datenbank | SQLite | Dauerhafte Speicherung der Aufgaben |

## Verantwortlichkeiten der Module

### Browser

Der Browser stellt die Laufzeitumgebung für die Benutzeroberfläche bereit.

Der Benutzer öffnet und bedient die TaskFlow-Webanwendung innerhalb des Browsers.

### Next.js Frontend

Das Frontend stellt die Benutzeroberfläche der Anwendung bereit.

Zu seinen Aufgaben gehören:

- Aufgaben anzeigen
- neue Aufgaben erfassen
- Aufgaben als erledigt oder offen markieren
- Aufgaben löschen
- Aufgaben nach Status filtern
- Aufgaben durchsuchen
- Statistiken darstellen
- Ladezustände anzeigen
- Leerzustände anzeigen
- Fehlerzustände darstellen

Das Frontend greift nicht direkt auf die Datenbank zu.

Es kommuniziert ausschließlich über HTTP und JSON mit der Task API.

### Task API

Die Task API übernimmt die fachliche Verwaltung der Aufgaben.

Zu ihren Aufgaben gehören:

- Aufgaben abrufen
- Aufgaben erstellen
- Eingaben validieren
- Aufgaben als erledigt oder offen markieren
- Aufgaben löschen
- Aufgabendaten in SQLite speichern
- gespeicherte Aufgaben aus SQLite lesen
- Statistikberechnungen bei der Analytics API anfordern

Der Datenbankzugriff wird durch eine Repository-Schicht gekapselt.

Die Task API stellt die zentrale Schnittstelle zwischen dem Frontend, der Datenbank und der Analytics API dar.

### Analytics API

Die Analytics API ist ausschließlich für die Berechnung von Aufgabenstatistiken verantwortlich.

Sie berechnet unter anderem:

- Gesamtanzahl der Aufgaben
- Anzahl offener Aufgaben
- Anzahl erledigter Aufgaben
- Anzahl überfälliger Aufgaben
- Abschlussquote
- gewichtete Auswertung nach Aufgabenpriorität

Die Analytics API speichert selbst keine Aufgaben.

Sie erhält die benötigten Aufgabendaten über HTTP und JSON von der Task API und sendet die berechneten Ergebnisse zurück.

### SQLite

SQLite speichert die Aufgabendaten dauerhaft.

Der Zugriff auf die Datenbank erfolgt ausschließlich über die Task API.

Das Frontend und die Analytics API greifen nicht direkt auf SQLite zu.

## Kommunikationswege

Die Hauptkommunikation der Anwendung erfolgt in folgender Reihenfolge:

```text
Benutzer
→ Browser
→ Next.js Frontend
→ Task API
→ Analytics API