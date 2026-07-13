# Google-Stitch-Analyse

## 1. Analysierte Dateien

Für diese Analyse wurden die vorhandenen Google-Stitch-Exporte, die Designbeschreibungen und die visuellen Ergebnisse geprüft. Die Verzeichnisse wurden als Zustände einer gemeinsamen Dashboard-Anwendung betrachtet.

| Bereich | Datei | Ergebnis |
| --- | --- | --- |
| TaskFlow Dashboard | [code.html](../../stitch/TaskFlow%20Dashboard/code.html) | vollständig gelesen |
| TaskFlow Dashboard | [DESIGN.md](../../stitch/TaskFlow%20Dashboard/DESIGN.md) | vollständig gelesen |
| TaskFlow Dashboard | `stitch/TaskFlow Dashboard/screen.png` | nicht vorhanden |
| TaskFlow Dashboard | [ergänzende Screenshot-Referenz](../screenshots/stitch/stitch-02-main-dashboard.png) | inspiziert, da `screen.png` im Stitch-Ordner fehlt |
| Add Task Modal | [code.html](../../stitch/Add%20Task%20Modal/code.html) | vollständig gelesen |
| Add Task Modal | [DESIGN.md](../../stitch/Add%20Task%20Modal/DESIGN.md) | vollständig gelesen |
| Add Task Modal | [screen.png](../../stitch/Add%20Task%20Modal/screen.png) | inspiziert |
| Empty State Dashboard | [code.html](../../stitch/Empty%20State%20Dashboard/code.html) | vollständig gelesen |
| Empty State Dashboard | [DESIGN.md](../../stitch/Empty%20State%20Dashboard/DESIGN.md) | vollständig gelesen |
| Empty State Dashboard | [screen.png](../../stitch/Empty%20State%20Dashboard/screen.png) | inspiziert |
| Loading State Dashboard | [code.html](../../stitch/Loading%20State%20Dashboard/code.html) | vollständig gelesen |
| Loading State Dashboard | [DESIGN.md](../../stitch/Loading%20State%20Dashboard/DESIGN.md) | vollständig gelesen |
| Loading State Dashboard | [screen.png](../../stitch/Loading%20State%20Dashboard/screen.png) | inspiziert |
| Statistics Error State | [code.html](../../stitch/Statistics%20Error%20State/code.html) | vollständig gelesen |
| Statistics Error State | [DESIGN.md](../../stitch/Statistics%20Error%20State/DESIGN.md) | vollständig gelesen |
| Statistics Error State | [screen.png](../../stitch/Statistics%20Error%20State/screen.png) | inspiziert |
| TaskFlow Logo | [DESIGN.md](../../stitch/TaskFlow%20Logo/DESIGN.md) | vollständig gelesen |
| TaskFlow Logo | [logo.png](../../stitch/TaskFlow%20Logo/logo.png) | inspiziert |

Zusätzliche lokale Assets innerhalb der analysierten Stitch-Verzeichnisse wurden nicht gefunden. Die HTML-Dateien verwenden jedoch externe Fonts, Icons und Remote-Bilder.

## 2. Übersicht der Screens

### TaskFlow Dashboard

Zweck: Der Dashboard-Export ist die beste Quelle für die Standardansicht der Anwendung. Er zeigt die geplante Aufgabenoberfläche mit Navigation, Statistikbereich, Fortschritt, Filterleiste und Aufgabenliste.

Sichtbare Bereiche:

- Kopfbereich mit TaskFlow-Logo, Suchfeld, Benachrichtigung und Benutzerbereich.
- Linke Sidebar mit Navigation.
- Dashboard-Header mit Titel `My Tasks`, Datumsanzeige und primärer `Add Task`-Aktion.
- Statistikbereich mit vier Kennzahlen.
- Fortschrittskarte für Workspace Completion.
- Aufgaben-Toolbar mit Statusfiltern und Sortier-/Filteraktionen.
- Aufgabenliste mit offenen und erledigten Aufgaben.
- Mobiler Floating-Action-Button.

Zustand: Standardzustand mit vorhandenen Aufgaben.

Einzigartige Elemente: Vollständige Aufgabenliste, Statusfilter, Fortschrittskarte und kompakte Aufgabenzeilen. Die ergänzende Screenshot-Referenz zeigt diese Struktur visuell, weil die Datei `stitch/TaskFlow Dashboard/screen.png` nicht vorhanden ist.

Duplikate: Sidebar, Header, Statistikbereich, Suchfeld, Aufgabenliste und Add-Task-Aktion tauchen in ähnlicher Form auch in den anderen Dashboard-Zuständen auf.

### Add Task Modal

Zweck: Der Screen zeigt den Modaldialog zum Erstellen einer Aufgabe über der weiterhin sichtbaren Dashboard-Oberfläche.

Sichtbare Bereiche:

- Gedimmter und leicht unscharfer Dashboard-Hintergrund.
- Zentrales Modal mit Titel `Create New Task`.
- Formularfelder für Titel, Priorität und Fälligkeitsdatum.
- Schließen-Icon, `Cancel` und `Save Task`.
- Hintergrund enthält Header, Sidebar, Statistik- und Aufgabenbereiche.

