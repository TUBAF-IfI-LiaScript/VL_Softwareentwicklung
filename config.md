<!--
author:   Sebastian Zug, Volker Göhler

email:    sebastian.zug@informatik.tu-freiberg.de, volker.goehler@informatik.tu-freiberg.de

version:  0.0.5
icon: https://upload.wikimedia.org/wikipedia/commons/e/e8/TUBAF_Logo.svg
comment:  This file provides commonly used meta information for all LiaScript courses in the folder

@config.semester: `Sommersemester 2026`
@config.university: `Technische Universität Freiberg`

-->

# Config variables

```
@config.semester:   `Sommersemester 2026`
@config.university: `Technische Universität Freiberg`
```

# Layout-Hilfsklassen (Flexbox)

Die Flexbox-Klassen `flex-container` / `flex-child` werden über die zentrale
CSS-Datei [`css/styles.css`](css/styles.css) bereitgestellt. Damit sie wirken,
muss die Vorlesung diese CSS-Datei per `link:`-Direktive im Kopf einbinden:

```text
link: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/css/styles.css
```

> **Hinweis:** `@style`-Blöcke werden über `import:` **nicht** zuverlässig in die
> importierende Vorlesung übernommen — CSS muss daher per `link:` direkt in der
> jeweiligen Vorlesung eingebunden werden.

Verwendung im Dokument:

```html
<section class="flex-container">

<div class="flex-child" style="min-width: 300px">

... Inhalt linke Spalte (Code, Text, Diagramm) ...

</div>

<div class="flex-child" style="min-width: 300px">

... Inhalt rechte Spalte ...

</div>

</section>
```

Die Klassen `flex-child-1` bis `flex-child-8` erlauben unterschiedliche
Spaltengewichte (z.B. `flex-child-2` ist doppelt so breit wie `flex-child-1`).
Auf schmalen Displays (< 600 px) brechen die Spalten automatisch untereinander um.

> **Wichtig:** Vor und nach jedem `<div>` bzw. `<section>` muss eine Leerzeile
> stehen, damit LiaScript den enthaltenen Markdown-Inhalt korrekt rendert.
