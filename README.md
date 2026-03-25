<!--
author: Sebastian Zug, Volker Göhler
email: sebastian.zug@informatik.tu-freiberg.de, volker.goehler@informatik.tu-freiberg.de
date: 25.03.2026

@style
/* TU Freiberg Color Palette */
:root {
  --tubaf-blue: #00305e;      /* Primary Blue */
  --tubaf-light-blue: #e6ebf0; /* Soft background tint */
  --tubaf-grey: #adb5bd;       /* Secondary Grey */
  --tubaf-text: #333333;
}

table {
  border-collapse: collapse;
  width: 100%;
  font-family: "Open Sans", Arial, sans-serif;
  color: var(--tubaf-text);
  margin: 20px 0;
}

/* Header: Bold TUBAF Blue with white text */
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

/* General cell padding */
td {
  padding: 12px;
  border-bottom: 1px solid #dee2e6;
}

/* The "Logical Join": Alternating background for row pairs */
/* Groups rows 1-2, 5-6, 9-10... in a light grey-blue tint */
tbody tr:nth-child(4n+1),
tbody tr:nth-child(4n+2) {
  background-color: var(--tubaf-light-blue);
}

/* Graphic Notion: Vertical blue bar on the left of joined rows */
/* This visually "clips" the two rows together */
tbody tr:nth-child(2n+1) td:first-child,
tbody tr:nth-child(2n) td:first-child {
  border-left: 5px solid var(--tubaf-blue);
}

/* Optional: Make the 'Week' number stand out in the first row of a pair */
tbody tr:nth-child(2n+1) td:first-child {
  font-weight: bold;
  color: var(--tubaf-blue);
}

/* Hover effect for better readability */
tbody tr:hover {
  background-color: #d1d9e0 !important;
}
@end

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://LiaScript.github.io/course/?https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/refs/heads/master/README.md) ![GitHub contributors](https://img.shields.io/github/contributors/TUBAF-IfI-LiaScript/VL_Softwareentwicklung)

# Übersicht zur Vorlesung (Sommersemester 2026)

| Woche | Tag       | SWE                                               | Einführung in SWE |
| :---- | --------- | :------------------------------------------------ |-------------------|
| 1     | 06. April | _Ostermontag_                                     | _Ostermontag_     |
|       | 10. April  | Organisation, Einführung                         | gemeinsam         |
| 2     | 13. April  | Softwareentwicklung als Prozess                  | gemeinsam         |
|       | 17. April | Konzepte von Dotnet und C#                        |                   |
| 3     | 20. April | Elemente der Sprache C# I                         | **Beginn der Übungen** |
|       | 24. April | Elemente der Sprache C# II                        |                   |
| 4     | 27. April | Strukturen / Konzepte der OOP                     |                   |
|       | 01. Mai   | _Erster Mai_                                      |  _Erster Mai_     |
| 5     | 4. Mai    | Säulen Objektorientierter Programmierung          | gemeinsam          |
|       | 8. Mai    | Klassenelemente in C#  / Vererbung                |                   |
| 6     | 11. Mai   | Klassenelemente in C#  / Interfaces               |                   |
|       | 15. Mai   | Anwendungsbeispiel **TODO** Godot?                |                   |
| 7     | 12. Mai   | Versionsmanagement im SWE-Prozess I               | gemeinsam          |
|       | 16. Mai   | Versionsmanagement im SWE_Pprozess II             | gemeinsam          |
| 8     | 18. Mai   | Generics                                          |                   |
|       | 22. Mai   | Container                                         |                   |
| 9     | 25. Mai   | _Pfingstmontag_                                   |   _Pfingstmontag_ |
|       | 29. Mai   | UML Konzepte                                      | gemeinsam          |
| 10    | 01. Juni  | UML Diagrammtypen                                 | gemeinsam          |
|       | 5. Juni   | UML Anwendungsbeispiel                            | gemeinsam          |
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