Zustand: Dialogzustand für die Aufgabenerstellung.

Einzigartige Elemente: Modal-Overlay, Formularvalidierung im HTML-Script, Schließen-/Speichern-Aktionen und Default-Datum.

Duplikate: Fast der gesamte Dashboard-Shell wird erneut im HTML ausgegeben. Für React sollte nur ein gemeinsames Layout gerendert werden, über dem `TaskModal` zustandsabhängig erscheint.

### Empty State Dashboard

Zweck: Der Screen zeigt den Dashboard-Zustand ohne vorhandene Aufgaben.

Sichtbare Bereiche:

- Sidebar mit Logo und Navigation.
- Oberer Inhaltsbereich mit Begrüßung und Suchfeld.
- Statistik-Karten mit Nullwerten.
- Großer leerer Aufgabenbereich mit Icon, Text `No tasks yet` und Call-to-Action.
- Zusätzliche Karten wie Upcoming Events, Recent Activity und Pro Tip.

Zustand: Leerer Datenzustand.

Einzigartige Elemente: Empty-State-Illustration, gestrichelte Fläche, CTA `Create Task` und sekundärer Button `Import Tasks`.

Duplikate: Shell, Statistikbereich, Suchfeld und Add-Task-Aktion überschneiden sich mit dem Standarddashboard. Zusatzkarten sind in mehreren Zuständen ähnlich, gehören aber nicht zum Kernumfang der geplanten Anwendung.

### Loading State Dashboard

Zweck: Der Screen zeigt den Ladezustand während Aufgaben- oder Dashboarddaten geladen werden.

Sichtbare Bereiche:

- Sidebar und Header.
- Statistikbereich mit Skeleton-Karten.
- Hauptbereich `Active Tasks` mit Skeleton-Zeilen.
- Ladehinweis mit wechselndem Text.
- Zusätzliche Skeleton-Karten.
- Mobiler Floating-Action-Button.

Zustand: Ladezustand.

Einzigartige Elemente: Skeleton-Flächen, Shimmer-Animation und rotierende Ladehinweise im Script.

Duplikate: Dashboard-Shell, Header, Statistikbereich und Aufgabenbereich entsprechen der gemeinsamen Struktur. Nur Inhalte und Animationen wechseln in den Ladezustand.

### Statistics Error State

Zweck: Der Screen zeigt den Fall, dass Statistikdaten nicht verfügbar sind, während Aufgaben weiterhin nutzbar bleiben.

Sichtbare Bereiche:

- Sidebar, Topbar und Dashboard-Inhalt.
- Fehler-/Hinweisbanner mit Text `Statistics are temporarily unavailable. Your tasks can still be managed.`
- Retry-Aktion.
- Deaktiviert wirkende Statistik-Karten.
- Aufgabenliste bleibt sichtbar und bedienbar.

Zustand: Fehlerzustand der verteilten Statistikkommunikation.

Einzigartige Elemente: Fehlerbanner, Retry-Button, ausgegraute Statistikwerte und Fokus auf robuste Weiterbenutzbarkeit der Aufgabenverwaltung.

Duplikate: Shell, Aufgabenliste, Add-Task-Aktion und mehrere Kartenstrukturen entsprechen den anderen Dashboard-Zuständen.

### TaskFlow Logo

Zweck: Der Logo-Export liefert die visuelle Grundlage für das Branding der Frontend-Anwendung.

Sichtbare Elemente:

- Abstrakte blaue/violette TaskFlow-Marke mit Pfeilbewegung.
- Wortmarke `TaskFlow`.
- Weißer Hintergrund im Export.

Verwendung im Frontend: Das Logo sollte als lokales Asset in `frontend/public/` übernommen und in Header oder Sidebar wiederverwendet werden. Für kleine UI-Bereiche sollte eine zugeschnittene oder skalierte Variante genutzt werden, damit der weiße Hintergrund und die große Exportfläche nicht zu unnötigem Leerraum führen.

## 3. Gemeinsame Anwendungsstruktur

Die Stitch-Exporte zeigen eine gemeinsame Dashboard-Anwendung mit mehreren Zuständen. Sie sollten nicht als unabhängige Next.js-Seiten umgesetzt werden.

Gemeinsame Shell:

- Header oder Topbar mit Suche, Benachrichtigung und Benutzerbereich.
- TaskFlow-Branding mit Logo.
- Sidebar mit Navigation auf Desktop.
- Mobile Navigation oder kompakter Header auf kleinen Viewports.
- Hauptbereich mit Dashboard-Titel und Add-Task-Aktion.
- Statistikbereich.
- Fortschrittsbereich.
- Aufgaben-Toolbar.
- Aufgabenliste.
- Mobiler Add-Button.

