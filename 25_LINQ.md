<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import:  https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
         https://raw.githubusercontent.com/liaTemplates/AlaSQL/master/README.md

-->

# Softwareentwicklung - 25 - LINQ

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/25_LINQ.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/25_LINQ.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 25](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/25_LINQ.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**

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

<!-- data-marker="45 5 45 10 error;" -->
```csharp    conventionalFiltering
using System;
using System.Collections.Generic;

namespace Rextester
{
  public class Character{
    protected string name;
    public int geburtsjahr;
    private static int Count;
    int index;

    public Character(string name, int geburtsjahr){
      this.name = name;
      this.geburtsjahr = geburtsjahr;
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
      List<Character> ComicHeros = new List<Character>{
         new Character("Spiderman", 1962),
         new Character("Donald Duck", 1931),
         new Character("Superman", 1938)
      };
      Console.WriteLine("Alle Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      foreach (Character c in ComicHeros){
        Console.WriteLine(c);
      }
      // Und nun? Wie filtern wir?
    }
  }
}
```
@Rextester.eval(@CSharp)

                    {{1}}
********************************************************************************

Die intuitive Lösung könnte folgendermaßen daher kommen:

Die Dokumentation von `List<T>` findet sich unter folgendem [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1.removeall?view=netcore-3.1)

1. Wir "erinnern" uns an das `Count` Member der Klasse `List`.
2. Für die Filteroperation implementieren Sie eine Loop. Sie können dazu `foreach` verwenden, weil `List<T>` das Interface `IEnumerable` implementiert.
3. Die Sortieroperation bedingt die Anwendung einer Vergleichsoperation zwischen den Elementen der Liste. Eine Variante ist die Implementierung des Interfaces `IComparable` zu diesem Zweck.

```csharp    Solution
using System;
using System.Collections.Generic;

namespace Rextester
{
    public class Character: IComparable{
      protected string name;
      public int geburtsjahr;
      private static int Count;
      int index;

      public Character(string name, int geburtsjahr){
        this.name = name;
        this.geburtsjahr = geburtsjahr;
        index = Count;
        Count = Count + 1;
      }

      public override string ToString(){
        string row = string.Format("|{0,6} | {1,-15} | {2,8} |",
                                        index, name, geburtsjahr);
        return row;
      }

      public int CompareTo(object obj){
        if (obj == null) return 1;

        Character otherCharacter = obj as Character;
        return string.Compare(this.name, otherCharacter.name);
      }
  }

  public class Program
  {
    public static void Main(string[] args){
      List<Character> ComicHeros = new List<Character>{
         new Character("Spiderman", 1962),
         new Character("Donald Duck", 1931),
         new Character("Superman", 1938)
      };

      Console.WriteLine($"\nEinträge in der Datenbank: {ComicHeros.Count}");

      Console.WriteLine("\nGefilterte Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      List<Character> ComicHerosFiltered = new List<Character>();
      foreach (Character c in ComicHeros){
        if (c.geburtsjahr < 1950) ComicHerosFiltered.Add(c);
      }
      foreach (Character c in ComicHerosFiltered){
        Console.WriteLine(c);
      }

      Console.WriteLine("\nSortierte Einträge in der Datenbank:");
      Console.WriteLine("| Index | Name            | Ursprung |");
      ComicHeros.Sort();
      foreach (Character c in ComicHeros){
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

Alternativ schauen wir uns weiter im Kanon der `List<T>` Klasse um und realisieren die Methoden `RemoveAll()` oder `Sort()`.

`RemoveAll()` zum Beispiel entfernt alle Elemente, die mit den Bedingungen
übereinstimmen, die durch das angegebene Prädikat definiert werden. Interessant
ist dabei die Umsetzung. Ein Prädikat ist eine generischer Delegat der einen
Instanzen eines Typs `T` auf ein Kriterium hin evaluiert und einen Bool-Wert
als Ausgabe generiert.

```csharp
public int RemoveAll (Predicate<T> match);
public delegate bool Predicate<in T>(T obj);
```

Analog kann `Sort()` mit einem entsprechenden Delegaten `Comparison` verknüpft werden.

```csharp
public void Sort (Comparison<T> comparison);
public delegate int Comparison<in T>(T x, T y);
```

<!-- data-marker="35 5 38 10 error; 40 5 46 10 log" -->
```csharp    Solution
using System;
using System.Collections.Generic;

namespace Rextester
{
    public class Character: IComparable{
      protected string name;
      public int year;
      private static int Count;
      int index;

      public Character(string name, int year){
        this.name = name;
        this.year = year;
        index = Count;
        Count = Count + 1;
      }

      public override string ToString(){
        string row = string.Format("|{0,6} | {1,-15} | {2,8} |",
                                        index, name, year);
        return row;
      }

      public int CompareTo(object obj){
        if (obj == null) return 1;

        Character otherCharacter = obj as Character;
        return string.Compare(this.name, otherCharacter.name);
      }
  }

  public class Program
  {

    private static bool before1950(Character entry)
    {
      return entry.year > 1950;
    }

    private static int sortByYear(Character x, Character y)
    {
       int output = 0;
       if (y.year < x.year) output = 1;
       if (y.year > x.year) output = -1;
       return output;
    }

    public static void Main(string[] args){
      List<Character> ComicHeros = new List<Character>{
         new Character("Spiderman", 1962),
         new Character("Donald Duck", 1931),
         new Character("Superman", 1938)
      };

      ComicHeros.RemoveAll(before1950);
      //ComicHeros.RemoveAll(x => x.year > 1950);
      //ComicHeros.Sort(sortByYear);
      foreach (Character c in ComicHeros){
        Console.WriteLine(c);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

Allerdings bleibt die Darstellung von komplexeren Abfragen wie "filtere die Helden heraus, die vor 1950 geboren sind, extrahiere die Vornamen und sortiere diese in Aufsteigender alphabetischer Folge" zu einem unübersichtlichen Darstellungsformat.

********************************************************************************


                        {{3}}
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

![OOPGeschichte](/img/25_LINQ/SQL-Beispiel.png)<!-- width="80%" --> [^DatenbankSchema]

[^DatenbankSchema]: Wikipedia "SQL", Nils Boßung, https://de.wikipedia.org/wiki/SQL#/media/Datei:SQL-Beispiel.svg

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

*Language Integrated Query* (LINQ) zielt auf die direkte Integration von
Abfragefunktionen in die Sprache. Dafür definieren C# (wie auch VB.NET und F#)
eigene Schlüsselwörter sowie eine Menge an vorbestimmten
LINQ-Methoden. Diese können aber durch den Anwender in der jeweiligen Sprache
erweitert werden.

```csharp  LINQexample.cs
var query =
   from e in employees
   where e.DepartmentId == 5
   select e;
```


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

![OOPGeschichte](/img/25_LINQ/AnbieterLINQ.png)<!-- width="50%" --> [^LinqAnbieter]

[^LinqAnbieter]: Wikimedia https://commons.wikimedia.org/wiki/File:AnbieterLINQ.png, Author 'Mussklprozz'

Merkmale von LINQ

+ Die Arbeit mit Abfrageausdrücken ist einfach, da sie viele vertraute Konstrukte der Sprache C# verwenden.

+ Alle Variablen in einem Abfrageausdruck sind stark typisiert, obwohl dieser in der Regel nicht explizit angegeben wird. Der Compiler übernimmt die Ableitung.

+ Eine Abfrage wird erst ausgeführt, wenn Sie über der Abfragevariable iteriert wird. Folglich muss die Quelle in einer iterierbaren Form vorliegen.

+ Zur Kompilierzeit werden Abfrageausdrücke gemäß den in der C#-Spezifikation festgelegten Regeln in Methodenaufrufe des Standardabfrageoperators konvertiert. Die Abfragesyntax ist aber einfacher zu lesen.

+ LINQ kombiniert Abfrageausdrücke und Methodenaufrufe (`count` oder `max`). Hierin liegt die Flexibilität des Konzeptes.

Diese Veranstaltung konzentriert sich auf die *LINQ to Objects* Realisierung von
LINQ. Dabei können Abfragen mit einer beliebigen `IEnumerable`- oder `IEnumerable<T>`-Auflistungen angewandt werden.

### Exkurs "Erweiterungsmethoden"
<!--
  comment: Extensions.cs
  ..............................................................................
  1. Füge eine weitere Funktion mit input output Konfigurationen ein
  ```
  public static myString ExtendText(this myString input)
  {
     input.content = input.content + " extended!";
     return input;
  }
  ```
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
-->

Erweiterungsmethoden ergänzen den Umfang von bestehenden Methoden einer Klasse ohne selbst in diesem Typ deklariert worden zu sein. Man beschreibt eine statische Methode und ordnet diese einer Klasse über den Typ des ersten Parameters zu.

> **Merke:** Erweiterungsmethoden stellen das bisherige Konzept der Deklaration von Klassen (etwas) auf den Kopf. Sie ermöglichen es zusätzliche Funktionalität "anzuhängen".

Das folgende Beispiel unterstreicht den Unterschied zur bisher nur angedeuteten Methode der partiellen Methoden, die eine verteilte Implementierung einer Klasse erlaubt. Hierfür muss der Quellcode vorliegen, die Erweiterungsmethode `Print()` kann auch auf eine Bibliothek angewandt werden.

<!-- data-marker="4 5 4 10 error; 13 5 13 10 error;" -->
```csharp           Extensions.cs
using System;

namespace Rextester
{
  partial class myString
  {
     public string content;
     public myString(string content)
     {
       this.content = content;
     }
  }

  partial class myString
  {
     public void sayHello() => Console.WriteLine("Say Hello!");
  }


  static class Exporter
  {
      public static void Print(this myString input)
      {
         Console.WriteLine(input.content);
      }
  }

  class Program
  {
      public static void Main(string[] args)
      {
        myString text = new myString("Bla fasel");
        text.sayHello();
        text.Print();
      }
  }
}
```
@Rextester.eval(@CSharp)

Erweiterungsmethoden schaffen uns die Möglichkeit weitere Funktionalität zu
integrieren und gleichzeitig Datenobjekte durch eine Verarbeitungskette "hindurchzureichen". Erweitern Sie die statische Klasse doch mal um eine Methode, die dem Inhalt der Membervariable `content` zusätzlichen Information einfügt.

Das Ganze ist natürlich noch recht behäbig, weil wir zwingend von einem bestimmten Typen ausgehen. Dies lässt sich über eine generische Implementierung lösen.

<!-- data-marker="27 5 27 10 error;" -->
```csharp           Extensions.cs
using System;

namespace Rextester
{
  abstract class myAbstractString{
      public string content;
      public myAbstractString(string content)
      {
        this.content = content;
      }
      public void sayClassName() => Console.WriteLine(this.GetType().Name);
  }

  class myString: myAbstractString
  {
     public myString(string content): base(content) {}

  }

  class yourString: myAbstractString
  {
     public yourString(string content): base(content) {}

  }

  static class Exporter
  {
      public static void Print<T>(this T input) where T: myAbstractString
      {
         Console.WriteLine(input.content);
         input.sayClassName();
      }
  }

  class Program
  {
      public static void Main(string[] args)
      {
        myString A = new myString("Bla fasel");
        A.Print();
        yourString B = new yourString("Bla blub");
        B.Print();
      }
  }
}
```
@Rextester.eval(@CSharp)

Sie können Erweiterungsmethoden verwenden, um eine Klasse oder eine Schnittstelle zu erweitern, jedoch nicht, um sie zu überschreiben. Entsprechen wird eine Erweiterungsmethode mit dem gleichen Namen und der gleichen Signatur wie eine Schnittstellen- oder Klassenmethode nie aufgerufen.

### Exkurs "Anonyme Typen"

Anonyme Typen erlauben die Spezifikation eines Satzes von schreibgeschützten Eigenschaften, ohne zunächst explizit einen Typ definieren zu müssen. Der Typname wird dabei automatisch generiert.

Anonyme Typen enthalten mindestens eine schreibgeschützte Eigenschaft, alle anderen Arten von Klassenmembern sind ausgeschlossen.

```csharp           Extensions.cs
using System;

namespace Rextester
{
  class Program
  {
      public static void Main(string[] args)
      {
          var v = new {text = "Das ist ein Text", zahl = 1};
          Console.WriteLine($"text = {v.text}, zahl = {v.zahl}");
          Console.WriteLine($"type = {v.GetType().Name}");

          var myPropertyInfo = v.GetType().GetProperties();
          Console.WriteLine("Properties sind:");
          for (int i = 0; i < myPropertyInfo.Length; i++)
          {
              Console.WriteLine(myPropertyInfo[i].ToString());
          }
      }
  }
}
```
@Rextester.eval(@CSharp)

Der Vorteil anonymer Typen liegt in ihrer Flexibilität. Die eigentlichen Daten werden entsprechend den Ergebnissen einer Funktion erzeugt.


### Exkurs "Enumarables"

![Collections](https://www.plantuml.com/plantuml/png/VP5FIyD04CNlyoaMl8afVe0GAlw1OZr8nVjsCzeXcrsPp1uKFxma4tN9jhqDx_VcCRnP3s9PKkzXw2XyMBQzSTuEmuq8qpu9RbmCE_f2Smq7Qj4uOkTHvoUKGsnrVY3qBS2qR3Rt8VN8LYAR-gKnTKr1aD-imwOn2zFUOsdwzTn6xz49nN3QiwL19deStz6qR_dJr8zNvdMPCll-KYxU6J7CwdF2XAMy4-kwKjvIwB0zdbIUiOYCBBfxZeyfImvvavTLazSFUODLzQrmDaFMZSBC3Tfh8KEsirgDy5-0xCWJR0mjMQQE8sYHIrMVeK9saJwZaDSOsjJx7m00)<!-- size="350px" -->

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
