<!--
author:   Sebastian Zug, Volker Göhler

email:    sebastian.zug@informatik.tu-freiberg.de, volker.goehler@informatik.tu-freiberg.de

version:  0.0.5
icon: https://upload.wikimedia.org/wikipedia/commons/e/e8/TUBAF_Logo.svg
comment:  This file provides commonly used meta information for all LiaScript courses in the folder

@config.semester: `Sommersemester 2026`
@config.university: `Technische Universität Freiberg`

@style
.flex-container {
    display: flex;
    flex-wrap: wrap; /* Items brechen auf schmalen Displays um */
    align-items: stretch;
}

.flex-child,
.flex-child-1 { flex: 1; }
.flex-child-2 { flex: 2; }
.flex-child-3 { flex: 3; }
.flex-child-4 { flex: 4; }
.flex-child-5 { flex: 5; }
.flex-child-6 { flex: 6; }
.flex-child-7 { flex: 7; }
.flex-child-8 { flex: 8; }

.flex-child,
.flex-child-1,
.flex-child-2,
.flex-child-3,
.flex-child-4,
.flex-child-5,
.flex-child-6,
.flex-child-7,
.flex-child-8 {
    margin-right: 20px; /* Abstand zwischen den Spalten */
}

@media (max-width: 600px) {
    .flex-child,
    .flex-child-1,
    .flex-child-2,
    .flex-child-3,
    .flex-child-4,
    .flex-child-5,
    .flex-child-6,
    .flex-child-7,
    .flex-child-8 {
        flex: 100%; /* volle Breite auf schmalen Geräten */
        margin-right: 0;
    }
}
@end

-->

# Config variables

```
@config.semester:   `Sommersemester 2026`
@config.university: `Technische Universität Freiberg`
```

# Layout-Hilfsklassen (Flexbox)

Über die zentrale `config.md` stehen allen Kursen Flexbox-Klassen zur Verfügung,
um Inhalte nebeneinander anzuordnen. Verwendung:

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