Kanonische Quelle: `stitch/TaskFlow Dashboard/code.html` sollte als primäre Strukturquelle verwendet werden. Dieser Export enthält die vollständigste und fachlich passendste Dashboard-Ansicht: Aufgabenliste, Filter, Fortschritt, Statistikwerte und Add-Task-Aktion. Die ergänzende Screenshot-Referenz `docs/screenshots/stitch/stitch-02-main-dashboard.png` bestätigt die visuelle Zielstruktur, obwohl `stitch/TaskFlow Dashboard/screen.png` fehlt.

Die anderen Screens liefern zustandsabhängige Varianten:

- `Add Task Modal`: Overlay über derselben Shell.
- `Empty State Dashboard`: Ersatzinhalt für eine leere Aufgabenliste.
- `Loading State Dashboard`: Skeleton-Variante von Statistik- und Aufgabenbereichen.
- `Statistics Error State`: Fehlerbanner und degradierte Statistikdarstellung bei nicht erreichbarer Analytics API.

## 4. Zustandsabhängige Bereiche

### Default Dashboard

Spezifisch für den Standardzustand:

- Echte Statistikwerte.
- Fortschrittskarte.
- Aufgabenliste mit Prioritäten, Fälligkeitsdaten und Status.
- Statusfilter `All`, `Open`, `Completed`.

Gemeinsam sichtbar: Header, Sidebar, Dashboard-Header, Add-Task-Aktion, Statistikbereich, Toolbar und Aufgabenbereich.

### Add-Task-Modal

Spezifisch für den Modalzustand:

- Overlay.
- Dialogkarte.
- Formularfelder für Titel, Priorität und Fälligkeitsdatum.
- Schließen-, Abbrechen- und Speichern-Aktion.

Gemeinsam sichtbar: Die Dashboard-Shell bleibt im Hintergrund sichtbar. In React sollte sie nicht dupliziert werden.

### Empty State

Spezifisch für den Empty State:

- Empty-State-Icon.
- Hinweistext für fehlende Aufgaben.
- CTA zum Erstellen einer Aufgabe.
- Optionaler Import-Button aus Stitch, der nicht zur geplanten Funktionalität gehört.

Gemeinsam sichtbar: Header, Sidebar, Dashboard-Header und Statistikbereich.

### Loading State

Spezifisch für den Loading State:

- Skeleton-Karten.
- Skeleton-Zeilen für Aufgaben.
- Shimmer-Animation.
- Ladehinweis.

Gemeinsam sichtbar: Layout, Header, Sidebar und die Positionen der späteren Inhalte.

### Statistics Error State

Spezifisch für den Statistikfehler:

- Fehlerbanner mit Retry-Aktion.
- Ausgegraute oder leere Statistikwerte.
- Hinweis, dass Aufgaben weiterhin verwaltet werden können.

Gemeinsam sichtbar: Aufgabenliste, Task-Toolbar, Add-Task-Aktion, Header und Sidebar.

## 5. Komponentenplan

### Layout-Komponenten

| Komponente | Verantwortung | Stitch-Quelle | Wiederverwendung | Erwartete Children | Erwartete Props | Stylesheet |
| --- | --- | --- | --- | --- | --- | --- |
| `DashboardLayout` | Gemeinsame Seitenstruktur mit Sidebar, Header und Hauptbereich | TaskFlow Dashboard, alle Zustands-Screens | wiederverwendbar | `Header`, `Sidebar`, Dashboard-Content | `children`, `isLoading`, `activeNavItem` | `styles/layout/DashboardLayout.module.css` |
| `Header` | Topbar mit Suche, Branding auf kleinen Viewports, Benutzer- und Icon-Aktionen | TaskFlow Dashboard, Empty, Loading, Error | wiederverwendbar | optional Suchfeld, Aktionsicons | `searchValue`, `onSearchChange`, `userName` | `styles/layout/Header.module.css` |
| `Sidebar` | Desktop-Navigation und Branding | alle Dashboard-Screens | wiederverwendbar | Navigationseinträge | `activeItem` | `styles/layout/Sidebar.module.css` |

### Dashboard-Komponenten

