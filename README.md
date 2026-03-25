<!--
author: "Sebastian Zug", "Volker Göhler"
email: "sebastian.zug@informatik.tu-freiberg.de", "volker.goehler@informatik.tu-freiberg.de"
date: 25.03.2026
edit: true
language: de
version: 2026.1
icon: https://tu-freiberg.de/sites/default/files/styles/crop_landscape_1300/public/2023-08/Bild2.png

link: "https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"

@style
:root {
  --tubaf-blue-dark: #00497f;
  --tubaf-blue-uni: #0069b4;
  --tubaf-grey-light: #f0f2f5;
  --tubaf-silver: #adb5bd;
  --tubaf-holiday-bg: #fff5f5; /* Light red for holidays */
  --tubaf-holiday-text: #b02a37;
}

/* Base table styling */
table {
  border-collapse: collapse;
  width: 100%;
  font-family: sans-serif;
}

thead th {
  background-color: var(--tubaf-blue-dark);
  color: white;
  padding: 12px;
  text-align: left;
  border-bottom: 3px solid var(--tubaf-blue-uni);
}

/* Row grouping logic (Every 2 rows) */
tbody tr:nth-child(4n+1), 
tbody tr:nth-child(4n+2) {
  background-color: var(--tubaf-grey-light);
}

/* The vertical "Join" indicator */
tbody tr td:first-child {
  border-left: 5px solid transparent;
}
tbody tr:nth-child(2n+1) td:first-child,
tbody tr:nth-child(2n) td:first-child {
  border-left-color: var(--tubaf-blue-uni);
}

/* Icon Classes using UTF-8 */
.icon::before {
  margin-right: 8px;
  font-style: normal;
  display: inline-block;
  color: var(--tubaf-blue-dark);
}

.icon-easter::before   { content: "🥚 "; } /* Egg */
.icon-mayday::before   { content: "🌿 "; } /* Leaf/Maypole vibe */
.icon-pentecost::before { content: "🕊️ "; } /* Dove (Pfingsten/Pentecost) */
.icon-exercise::before { content: "✍️ "; } /* Writing hand */
.icon-joined::before   { content: "🔗 "; } /* Link for conjoined */

/* 1. Target the TD or TR that contains a holiday class */
/* This colors the entire row if any cell has a holiday span */
tbody tr:has(.holiday) {
  background-color: var(--tubaf-holiday-bg) !important;
  color: var(--tubaf-holiday-text);
}
@end

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://LiaScript.github.io/course/?https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/refs/heads/master/README.md) ![GitHub contributors](https://img.shields.io/github/contributors/TUBAF-IfI-LiaScript/VL_Softwareentwicklung)

# Übersicht zur Vorlesung (Sommersemester 2026)

<!--
  data-type="none"
-->
| Woche | Tag       | SWE                                               | Einführung in SWE |
| :---- | --------- | :------------------------------------------------ |-------------------|
| 1     | 06. April | _Ostermontag_<!-- class="holiday icon-easter" --> | _Ostermontag_<!-- class="holiday icon-easter" -->      |
|       | 10. April | Organisation, Einführung                         | gemeinsam<!-- class="icon-joined" -->         |
| 2     | 13. April | Softwareentwicklung als Prozess                  | gemeinsam<!-- class="icon-joined" -->         |
|       | 17. April | Konzepte von Dotnet und C#                        |                   |
| 3     | 20. April | Elemente der Sprache C# I                         | **Beginn der Übungen**<!-- class="icon-exercise" --> |
|       | 24. April | Elemente der Sprache C# II                        |                   |
| 4     | 27. April | Strukturen / Konzepte der OOP                     |                   |
|       | 01. Mai   | _Erster Mai_<!-- class="holiday icon-mayday" -->                                       |  _Erster Mai_<!-- class="holiday icon-mayday" -->      |
| 5     | 04. Mai   | Säulen Objektorientierter Programmierung          | gemeinsam<!-- class="icon-joined" -->          |
|       | 08. Mai   | Klassenelemente in C#  / Vererbung                |                   |
| 6     | 11. Mai   | Klassenelemente in C#  / Interfaces               |                   |
|       | 15. Mai   | Anwendungsbeispiel **TODO** Godot?                |                   |
| 7     | 18. Mai   | Versionsmanagement im SWE-Prozess I               | gemeinsam<!-- class="icon-joined" -->          |
|       | 22. Mai   | Versionsmanagement im SWE_Pprozess II             | gemeinsam<!-- class="icon-joined" -->          |
| 8     | 25. Mai   | _Pfingstmontag_<!-- class="holiday icon-pentecoste" -->                                    |   _Pfingstmontag_<!-- class="holiday icon-pentecoste" -->  |
|       | 29. Mai   | Generics                                          |                   |
| 9     | 01. Juni  | Container                                         |                   |
|       | 05. Juni  | UML Konzepte                                      | gemeinsam<!-- class="icon-joined" -->          |
| 10    | 08. Juni  | UML Diagrammtypen                                 | gemeinsam<!-- class="icon-joined" -->          |
|       | 12. Juni  | UML Anwendungsbeispiel                            | gemeinsam<!-- class="icon-joined" -->          |
| 11    | 15. Juni  | Dokumentation und Build Toolchains                |                   | 
|       | 19. Juni  | Delegaten                                         |                   |
| 12    | 22. Juni  | Events                                            |                   |
|       | 26. Juni  | Threadkonzepte in C#                              |                   |
| 13    | 29. Juni  | Taskmodell                                        |                   |
|       | 03. Juli  | Testen                                            |                   |
| 14    | 06. Juli  | Continuous Integration in GitHub                  |                   |
|       | 10. Juli  | Design Pattern                                    |                   |
| 15    | 13. Juli  | Language Integrated Query                         |                   |
|       | 17. Juli  | GUI - MAUI                                        |                   |
