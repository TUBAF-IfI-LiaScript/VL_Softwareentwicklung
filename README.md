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
  --tubaf-blue: #00305e;
  --tubaf-light-blue: #e6ebf0;
  --tubaf-grey: #adb5bd;
  --tubaf-text: #333333;
}

table {
  border-collapse: collapse;
  width: 100%;
  font-family: "Open Sans", Arial, sans-serif;
  color: var(--tubaf-text);
  margin: 20px 0;
}

thead th {
  text-align: left;
  padding: 14px 12px;
  background-color: var(--tubaf-blue);
  color: #ffffff;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 0.05em;
  border-bottom: 4px solid var(--tubaf-grey);
}

td {
  padding: 12px;
  border-bottom: 1px solid #dee2e6;
}

tbody tr:nth-child(4n+1),
tbody tr:nth-child(4n+2) {
  background-color: var(--tubaf-light-blue);
}

tbody tr:nth-child(2n+1) td:first-child,
tbody tr:nth-child(2n) td:first-child {
  border-left: 5px solid var(--tubaf-blue);
}

tbody tr:nth-child(2n+1) td:first-child {
  font-weight: bold;
  color: var(--tubaf-blue);
}

tbody tr:hover {
  background-color: #d1d9e0 !important;
}

.icon-event::before {
  font-family: "Font Awesome 6 Free";
  font-weight: 900;
  margin-right: 8px;
  color: #00305e;
  display: inline-block;
  width: 20px;
  text-align: center;
}

.icon-easter::before { content: "\f706"; color: #d4a017; }
.icon-mayday::before { content: "\f06c"; color: #c0392b; }
.icon-pentecost::before { content: "\f6d9"; color: #5dade2; }
.icon-exercise::before { content: "\f303"; }
.icon-joined::before { content: "\f0c1"; font-size: 0.8em; }

.holiday {
  color: #777;
  font-style: italic;
  background-color: #fff5f5 !important;
}
@end

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://LiaScript.github.io/course/?https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/refs/heads/master/README.md) ![GitHub contributors](https://img.shields.io/github/contributors/TUBAF-IfI-LiaScript/VL_Softwareentwicklung)

# Übersicht zur Vorlesung (Sommersemester 2026)

| Woche | Tag       | SWE                                               | Einführung in SWE |
| :---- | --------- | :------------------------------------------------ |-------------------|
| 1     | 06. April | _Ostermontag_<!-- class="holiday icon-easter" --> | _Ostermontag_<!-- class="holiday icon-easter" -->      |
|       | 10. April  | Organisation, Einführung                         | gemeinsam<!-- class="icon-joined" -->         |
| 2     | 13. April  | Softwareentwicklung als Prozess                  | gemeinsam<!-- class="icon-joined" -->         |
|       | 17. April | Konzepte von Dotnet und C#                        |                   |
| 3     | 20. April | Elemente der Sprache C# I                         | **Beginn der Übungen**<!-- class="icon-exercise" --> |
|       | 24. April | Elemente der Sprache C# II                        |                   |
| 4     | 27. April | Strukturen / Konzepte der OOP                     |                   |
|       | 01. Mai   | _Erster Mai_<!-- class="holiday icon-mayday" -->                                       |  _Erster Mai_<!-- class="holiday icon-mayday" -->      |
| 5     | 4. Mai    | Säulen Objektorientierter Programmierung          | gemeinsam<!-- class="icon-joined" -->          |
|       | 8. Mai    | Klassenelemente in C#  / Vererbung                |                   |
| 6     | 11. Mai   | Klassenelemente in C#  / Interfaces               |                   |
|       | 15. Mai   | Anwendungsbeispiel **TODO** Godot?                |                   |
| 7     | 12. Mai   | Versionsmanagement im SWE-Prozess I               | gemeinsam<!-- class="icon-joined" -->          |
|       | 16. Mai   | Versionsmanagement im SWE_Pprozess II             | gemeinsam<!-- class="icon-joined" -->          |
| 8     | 18. Mai   | Generics                                          |                   |
|       | 22. Mai   | Container                                         |                   |
| 9     | 25. Mai   | _Pfingstmontag_<!-- class="holiday icon-pentecoste" -->                                    |   _Pfingstmontag_<!-- class="holiday icon-pentecoste" -->  |
|       | 29. Mai   | UML Konzepte                                      | gemeinsam<!-- class="icon-joined" -->          |
| 10    | 01. Juni  | UML Diagrammtypen                                 | gemeinsam<!-- class="icon-joined" -->          |
|       | 5. Juni   | UML Anwendungsbeispiel                            | gemeinsam<!-- class="icon-joined" -->          |
| 11    | 8. Juni   | Dokumentation und Build Toolchains                |                   | 
|       | 12. Juni  | Delegaten                                         |                   |
| 12    | 15. Juni  | Events                                            |                   |
|       | 19. Juni  | Threadkonzepte in C#                              |                   |
| 13    | 22. Juni  | Taskmodell                                        |                   |
|       | 26. Juni  | Testen                                            |                   |
| 14    | 29. Juni  | Continuous Integration in GitHub                  |                   |
|       | 03. Juli  | Design Pattern                                    |                   |
| 15    | 06. Juli  | Language Integrated Query                         |                   |
|       | 10. Juli  | GUI - MAUI                                        |                   |
| 16    | 13. Juli  | Puffer                                            |                   |
|       | 17. Juli  | Puffer                                            |                   |