| Komponente | Verantwortung | Stitch-Quelle | Wiederverwendung | Erwartete Children | Erwartete Props | Stylesheet |
| --- | --- | --- | --- | --- | --- | --- |
| `DashboardHeader` | Seitentitel, Datum und primäre Add-Task-Aktion | TaskFlow Dashboard | wiederverwendbar | Button oder Aktionsslot | `title`, `subtitle`, `onAddTask` | `styles/dashboard/DashboardHeader.module.css` |
| `StatisticsPanel` | Layout für Statistik-Karten und Fehler-/Ladezustände | TaskFlow Dashboard, Loading, Error | wiederverwendbar | mehrere `StatisticCard` | `statistics`, `isLoading`, `error`, `onRetry` | `styles/dashboard/StatisticsPanel.module.css` |
| `StatisticCard` | Einzelne Kennzahl mit Icon, Label, Wert und optionaler Entwicklung | TaskFlow Dashboard, Empty, Loading, Error | wiederverwendbar | Icon | `label`, `value`, `description`, `tone`, `isLoading`, `isDisabled` | `styles/dashboard/StatisticCard.module.css` |
| `ProgressCard` | Fortschrittsanzeige, z. B. Completion Percentage oder gewichteter Score | TaskFlow Dashboard | wiederverwendbar | Progress-Bar | `label`, `value`, `percentage`, `caption` | `styles/dashboard/ProgressCard.module.css` |
| `TaskToolbar` | Statusfilter, Suche oder Sortier-/Filter-Aktionen | TaskFlow Dashboard | wiederverwendbar | Filterbuttons | `status`, `search`, `onStatusChange`, `onSearchChange` | `styles/dashboard/TaskToolbar.module.css` |
| `TaskList` | Rendert Aufgaben, Empty State oder Loading State | TaskFlow Dashboard, Empty, Loading | wiederverwendbar | `TaskCard`, `EmptyState`, `LoadingState` | `tasks`, `isLoading`, `status`, `search` | `styles/dashboard/TaskList.module.css` |
| `TaskCard` | Darstellung einer Aufgabe mit Titel, Priorität, Fälligkeit, Status und Aktionen | TaskFlow Dashboard, Error | wiederverwendbar | Aktionsbuttons | `task`, `onToggle`, `onEdit`, `onDelete` | `styles/dashboard/TaskCard.module.css` |
| `MobileAddButton` | Floating-Action-Button für kleine Viewports | TaskFlow Dashboard, Empty, Loading | wiederverwendbar | Icon | `onClick`, `label` | `styles/dashboard/MobileAddButton.module.css` |

### Modal-Komponente

| Komponente | Verantwortung | Stitch-Quelle | Wiederverwendung | Erwartete Children | Erwartete Props | Stylesheet |
| --- | --- | --- | --- | --- | --- | --- |
| `TaskModal` | Formular zum Erstellen und später Bearbeiten einer Aufgabe | Add Task Modal | zustandsabhängig, aber wiederverwendbar für Create/Edit | Formularfelder, Buttonleiste | `mode`, `task`, `isOpen`, `onClose`, `onSubmit`, `validationError` | `styles/modal/TaskModal.module.css` |

### Zustands-Komponenten

| Komponente | Verantwortung | Stitch-Quelle | Wiederverwendung | Erwartete Children | Erwartete Props | Stylesheet |
| --- | --- | --- | --- | --- | --- | --- |
| `EmptyState` | Anzeige bei leerer Aufgabenliste | Empty State Dashboard | zustandsabhängig | CTA-Button | `title`, `message`, `onCreateTask` | `styles/states/EmptyState.module.css` |
| `LoadingState` | Skeletons für Statistik- und Aufgabenbereiche | Loading State Dashboard | zustandsabhängig | Skeleton-Zeilen | `variant`, `message` | `styles/states/LoadingState.module.css` |
| `StatisticsError` | Hinweis bei nicht verfügbarer Analytics API | Statistics Error State | zustandsabhängig | Retry-Button | `message`, `onRetry` | `styles/states/StatisticsError.module.css` |

## 6. Design-System und CSS-Tokens

Die DESIGN.md-Dateien enthalten ein konsistentes System aus Material-ähnlichen Farbrollen, Inter-Typografie, abgerundeten Formen, Schatten und Spacing. Die HTML-Dateien konkretisieren dieses System über Tailwind-Konfiguration und Utility-Klassen.

Wichtige Beobachtungen:

- Hintergrundfarbe im Code: `#f8f9ff`.
- Primärfarbe: `#3525cd`.
- Primärcontainer und aktive Elemente: `#e2dfff`.
- Textfarbe: `#0b1c30`.
- Gedämpfte Textfarbe: `#464555`.
- Fehlerfarbe: `#ba1a1a`.
- Fehlercontainer: `#ffdad6`.
- Erfolgsfarbe in den Screens: vor allem `#16a34a` beziehungsweise grüne Tailwind-Klassen.
- Prioritäten verwenden Rot für High, Amber/Gelb für Medium und Slate/Grau für Low.
- Karten sind überwiegend weiß mit feinen Rändern und sehr subtilen Schatten.
- Buttons und Inputs verwenden meist 8px Radius; Karten und Modalflächen meist 12px.
- Die DESIGN.md-Radien weichen teilweise von der Tailwind-Konfiguration im HTML ab. Für die Umsetzung sollten die tatsächlich sichtbaren HTML-Werte priorisiert werden.

Vorgeschlagene CSS Custom Properties für `frontend/styles/tokens.css`:

