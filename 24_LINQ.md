<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
import: https://raw.githubusercontent.com/liaTemplates/AlaSQL/master/README.md
-->

# Vorlesung Softwareentwicklung - 24 - Language-Integrated Query (LINQ)

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/24_LINQ.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 24](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/24_LINQ.md#1)

---------------------------------------------------------------------

**Wie weit sind wir schon gekommen?**

c# Schlüsselwörter:

| abstract    | as       | base     |`bool`      |`break`     |`byte`     |
|`case`       |`catch`   | char     |`checked`   |`class`     | const     |
|`continue`   |`decimal` | default  | delegate   |`do`        |`double`   |
|`else`       |`enum`    | event    | explicit   | extern     |`false`    |
|`finally`    | fixed    |`float`   |`for`       |`foreach`   |`goto`     |
|`if`         | implicit | in       |`int`       | interface  |`internal` |
| is          | lock     |`long`    |`namespace` |`new`       | null      |
| object      | operator |`out`     | override   |`params`    |`private`  |
| protected   |`public`  | readonly |`ref`       |`return`    |`sbyte`    |
| sealed      |`short`   | sizeof   | stackalloc |`static`    |`string`   |
|`struct`     |`switch`  |`this`    |`throw`     |`true`      |`try`      |
| typeof      |`uint`    |`ulong`   |`unchecked` | unsafe     |`ushort`   |
|`using`      | virtual  |`void`    | volatile   |`while`     |           |


Auf die Auführung der kontextabhängigen Schlüsselwörter wie `where` oder
`ascending` wurde hier verzichtet.

---

## Kontrollfragen

*1. Hier stehen jetzt Ihre Fragen ...*

## Ankündigung

0. **Die Klausur findet am 23.07.2019, von 12:30 bis 14:30 statt.**

1. **Bitte senden Sie bis zum Freitag Abend Fragen an die Tutoren, die Sie mit Blick auf die Klausur noch mal besprochen haben wollen.**

    Dieses Angebot sollte Teil Ihrer Vorbereitungen auf die Prüfung sein und nicht deren Beginn!

2. **In die Klausur dürfen Sie einen Notizzettel A4 (bedruckt, beschrieben) mitbringen.**

    Welcher Art Ihre Notizen sind, ist Ihnen überlassen. Sie sollten sich dafür entscheiden aussagekräftige Beispiele vorzubereiten, die Ihnen  Anleitung für die Lösung der Aufgaben gibt.

3. **Studentische Mitarbeiter als Tutoren gesucht!**

## Motivation

Gegeben sei das Datenset eines Comic-Begeisterten in Form einer generischen
Liste `List<T>`.

1. Bestimmen Sie die Zahl der Einträge unseres Datensatzes
2. Filtern Sie die Liste der Comic Figuren nach dem Alter und
3. Sortieren Sie die Liste nach dem Anfangsbuchstaben des Namens.

```csharp    conventionalFiltering
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Character{
    protected string name;             
    public int geburtsjahr;

    public Character(string name, int geburtsjahr){
      this.name = name;
      this.geburtsjahr = geburtsjahr;
    }
  }

  public class ListedCharacter: Character{
      public static int Count;
      int index;

      public ListedCharacter(string name, int geburtsjahr):
                        base(name, geburtsjahr){
        index = Count;
        Count = Count + 1;
      }

      public override string ToString(){
         string row = string.Format("|{0,6} | {1,-15} | {2,8} |",
                                      index, name, geburtsjahr);
         return row;
      }
  }

  public class Program
  {
    public static void Main(string[] args){
      List<ListedCharacter> ComicHeros = new List<ListedCharacter>{
         new ListedCharacter("Spiderman", 1962),
         new ListedCharacter("Donald Duck", 1931),
         new ListedCharacter("Superman", 1938)
      };
      Console.WriteLine("Alle Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      foreach (ListedCharacter c in ComicHeros){
        Console.WriteLine(c);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

                    {{1}}
********************************************************************************

Die Lösung könnte folgendermaßen daher kommen:

Die Dokumentation von `List<T>` findet sich unter folgendem [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1?view=netframework-4.8)

1. Bei der Konsultation der Dokumentation von List "entdecken" Sie die Eigenschaft `Count`.
2. Für die Filteroperation implementieren Sie eine Loop. Sie können dazu `foreach` verwenden, weil `List<T>` das Interface `IEnumerable` implementiert.
3. Die Sortieroperation bedingt die Anwendung einer Vergleichsoperation zwischen den Elementen der Liste. Eine Variante ist die Implementierung des Interfaces `IComparable` zu diesem Zweck.

```csharp    Solution
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Character{
    protected string name;             
    public int geburtsjahr;

    public Character(string name, int geburtsjahr){
      this.name = name;
      this.geburtsjahr = geburtsjahr;
    }
  }

  public class ListedCharacter: Character, IComparable{
      public static int Count;
      int index;

      public ListedCharacter(string name, int geburtsjahr): base(name, geburtsjahr){
        index = Count;
        Count = Count + 1;
      }

      public override string ToString(){
         string row = string.Format("|{0,6} | {1,-15} | {2,8} |", index, name, geburtsjahr);
         return row;
      }

      public int CompareTo(object obj){
        if (obj == null) return 1;

        ListedCharacter otherCharacter = obj as ListedCharacter;
        return string.Compare(this.name, otherCharacter.name);
      }
  }

  public class Program
  {
    public static void Main(string[] args){
      List<ListedCharacter> ComicHeros = new List<ListedCharacter>{
         new ListedCharacter("Spiderman", 1962),
         new ListedCharacter("Donald Duck", 1931),
         new ListedCharacter("Superman", 1938)
      };
      Console.WriteLine("Alle Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      foreach (ListedCharacter c in ComicHeros){
        Console.WriteLine(c);
      }

      Console.WriteLine("Gefilterte Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      List<ListedCharacter> ComicHerosFiltered = new List<ListedCharacter>();
      foreach (ListedCharacter c in ComicHeros){
        if (c.geburtsjahr < 1950) ComicHerosFiltered.Add(c);
      }
      foreach (ListedCharacter c in ComicHerosFiltered){
        Console.WriteLine(c);
      }

      Console.WriteLine("Sortierte Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      ComicHeros.Sort();
      foreach (ListedCharacter c in ComicHeros){
        Console.WriteLine(c);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

Eine Menge Aufwand für einen simple Operation! Welche zusätzlichen Probleme
werden auftreten, wenn Sie eine solche Kette aus Datenerfassung, Verarbeitung
und Ausgabe in realen Anwendungen umsetzen?

********************************************************************************

                        {{2}}
********************************************************************************

Die Methoden für den Datenzugriff und die Manipulation abhängig vom Datentyp
(Felder, Objektlisten) und der Herkunft (XML-Dokumente, Datenbanken,
Excel-Dateien, usw.).LINQ versucht dieses Problem zu beseitigen, indem es
innerhalb der Entwicklungsplattform .NET eine einheitliche Methode für jeglichen
Datenzugriff zur Verfügung stellt. Die Syntax der Abfragen in LINQ orientiert
sich dabei an der *Structured Query Language* (SQL).

********************************************************************************

### Exkurs SQL

Hier folgt ein kurzer Einschub zum Thema SQL ... um allen Teilnehmern eine sehr
grundlegende Sicht zu vermitteln:

SQL ist eine Datenbanksprache zur Definition von Datenstrukturen in relationalen
Datenbanken sowie zum Bearbeiten (Einfügen, Verändern, Löschen) und Abfragen von
darauf basierenden Datenbeständen.

Ausgangspunkt sind Datenbanktabellen, die Abfragen dienen dabei der Generierung  
spezifischer Informationssets:

+ "Alle Bücher mit Buchnummern von 123400 bis 123500"
+ "Alle Buchnummern mit Autoren, die im 19. Jahrhundert erschienen."
+ "In welchem Jahrhundert veröffentlichte welcher Verlag die meisten Bücher?"
+ ...

| Buchnummer | Autor              | Verlag                    | Datum | Titel                  |
| ---------- | ------------------ | ------------------------- | ----- | ---------------------- |
| 123456     | Hans Vielschreiber | Musterverlag              | 2007  | Wir lernen SQL         |
| 123457     | J. Gutenberg       | Gutenberg und Co.         | 1452  | Drucken leicht gemacht |
| 123458     | Galileo Galilei    | Inquisition International | 1640  | Eppur si muove         |
| 123459     | Charles Darwin     | Vatikan Verlag            | 1860  | Adam und Eva           |

SQL basiert auf der relationalen Algebra, ihre Syntax ist relativ
einfach aufgebaut und semantisch an die englische Umgangssprache angelehnt. Die
Bezeichnung SQL bezieht sich auf das englische Wort “query” (deutsch:
„Abfrage“). Mit Abfragen werden die in einer Datenbank gespeicherten Daten
abgerufen, also dem Benutzer oder einer Anwendersoftware zur Verfügung gestellt.
Durch den Einsatz von SQL strebt man die Unabhängigkeit der Anwendungen vom
eingesetzten Datenbankmanagementsystem an.

SQL-Aufrufe sind deklarativ, weil der Entwickler hier nur das WAS und nicht das
WIE festlegt. Dabei strukturieren sich die Befehle in 4 Kategorien:

+ Befehle zur Abfrage und Aufbereitung der gesuchten Informationen
+ Befehle zur Datenmanipulation (Ändern, Einfügen, Löschen)
+ Befehle zur Definition des Datenbankschemas
+ Befehle für die Rechteverwaltung und Transaktionskontrolle.

Eine Datenbanktabelle stellt eine Datenbank-Relation dar. Die Relation ist Namensgeber und Grundlage der relationalen Datenbanken.

![OOPGeschichte](/img/24_LINQ/SQL-Beispiel.png)<!-- width="80%" --> [DatenbankSchema](#7)

                                  {{1-2}}
*******************************************************************************
**Erzeugung der Tabellen**

``` text -student.csv
MatrNr,Name
26120,Fichte
25403,Jonas
27103,Fauler
```

``` sql
CREATE TABLE Student;
INSERT INTO Student SELECT * from ?;
```
``` text -student.csv
MatrNr,Name
26120,Fichte
25403,Jonas
27103,Fauler
```
@AlaSQL.eval_with_csv

``` sql
CREATE TABLE hoert;
INSERT INTO hoert SELECT * from ?;
```
``` text -hoert.csv
MatrNr,VorlNr
26120,5001
25403,5001
27103,5045
```
@AlaSQL.eval_with_csv


``` sql
CREATE TABLE Vorlesung;
INSERT INTO Vorlesung SELECT * from ?;
```
``` text -vorlesung.csv
VorlNr,Titel,PersNr
5001,ET,15
5022,IT,12
5045,DB,12
```
@AlaSQL.eval_with_csv

``` sql
CREATE TABLE Professor;
INSERT INTO Professor SELECT * from ?;
```
``` text -prof.csv
PersNr,Name
12,Wirth
15,Tesla
20,Urlauber
```
@AlaSQL.eval_with_csv

*******************************************************************************

                                {{2}}
*******************************************************************************

**Beispiele**

``` sql    Auslesen aller Spalten und aller Zeilen
SELECT *
FROM Student;
```
@AlaSQL.eval


``` sql     Abfrage mit Spaltenauswahl
SELECT VorlNr, Titel
FROM Vorlesung;
```
@AlaSQL.eval

``` sql      Abfrage mit eindeutigen Werten
SELECT DISTINCT MatrNr
FROM hoert;
```
@AlaSQL.eval

``` sql        Abfrage mit Filter und Sortierung
SELECT VorlNr, Titel
FROM Vorlesung
WHERE Titel = 'ET';
```
@AlaSQL.eval

`LIKE` kann mit verschiedenen Platzhaltern verwendet werden: _ steht für ein einzelnes beliebiges Zeichen, % steht für eine beliebige Zeichenfolge. Manche Datenbanksysteme bieten weitere solche Wildcard-Zeichen an, etwa für Zeichenmengen.

`ORDER BY` öffnet die Möglichkeit die Reihung anzupassen.

``` sql   Linker äußerer Verbund
SELECT Vorlesung.VorlNr, Vorlesung.Titel, Professor.PersNr, Professor.Name
FROM Professor LEFT OUTER JOIN Vorlesung
ON Professor.PersNr = Vorlesung.PersNr;
```
@AlaSQL.eval

``` sql   Gruppierung mit Aggregat-Funktionen
SELECT COUNT(Vorlesung.PersNr) AS Anzahl, Professor.PersNr, Professor.Name
FROM Professor LEFT OUTER JOIN Vorlesung
ON Professor.PersNr = Vorlesung.PersNr
GROUP BY Professor.Name, Professor.PersNr;
```
@AlaSQL.eval

****************************************************************

## LINQ

*Language Integrated Query* (LINQ) umfasst ein Konzept in .NET, dass auf der
direkte Integration von Abfragefunktionen abzielt. Dafür definieren die C#,
VB.NET und F# eigene Schlüsselwörter sowie eine Menge an vorbestimten
LINQ-Methoden. Diese können aber durch den Anwender in der jeweiligen Sprache
erweitert werden.

LINQ-Anweisungen sind unmittelbar als Quelltext in .NET-Programme eingebettet.
Somit kann der Code durch den Compiler auf Fehler geprüft werden. Andere
Verfahren wie *ActiveX Data Objects* ADO und *Open Database Connectivity* ODBC
hingegen verwenden Abfragestrings. Diese können erst zur Laufzeit interpretiert
werden; dann wirken Fehler gravierender und sind schwieriger zu analysieren.

Innerhalb des Quellprogramms in C# oder VB.NET präsentiert LINQ die
Abfrage-Ergebnisse als streng typisierte Aufzählungen. Somit gewährleistet es
Typsicherheit bereits zur Übersetzungszeit wobei ein minimaler Codeeinsatz zur
Realisierung von Filter-, Sortier- und Gruppiervorgänge in Datenquellen
investiert wird.

![OOPGeschichte](/img/24_LINQ/AnbieterLINQ.png)<!-- width="80%" --> [LINQEbenen](#7)

Merkmale von LINQ

+ Die Arbeit mit Abfrageausdrücken ist einfach, da sie viele vertraute Konstrukte der Sprache C# verwenden.

+ Alle Variablen in einem Abfrageausdruck sind stark typisiert, obwohl dieser in der Regel nicht explizit angegeben wird. Der Compiler übernimmt die Ableitung.

+ Eine Abfrage wird erst ausgeführt, wenn Sie über der Abfragevariable iteriert wird. Folglich muss die Quelle in einer iterierbaren Form vorliegen.

+ Zur Kompilierzeit werden Abfrageausdrücke gemäß den in der C#-Spezifikation festgelegten Regeln in Methodenaufrufe des Standardabfrageoperators konvertiert. Die Abfragesyntax ist aber einfacher zu lesen.

+ LINQ kombiniert Abfrageausdrücke und Methodenaufrufe (`count` oder `max`). Hierin liegt die Flexibilität des Konzeptes.

Diese Veranstaltung konzentriert sich auf die *LINQ to Objects* Realisierung von
LINQ. Dabei können Abfragen mit einer beliebigen `IEnumerable`- oder `IEnumerable<T>`-Auflistungen angewandt werden.

### Exkurs "Enumarables"

![Collections](http://www.plantuml.com/plantuml/png/VP5FIyD04CNlyoaMl8afVe0GAlw1OZr8nVjsCzeXcrsPp1uKFxma4tN9jhqDx_VcCRnP3s9PKkzXw2XyMBQzSTuEmuq8qpu9RbmCE_f2Smq7Qj4uOkTHvoUKGsnrVY3qBS2qR3Rt8VN8LYAR-gKnTKr1aD-imwOn2zFUOsdwzTn6xz49nN3QiwL19deStz6qR_dJr8zNvdMPCll-KYxU6J7CwdF2XAMy4-kwKjvIwB0zdbIUiOYCBBfxZeyfImvvavTLazSFUODLzQrmDaFMZSBC3Tfh8KEsirgDy5-0xCWJR0mjMQQE8sYHIrMVeK9saJwZaDSOsjJx7m00)<!-- size="350px" -->

```csharp
public interface IEnumerable<out T> : System.Collections.IEnumerable{
  public IEnumerator<T> = GetEnumerator();
}

public interface IEnumerator<out T> : IDisposable,
                                      System.Collections.IEnumerator{
  public object Current { get; }
  public bool MoveNext ();
  public void Reset ();
}
```

Zur Wiederholung soll nochmals ein kurzes Implementierungsbeispiel gezeigt
werden. An dieser Stelle wird eine Klasse myStrings umgesetzt, die als
Enumerationstyp realisiert werden soll. Entsprechend implementiert die Klasse
`IEnumerable` das Interface `IEnumerable<string>` und referenziert einen
Enumeratortyp `StringEnumerator`, der wiederum das Interface generische
Interface `IEnumerator<string>` umsetzt.

Transformieren Sie folgendes Codefragment in eine UML Darstellung.

```csharp           GenericIEnumerable
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
  class myStrings : IEnumerable<string>{
     public string []  str_arr = new string[] {"one" , "two" ,"three", "four", "five"};

     public IEnumerator<string> GetEnumerator()
     {
         IEnumerator<string> r = new StringEnumerator(this);
         return r ;
     }

     IEnumerator IEnumerable.GetEnumerator()
     {
         return GetEnumerator() ;
     }
  }

  class StringEnumerator : IEnumerator<string>{
      int index;
      myStrings sp;

      public StringEnumerator (myStrings str_obj){
         index = -1 ;
         sp = str_obj ;           
      }

      object IEnumerator.Current{
        get
          { return sp.str_arr[ index ] ; }
      }

      public string Current{
        get
          { return sp.str_arr[ index ] ; }
      }

      public bool MoveNext( ){
         if ( index < sp.str_arr.Length - 1 ){
             index++ ;
             return true ;
         }
         return false ;
      }

      public void Reset( ){
         index = -1 ;
      }  

      public void Dispose(){
        // pass
      }
  }

  class Program {  
      public static void Main(string[] args){
        myStrings spp = new myStrings();

        foreach( string i in spp)
                System.Console.WriteLine(i);
      }  
  }
}
```
@Rextester.eval(@CSharp)

![Protected](http://www.plantuml.com/plantuml/png/bLBFIyCm5BxdhtZ7ZR7YkPGmKP4T9k9wKnbf-vpHDXb9EgPp_xjfkffWrE6fzFlo_NWlcMd3b6cRcZpp2g7aggmHY7xbOiCKQw2icTRdnYXUj0RdfHHB_evmHeXZe7bRMawizPx01BHHAwPK2jg1SFy87NoDvagq3Ifcf1gDKvZxtwm_Ie70z0ilQap-4f73aD-dUyRMi3vSLBXBxSU0-zURr3Vje4aaX94_q8qXYvUnqmQnoKMh50hJjR4ybiPP1MW_J5VFPgDwOYM6GsKvXIoR3nIbjkwf_UJqLxlLVyqYfu7uqMaXjtY3EpTO8MNjm3lKw92jv1Kuw9BhZTGuWDz2UhQh6sMSrFg2yUR2rQUG-oTng-IwUxhxuzN7Tx_NXXbU7W0MZ8imUz1EfzIBIB1oo3wI9F1BRQk2YuhI1m5PRcN7znoALhqgG4WmcYFZztZQsPuQd3r2WeN7f2-UsH6ZK393KRLD_Ga0)<!-- width="80%" --> [PublicPrivate.plantUML]()

Welchen Vorteil habe ich verglichen mit einer nicht-enumerate Datenstruktur, zum
Beispiel einem array? Im Hinblick auf eine konkrete Implementierung ist zwischen
dem Komfort der erweiterten API und den Performance-Eigenschaften abzuwägen.

Einen Überblick dazu bietet unter anderem die Diskussion unter
https://stackoverflow.com/questions/169973/when-should-i-use-a-list-vs-a-linkedlist/29263914#29263914

### Grundlagen

Sie können LINQ zur Abfrage beliebiger aufzählbarer Auflistungen wie List<T>,
Array oder Dictionary<TKey,TValue> verwenden. Die Auflistung kann entweder
benutzerdefiniert sein oder von einer .NET Framework-API zurückgegeben werden.

Alle LINQ-Abfrageoperationen bestehen aus drei unterschiedlichen Aktionen:

+ Abrufen der Datenquelle
+ Erstellen der Abfrage
+ Ausführen der Abfrage

Für ein einfaches Beispiel, das Filtern einer Liste von Zahlenwerten realisiert
sich dies wie folgt:

```csharp           LINQBasicExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Program {  
        public static void Main(string[] args){
          // Spezifikation der Datenquelle
          int[] scores = new int[] { 97, 92, 81, 60 };

          // Definition der Abfrage
          IEnumerable<int> scoreQuery =
              from score in scores    // Bezug zur Datenquelle
              where score > 80        // Filterkriterium
              select score;           // "Projektion" des Rückgabewertes

          // Execute the query.
          foreach (int i in scoreQuery)
          {
              Console.Write(i + " ");
          }
        }  
    }
}
```
@Rextester.eval(@CSharp)

## Anhang

**Referenzen**

[DatenbankSchema] Wikipedia "SQL", Nils Boßung, https://de.wikipedia.org/wiki/SQL#/media/Datei:SQL-Beispiel.svg

[Mössenböck] Mössenböck, Hanspeter, "Kompaktkurs C#", dpunkt.verlag, 2019

**Autoren**

Sebastian Zug, André Dietrich