```css
:root {
  --color-background: #f8f9ff;
  --color-surface: #f8f9ff;
  --color-surface-lowest: #ffffff;
  --color-surface-low: #eff4ff;
  --color-surface-container: #e5eeff;
  --color-surface-high: #dce9ff;
  --color-surface-highest: #d3e4fe;
  --color-surface-variant: #d3e4fe;

  --color-text: #0b1c30;
  --color-text-muted: #464555;
  --color-outline: #777587;
  --color-outline-variant: #c7c4d8;

  --color-primary: #3525cd;
  --color-primary-container: #4f46e5;
  --color-primary-soft: #e2dfff;
  --color-primary-soft-dim: #c3c0ff;
  --color-on-primary: #ffffff;

  --color-secondary: #565e74;
  --color-secondary-container: #dae2fd;
  --color-tertiary: #7e3000;

  --color-error: #ba1a1a;
  --color-error-container: #ffdad6;
  --color-success: #16a34a;

  --color-priority-high-bg: #fef2f2;
  --color-priority-high-text: #b91c1c;
  --color-priority-medium-bg: #fffbeb;
  --color-priority-medium-text: #b45309;
  --color-priority-low-bg: #f1f5f9;
  --color-priority-low-text: #64748b;

  --font-family-base: "Inter", system-ui, sans-serif;
  --font-size-display: 36px;
  --line-height-display: 44px;
  --font-weight-display: 700;
  --font-size-headline-lg: 28px;
  --line-height-headline-lg: 36px;
  --font-weight-headline: 600;
  --font-size-headline-md: 20px;
  --line-height-headline-md: 28px;
  --font-size-body-lg: 16px;
  --line-height-body-lg: 24px;
  --font-size-body-md: 14px;
  --line-height-body-md: 20px;
  --font-size-label-md: 12px;
  --line-height-label-md: 16px;
  --font-size-label-sm: 11px;
  --line-height-label-sm: 14px;
  --font-weight-label: 600;

  --space-1: 4px;
  --space-2: 8px;
  --space-3: 12px;
  --space-4: 16px;
  --space-6: 24px;
  --space-8: 32px;
  --space-10: 40px;

  --radius-sm: 4px;
  --radius-md: 8px;
  --radius-lg: 12px;
  --radius-pill: 9999px;

  --shadow-card: 0 1px 2px rgb(15 23 42 / 0.05);
  --shadow-floating: 0 10px 25px rgb(15 23 42 / 0.18);

  --layout-max-width: 1440px;
  --sidebar-width: 256px;
  --header-height: 64px;

  --transition-fast: 150ms ease;
  --transition-base: 200ms ease;
}
```

## 7. Responsive Verhalten

Die Stitch-HTML-Dateien verwenden Tailwind-Breakpoints:

- `sm`: ab 640px.
- `md`: ab 768px.
- `lg`: ab 1024px.
- `xl`: ab 1280px.

Desktop:

- Sidebar mit 256px Breite.
- Hauptinhalt mit linker Einrückung oder flexiblem Layout neben der Sidebar.
- Statistikbereich als vier Spalten.
- Aufgabenzeilen horizontal mit Status, Priorität, Datum und Aktionen.
- Header mit Suche, Icons und Benutzerbereich.

Tablet:

- Statistikbereich meist zwei Spalten.
- Sidebar-Verhalten ist inkonsistent: einige Exporte zeigen sie ab `md`, der Hauptdashboard-Export erst ab `lg`.
- Für die Umsetzung sollte eine klare Entscheidung getroffen werden: Sidebar ab `1024px`, darunter kompakter Header.

Mobile:

- Sidebar wird ausgeblendet.
- Statistik-Karten stapeln sich.
- Aufgabenliste muss vertikal lesbar werden.
- Toolbar sollte Filter und Suche untereinander oder als scrollbare Segmentsteuerung darstellen.
- Floating-Add-Button wird sichtbar.
- Modal sollte mit seitlichem Abstand von 16px fast die volle Breite nutzen.

Vorgeschlagene Plain-CSS-Media-Queries:

```css
@media (max-width: 767px) {
  /* Mobile: keine Sidebar, gestapelte Karten, Floating Add Button */
}

@media (min-width: 768px) and (max-width: 1023px) {
  /* Tablet: zwei Statistikspalten, kompakter Inhaltsbereich */
}

@media (min-width: 1024px) {
  /* Desktop: Sidebar, vier Statistikspalten, horizontale Aufgabenzeilen */
}

@media (min-width: 1280px) {
  /* Breite Desktop-Ansicht mit maximaler Content-Breite */
}
```

## 8. Interaktionen und Animationen

In Stitch vorhandene Interaktionen:

- Add-Task-Aktion öffnet im Modal-Export den Dialog.
- Modal kann geschlossen werden.
- Formularvalidierung prüft Titel und Datum im Script des Modal-Exports.
- Speichern wird im Stitch-Script simuliert.
- Task-Karten haben Hover-Zustände.
- Suchfeld im Dashboard ändert beim Fokus teilweise die Breite.
- Empty-State-Buttons haben kleine Druckanimationen.
- Loading-State nutzt Skeleton-Shimmer und wechselnde Ladehinweise.
- Statistics-Error-State enthält Retry-Aktion.
- Checkboxen können im Error-State visuell erledigte Aufgaben markieren.

Später in der Anwendung erforderliche Interaktionen:

- Aufgabe erstellen.
- Aufgabe bearbeiten.
- Aufgabe löschen.
- Aufgabe als erledigt/offen umschalten.
- Aufgaben nach Status filtern.
- Aufgaben nach Titel suchen.
- Dashboard-Statistik mit `basic` oder `weighted` Strategie laden.
- Retry bei nicht verfügbarer Analytics API.
- Formularfehler aus Frontend- und Backend-Validierung anzeigen.
- Fokuszustände für Tastaturbedienung erhalten.

Nicht direkt übernehmen:

- Simulierte Alerts und Reloads aus Stitch.
- Nicht standardkonforme oder fragile Selektoren aus Inline-Scripts.
- Reine Demo-Aktionen wie `Import Tasks`, `Upgrade Plan`, Team- oder Kalenderfunktionen.

## 9. Tailwind- und HTML-Konvertierung

Die Stitch-Exporte sind statische HTML-Dateien mit Tailwind CDN, eingebetteter Tailwind-Konfiguration, Material-Symbol-Font, Google-Font-Importen, Inline-Styles und kleinen Scripts.

Konvertierung in die Next.js-Struktur:

- Tailwind CDN nicht übernehmen.
- Utility-Klassen in semantische JSX-Strukturen und CSS Modules übersetzen.
- Globale Basiswerte in `globals.css` belassen.
- Designwerte in `tokens.css` sammeln.
- Layoutbezogene Styles in `styles/layout/`.
- Dashboard-Styles in `styles/dashboard/`.
- Modal-Styles in `styles/modal/`.
- Zustands-Styles in `styles/states/`.
- Wiederholte Full-Page-Shells zu `DashboardLayout`, `Header` und `Sidebar` zusammenführen.
- Inline-Scripts durch React-State, Props und Event-Handler ersetzen.
- Remote-Bilder durch lokale Assets oder stabile Framework-Assets ersetzen.

Kategorien:

- Tailwind-Konfiguration: in CSS Custom Properties übertragen.
- Utility-Klassen: in sprechende CSS-Klassen in CSS Modules umwandeln.
- Embedded Styles: auf globale Basisstyles, Komponentenstyles und Animationsstyles aufteilen.
- Externe Fonts: später kontrolliert über Next.js oder CSS laden.
- Icons: einheitliche Icon-Strategie wählen; nicht mehrfach denselben Material-Symbol-Link einbinden.
- Scripts: nicht kopieren, sondern als React-Logik neu modellieren.
- Doppelte Markup-Blöcke: in React-Komponenten deduplizieren.

## 10. Externe Abhängigkeiten und Assets

Gefundene externe Abhängigkeiten:

- Google Fonts `Inter`.
- Google Material Symbols Outlined.
- Tailwind CDN mit Forms- und Container-Queries-Plugins.
- Remote-Bilder über `googleusercontent.com`.
- Inline-JavaScript in den HTML-Dateien.

Empfehlung:

- Inter kann beibehalten werden, sollte aber später über den Next.js-Mechanismus oder einen kontrollierten CSS-Import eingebunden werden.
- Material Symbols können für den Prototyp beibehalten werden, sollten aber nur einmal global geladen werden. Alternativ kann später eine React-Icon-Lösung genutzt werden, wenn sie im Projekt bewusst eingeführt wird.
- Tailwind nicht verwenden, da die geplante Frontend-Umsetzung plain CSS und CSS Modules vorsieht.
- Remote-Bilder vermeiden. Benutzeravatar und dekorative Remote-Bilder sind für den geplanten Funktionsumfang nicht notwendig.
- Das Logo aus [logo.png](../../stitch/TaskFlow%20Logo/logo.png) sollte lokal in `frontend/public/` übernommen werden.
- Zusätzliche Demo-Grafiken wie Pro-Tip- oder Team-Karten sollten nicht umgesetzt werden, solange sie nicht zur geplanten Aufgabenverwaltung gehören.

## 11. Inkonsistenzen und Bereinigungen

| Inkonsistenz | Beobachtung | Frontend-Entscheidung |
| --- | --- | --- |
| Fehlendes Dashboard-Bild | `stitch/TaskFlow Dashboard/screen.png` fehlt. | Code und DESIGN.md als Quelle nutzen, ergänzende vorhandene Screenshot-Referenz nur als visuelle Bestätigung verwenden. |
| Unterschiedliche Sidebar-Breakpoints | Manche Screens zeigen Sidebar ab `md`, andere ab `lg`. | Einheitlich Sidebar ab `1024px`; darunter kompakter Header und mobiler Add-Button. |
| Unterschiedliche aktive Navigation | `My Tasks`, `Overview` und weitere Einträge variieren. | Für den MVP die Aufgabenansicht als aktive Hauptansicht verwenden. |
| Unterschiedliche Statistiklabels | Screens mischen Total, Open, Completed, Overdue, Productivity und In Progress. | An Backend-Modell ausrichten: TotalTasks, OpenTasks, CompletedTasks, OverdueTasks, CompletionPercentage, WeightedOpenScore. |
| Unterschiedliche Button-Radien | Buttons nutzen `rounded-lg`, Empty-State teils `rounded-xl`. | Buttons 8px, Karten und Modal 12px. |
| Remote-Avatare | Mehrere Screens verwenden unterschiedliche externe Avatare. | Für den MVP weglassen oder durch lokale Platzhalter ersetzen. |
| Doppelte Full-Page-Shells | Jeder Zustand enthält komplette Layout-Struktur. | Shell einmal in `DashboardLayout` rendern, Zustände als Teilbereiche austauschen. |
| Doppelte Font-Links | Material Symbols wird mehrfach eingebunden. | Nur einmal global laden oder durch lokales Icon-Konzept ersetzen. |
| Demo-Funktionen | Import, Team, Calendar, Upgrade Plan und Focus Mode sind nicht Teil der geplanten Features. | Nicht in den funktionalen MVP übernehmen. |
| Fragile Inline-Scripts | Scripts simulieren Verhalten oder enthalten ungeeignete Selektoren. | Nicht übernehmen; React-Events und API-Zustände verwenden. |
| Unterschiedliche Header-Varianten | Logo, Suche und Benutzerbereich variieren je Screen. | Einen gemeinsamen Header aus dem Hauptdashboard ableiten. |

## 12. Empfohlene Frontend-Struktur

Die folgende Struktur ist eine Empfehlung für den nächsten Umsetzungsschritt. Sie wurde in dieser Aufgabe nicht erstellt oder verändert.

```text
frontend/
├── app/
│   ├── layout.jsx
│   └── page.jsx
├── components/
│   ├── layout/
│   │   ├── DashboardLayout.jsx
│   │   ├── Header.jsx
│   │   └── Sidebar.jsx
│   ├── dashboard/
│   │   ├── DashboardHeader.jsx
│   │   ├── StatisticsPanel.jsx
│   │   ├── StatisticCard.jsx
│   │   ├── ProgressCard.jsx
│   │   ├── TaskToolbar.jsx
│   │   ├── TaskList.jsx
│   │   ├── TaskCard.jsx
│   │   └── MobileAddButton.jsx
│   ├── modal/
│   │   └── TaskModal.jsx
│   └── states/
│       ├── EmptyState.jsx
│       ├── LoadingState.jsx
│       └── StatisticsError.jsx
├── styles/
│   ├── globals.css
│   ├── tokens.css
│   ├── layout/
│   │   ├── DashboardLayout.module.css
│   │   ├── Header.module.css
│   │   └── Sidebar.module.css
│   ├── dashboard/
│   │   ├── DashboardHeader.module.css
│   │   ├── StatisticsPanel.module.css
│   │   ├── StatisticCard.module.css
│   │   ├── ProgressCard.module.css
│   │   ├── TaskToolbar.module.css
│   │   ├── TaskList.module.css
│   │   ├── TaskCard.module.css
│   │   └── MobileAddButton.module.css
│   ├── modal/
│   │   └── TaskModal.module.css
│   ├── states/
│   │   ├── EmptyState.module.css
│   │   ├── LoadingState.module.css
│   │   └── StatisticsError.module.css
│   └── pages/
│       └── home.module.css
└── public/
    └── taskflow-logo.png
```

## 13. Zuordnung zwischen Stitch und React

| React-Komponente | Stitch-Quelle | Quellelement oder Bereich | Typ | Ziel-CSS-Modul |
| --- | --- | --- | --- | --- |
| `DashboardLayout` | TaskFlow Dashboard | Page Wrapper, Sidebar, Main | geteilt | `styles/layout/DashboardLayout.module.css` |
| `Header` | TaskFlow Dashboard, Empty, Loading, Error | Topbar, Suche, User Actions | geteilt | `styles/layout/Header.module.css` |
| `Sidebar` | alle Dashboard-Screens | Navigation, Logo, Desktop Shell | geteilt | `styles/layout/Sidebar.module.css` |
| `DashboardHeader` | TaskFlow Dashboard | `My Tasks`, Datum, Add Task | geteilt | `styles/dashboard/DashboardHeader.module.css` |
| `StatisticsPanel` | TaskFlow Dashboard, Loading, Error | Statistik-Kartenbereich | geteilt mit Zuständen | `styles/dashboard/StatisticsPanel.module.css` |
| `StatisticCard` | TaskFlow Dashboard, Empty, Loading, Error | einzelne Statistik-Karte | geteilt | `styles/dashboard/StatisticCard.module.css` |
| `ProgressCard` | TaskFlow Dashboard | Workspace Completion | geteilt | `styles/dashboard/ProgressCard.module.css` |
| `TaskToolbar` | TaskFlow Dashboard | Statusfilter, Such-/Sortierbereich | geteilt | `styles/dashboard/TaskToolbar.module.css` |
| `TaskList` | TaskFlow Dashboard, Empty, Loading | Aufgabenlistenbereich | geteilt mit Zuständen | `styles/dashboard/TaskList.module.css` |
| `TaskCard` | TaskFlow Dashboard, Statistics Error State | Aufgabe mit Status, Priorität, Aktionen | geteilt | `styles/dashboard/TaskCard.module.css` |
| `MobileAddButton` | TaskFlow Dashboard, Empty, Loading | Floating Add Button | geteilt | `styles/dashboard/MobileAddButton.module.css` |
| `TaskModal` | Add Task Modal | Modal Overlay und Formular | zustandsspezifisch | `styles/modal/TaskModal.module.css` |
| `EmptyState` | Empty State Dashboard | leere Aufgabenfläche | zustandsspezifisch | `styles/states/EmptyState.module.css` |
| `LoadingState` | Loading State Dashboard | Skeletons und Ladehinweise | zustandsspezifisch | `styles/states/LoadingState.module.css` |
| `StatisticsError` | Statistics Error State | Fehlerbanner und Retry | zustandsspezifisch | `styles/states/StatisticsError.module.css` |

## 14. Implementierungsreihenfolge

Empfohlene Reihenfolge für den nächsten Schritt:

1. Design-Tokens und globale Basisstyles aus der Stitch-Konfiguration ableiten.
2. Gemeinsames Layout mit `DashboardLayout`, `Header` und `Sidebar` erstellen.
3. Dashboard-Grundstruktur mit `DashboardHeader`, `StatisticsPanel`, `ProgressCard` und `TaskToolbar` umsetzen.
4. Aufgabenliste mit `TaskList` und `TaskCard` auf Basis statischer Beispieldaten bauen.
5. `MobileAddButton` und responsive Regeln ergänzen.
6. `TaskModal` als reinen UI-Zustand ohne Backend-Integration erstellen.
7. `EmptyState`, `LoadingState` und `StatisticsError` als austauschbare Zustände integrieren.
8. Doppelte oder nicht geplante Stitch-Elemente auslassen.
9. Erst nach stabiler UI-Struktur API-Aufrufe, Anwendungszustand und CRUD-Logik verbinden.

## 15. Abgrenzung zum nächsten Schritt

Diese Aufgabe hat ausschließlich die Google-Stitch-Dateien analysiert und eine Umsetzungsgrundlage dokumentiert.

Nicht Teil dieser Aufgabe:

- React-Implementierung.
- CSS-Implementierung.
- Backend-Integration.
- API-Aufrufe.
- Application State Management.
- CRUD-Funktionalität.
- Installation zusätzlicher Pakete.
- Änderungen in `frontend/`, `backend/` oder `stitch/`.

Überprüfung:

- Alle vorhandenen `code.html`-Dateien wurden vollständig gelesen.
- Alle vorhandenen `DESIGN.md`-Dateien wurden vollständig gelesen.
- Alle vorhandenen `screen.png`-Dateien und `logo.png` wurden inspiziert.
- `stitch/TaskFlow Dashboard/screen.png` war nicht vorhanden und konnte deshalb nicht inspiziert werden.
- `frontend/` wurde nicht geändert.
- `backend/` wurde nicht geändert.
- `stitch/` wurde nicht geändert.
- Als Ausgabe dieser Aufgabe wurde nur `docs/frontend/stitch-analysis.md` erstellt.

Inspizierte Dateien:

- [stitch/TaskFlow Dashboard/code.html](../../stitch/TaskFlow%20Dashboard/code.html)
- [stitch/TaskFlow Dashboard/DESIGN.md](../../stitch/TaskFlow%20Dashboard/DESIGN.md)
- [docs/screenshots/stitch/stitch-02-main-dashboard.png](../screenshots/stitch/stitch-02-main-dashboard.png)
- [stitch/Add Task Modal/code.html](../../stitch/Add%20Task%20Modal/code.html)
- [stitch/Add Task Modal/DESIGN.md](../../stitch/Add%20Task%20Modal/DESIGN.md)
- [stitch/Add Task Modal/screen.png](../../stitch/Add%20Task%20Modal/screen.png)
- [stitch/Empty State Dashboard/code.html](../../stitch/Empty%20State%20Dashboard/code.html)
- [stitch/Empty State Dashboard/DESIGN.md](../../stitch/Empty%20State%20Dashboard/DESIGN.md)
- [stitch/Empty State Dashboard/screen.png](../../stitch/Empty%20State%20Dashboard/screen.png)
- [stitch/Loading State Dashboard/code.html](../../stitch/Loading%20State%20Dashboard/code.html)
- [stitch/Loading State Dashboard/DESIGN.md](../../stitch/Loading%20State%20Dashboard/DESIGN.md)
- [stitch/Loading State Dashboard/screen.png](../../stitch/Loading%20State%20Dashboard/screen.png)
- [stitch/Statistics Error State/code.html](../../stitch/Statistics%20Error%20State/code.html)
- [stitch/Statistics Error State/DESIGN.md](../../stitch/Statistics%20Error%20State/DESIGN.md)
- [stitch/Statistics Error State/screen.png](../../stitch/Statistics%20Error%20State/screen.png)
- [stitch/TaskFlow Logo/DESIGN.md](../../stitch/TaskFlow%20Logo/DESIGN.md)
- [stitch/TaskFlow Logo/logo.png](../../stitch/TaskFlow%20Logo/logo.png)

Erstellte Dokumentationsdatei:

- [docs/frontend/stitch-analysis.md](stitch-analysis.md)

Nicht verfügbare oder nicht lesbare Assets:

- `stitch/TaskFlow Dashboard/screen.png` ist im Repository nicht vorhanden.

Es wurde kein Anwendungscode geändert.
