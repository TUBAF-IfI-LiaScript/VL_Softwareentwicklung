<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import:  https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
         https://raw.githubusercontent.com/liaTemplates/AlaSQL/master/README.md

-->

# Softwareentwicklung - 26- LINQ

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/26_LINQ_II.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/26_LINQ_II.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 26](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/26_LINQ_II.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**

## LINQ - Grundlagen

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


**Datenquellen**

| Zugriff            | Bedeutung                                                            |
| ------------------ | -------------------------------------------------------------------- |
| LINQ to Objects    | Zugriff auf Objektlisten und -Hierarchien im Arbeitsspeicher         |
| LINQ to SQL        | Abfrage und Bearbeitung von Daten in MS-SQL-Datenbanken              |
| LINQ to Entities   | Abfrage und Bearbeitung von Daten im relationalen Modell von ADO.NET |
| LINQ to XML        | Zugriff auf XML-Inhalte                                              |
| LINQ to DataSet    | Zugriff auf ADO.NET-Datensammlungen und -Tabellen                    |
| LINQ to SharePoint | Zugriff auf SharePoint-Daten                                         |

Im Rahmen dieser Veranstaltung konzentrieren wir uns auf die LINQ to Objects
Variante.

**Query Ausdrücke**

Insgesamt sind 7 Query-Klauseln vorimplementiert, können aber durch Erweiterungsmethoden ergänzt werden.

| Ausdruck | Bedeutung                                         |
| -------- | ------------------------------------------------- |
| from     | definieren der Laufvariable und einer Datenquelle |
| where    | filtert die Daten nach bestimten Kriterien      |
| orderby  | sortiert die Elemente                             |
| select   | projeziert die Laufvariable auf die Ergebnisfolge |
| group    | bildet Gruppen innerhalb der Ergebnismenge        |
| join     | vereinigt Elemente mehrere Datenquellen           |
| let      | definiert eine Hilfsvariable                      |

```csharp
class Student{
  public string Name;
  public int Id;
  public string Subject{get; set;}
  public Student(){}
}

// Collection Initialization
List<Student> students = new List<Student>{
  new Student("Max Müller"){Subject = "Technische Informatik", id = 1},
  new Student("Maria Maier"){Subject = "Softwareentwicklung", id = 2},
  new Student("Martin Morawschek"){Subject = "Höhere Mathematik I", id = 3}
}

// Implizite Typdefinition
var result = from s in students         // Spezifikation der Datenquelle
             where s.Subject == "Softwarentwicklung"
             orderby s.Name
             select new (s.Name, s.Id)  // Projektion der Ausgabe

// explizite Typdefinition
IEnumerable<Student> result = from s in students
                              ...
```

Im vorangehenden Beispiel ist `students` die Datenquelle, über der die Abfrage
bearbeitet wird. Der List-Datentyp implementiert das Interface `IEnumerable<T>`.
Die letzte Zeile bildet das Ergebnis auf die Rückgabe ab, dem Interface
entsprechen auf ein `IEnumerable<Student>` mit den Feldern Name und Id.

Die Berechnung der Folge wird nicht als Ganzes realisiert sondern bei einer
Iteration durch den Datentyp `List<Student>`.

Für nicht-generische Typen (die also IEnumerable anstatt IEnumerable<T> unmittelbar)
implementieren, muss zusätzlich der Typ der Laufvariable angegeben werden,
da diese nicht aus der Datenquelle ermittelt werden kann.

```csharp           StudentListExample
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
  public class Student
  {
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public int[] Scores { get; set; }
  }

   class Program {
      public static void Main(string[] args){
        //ArrayList StudentList = new ArrayList();  <-- Nicht mehr benutzen
        List<Student> StudentList = new List<Student>();
        StudentList.Add(
            new Student{
                FirstName = "Svetlana", LastName = "Müller", Scores = new int[] { 98, 92, 81, 60 }
                });
        StudentList.Add(
            new Student {
                FirstName = "Claire", LastName = "O’Donnell", Scores = new int[] { 75, 84, 91, 39 }
                });

        var query = from student in StudentList
                    where student.Scores[0] > 95
                    select student;

        foreach (Student s in query)
            Console.WriteLine(s.LastName + ": " + s.Scores[0]);
      }
   }
}
```
@Rextester.eval(@CSharp)

Welche Struktur ergibt sich dabei generell für eine LINQ-Abfrage? Ein Query
beginnt immer mit einer `from`-Klausel und endet mit einer `select` oder `group`-Klausel.

Allgemeingültig lässt sich, entsprechend den Ausführungen in [Mössenböck](#12) folgende Syntax ableiten:

```
QueryExpr =
   "from" [Type] variable "in" SrcExpr
   QueryBody
QueryBody =
   { "from" [Type] variable "in" SrcExpr
   | "where" BoolExpr
   | "orderby" Expr ["ascending" | "descending"] {"," Expr ["ascending" | "descending"]}
   | "join" [Type] variable "in" SrcExpr "on" Expr "equals" Expr ["into" variable]
   | "let" variable "=" Expr
   }
   ( "select" ProjectionExpr ["into" variable QueryBody]
   | "group" ProjectionExpr "by" Expr ["into" variable QueryBody]
   ).
```

Mit der isolierten Definition der Abfragen können diese mehrfach auf die Daten
angewandt werden. Man spricht dabei von einer "verzögerten Ausführung" - jeder
Aufruf der Ausgabe generiert eine neue Abfrage.

```csharp           DelayedEvaluation
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Program {
        public static void Main(string[] args){

          var numbers = new List<int>() {1,2,3,4};

          // Spezifikation der Anfrage
          var query = from x in numbers
                      select x;

          Console.WriteLine(query.GetType());

          // Manipulation der Daten
          numbers.Add(5);
          Console.WriteLine(query.Count());
          // Manipulation und erneute Anwendung der Abfrage
          numbers.Add(6);
          Console.WriteLine(query.Count()); // 6

        }
    }
}
```
@Rextester.eval(@CSharp)

### Hinter den Kulissen

Der Compiler transformiert LINQ-Anfragen in der Abfragesyntax in
Lambda-Ausdrücke, Erweiterungsmethoden, Objektinitializer und anonyme Typen.
Dabei sprechen wir von der Methodensyntax. Abfragesyntax und Methodensyntax sind
semantisch identisch, aber viele Benutzer finden die Abfragesyntax einfacher und
leichter zu lesen. Da aber einige Abfragen nur in der Methodensyntax möglich
sind, müssen sie diese bisweilen nutzen. Beispiele dafür sind `Max()`, `Min()`,
oder `Take()`.

Nehmen wir also nochmals eine Anzahl von Studenten an, die in einer generischen
Liste erfasst wurden:

```csharp
List<Student> students = new List<Student>({
  new Student{
      Id = "123sdf234"
      FirstName = "Svetlana",
      LastName = "Omelchenko",
      Field = "Computer Science",
      Scores = new int[] { 98, 92, 81, 60 }
  };
  //...
  });

var result = from s in students
             where s.Field == "Computer Science"
             orderby s.LastName
             select new {s.LastName, s.Id};
```

Der Compiler generiert daraus folgenden Code:

```csharp
IEnumerable<Student> result = students
                              .Where(s => s.Field == "Computer Science" )
                              .OrderBy(s => s.LastName)
                              .Select(s => new {s.LastName, s.Id});
```

Wieso hat meine Klasse `Student` plötzlich eine Methode `where`? Eine der
Grundlage sind sogenannte Erweiterungsmethoden. Erweiterungsmethoden werden in
C# in einer statischen Klasse als statische Methode definiert. Das Schlüsselwort
this vor dem ersten Parameter definiert den zu erweiternden Typen. Dieser erste
Parameter wird beim Aufruf nicht mit übergeben.

Im Beispiel soll die Klasse System.String um eine weitere Substring-Anweisung
ergänzt werden:

```csharp
public static class MyStringExtensions
{
  //                               zu erweiternder Typ
  //                                      |
  public static string MySubstring(this string me, int position, int length)
  {
     //beliebige Logik
     return "My" + me.Substring(position, length);
  }
}

// Anwendung
string teststring = "test";
teststring.MySubstring(1, 2);
```

Dabei wird die eigentliche Filterfunktion als Delegat übergeben, dies wiederum
kann durch eine Lambdafunktion ausgedrückt werden.
https://docs.microsoft.com/de-de/dotnet/api/system.linq.enumerable.where?view=netframework-4.8

Dabei beschreiben die Lambdafunktionen sogenannten Prädikate, Funktionen, die eine
bestimmte Bedingung prüfen und einen boolschen Wert zurückgeben.

```csharp        WhereExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Program {

        public static bool filterme(int num){
            bool result = false;
            if (num > 10) result = true;
            return result;
        }

        public static void Main(string[] args){

          int[] numbers = { 0, 30, 20, 15, 90, 85, 40, 75 };

          //Func<int, bool> filter = delegate(int num) { return num > 10; };
          Func<int, bool> filter = filterme;
          IEnumerable<int> query =
              numbers.Where(filter);

          //IEnumerable<int> query =
          //      numbers.Where(s => s > 10);

          foreach (int number in query)
          {
              Console.WriteLine(number);
          }
        }
    }
}
```
@Rextester.eval(@CSharp)

## Basisfunktionen von LINQ

Mit LINQ lassen sich Elementaroperationen definieren, die dann im Ganzen die Mächtigkeit des Konzeptes ausmachen.

### Filtern
<!--
  comment: FilterExample.cs
  ..............................................................................
  1. Füge eine eigenständige Methode für die Bestimmung von geraden Zahlen ein
  ```
  public static bool even(int value)
  {
     return value % 2 == 0;
  }
  ```
  ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
-->

Das Beispiel zur Filterung einer Customer-Tabelle wurde der C# Dokumentation
unter https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations
entnommen.

Die üblichste Abfrageoperation ist das Anwenden eines Filters in Form eines
booleschen Ausdrucks. Das Filtern bewirkt, dass im Ergebnis nur die Elemente
enthalten sind, für die der Ausdruck eine wahre Aussage liefert.

Das Ergebnis wird durch Verwendung der `where`-Klausel erzeugt. Faktisch gibt
der Filter an, welche Elemente nicht in die Quellsequenz eingeschlossen werden
sollen. In folgendem Beispiel werden nur die customers zurückgegeben, die eine
Londoner Adresse haben.

```csharp
var queryLondonCustomers = from customer in customers
                           where customer.City == "London"
                           select customer;
```

Sie können die logischen Operatoren `&&` und `||` verwenden, um so viele Filterausdrücke wie
benötigt in der `where`-Klausel anzuwenden.

```csharp        FilterExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Program {

        public static void Main(string[] args){

          var numbers = new List<int>() {-1, 7,11,21,32,42};

          var query = from i in numbers
                      where i < 40 && i > 0
                      select i;

          foreach (var x in query)
            Console.WriteLine(x);
        }
    }
}
```
@Rextester.eval(@CSharp)

Die entsprechenden Operatoren können aber auch um eigenständige Methoden ergänzt
werden. Versuchen Sie zum Beispiel die Bereichsabfrage um eine Prüfung zu
erweitern, ob der Zahlenwert gerade ist.

### Gruppieren

Die group-Klausel ermöglicht es, die Ergebnisse auf der Basis eines Merkmals
zusammenzufassen. Die group-Klausel gibt entsprechend eine Sequenz von
`IGrouping<TKey,TElement>`-Objekten zurück, die null oder mehr Elemente
enthalten, die mit dem Schlüsselwert `TKey` für die Gruppe übereinstimmen. Der
Compiler leiten den Typ des Schlüssels anhand der Parameter von `group` her.
IGrouping selbst implementiert das Interface `IEnumerable` und kann damit
iteriert werden.

```csharp
var queryCustomersByCity =
    from customer in customers
    group customer by customer.City;

// customerGroup is an IGrouping<string, Customer> now!
foreach (var customerGroup in queryCustomersByCity)   // Iteration 1
{
    Console.WriteLine(customerGroup.Key);
    foreach (Customer customer in customerGroup)      // Iteration 2
    {
        Console.WriteLine("    {0}", customer.Name);
    }
}
```

Dabei können die Ergebnisse einer Gruppierung wiederum Ausgangsbasis für eine
weitere Abfrage sein, wenn das Resultat mit `into` in einem Zwischenergebnis
gespeichert wird.

```csharp        GroupByExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Student{
      public string Name;
      public int id;
      public string Subject{get; set;}
      public Student(){}

      public Student(string name){
         this.Name = name;
      }
    }

    class Program {
        public static void Main(string[] args){

          List<Student> students = new List<Student>{
            new Student("Max Müller"){Subject = "Technische Informatik", id = 1},
            new Student("Maria Maier"){Subject = "Softwareentwicklung", id = 2},
            new Student("Martin Morawschek"){Subject = "Höhere Mathematik I", id = 3},
            new Student("Katja Schulz"){Subject = "Technische Informatik", id = 4},
            new Student("Karl Tischer"){Subject = "Softwareentwicklung", id = 5},
          };

          var query = from s in students
                      group s by s.Subject;

          foreach (var studentGroup in query)
          {
              Console.WriteLine(studentGroup.Key);
              foreach (Student student in studentGroup)
              {
                    Console.WriteLine("    {0}", student.Name);
              }
           }

           var query2 = from s in students
                        group s by s.Subject into sg
                        select new {Subject = sg.Key, Count = sg.Count()};

           Console.WriteLine();
           foreach (var group in query2){
             Console.WriteLine(group.Count + " students attend in " + group.Subject);
           }

        }
    }
}
```
@Rextester.eval(@CSharp)

### Sortieren

Bei einem Sortiervorgang werden die Elemente einer Sequenz auf Grundlage eines
oder mehrerer Attribute sortiert. Mit dem ersten Sortierkriterium wird eine
primäre Sortierung der Elemente ausgeführt. Sie können die Elemente innerhalb
jeder primären Sortiergruppe sortieren, indem Sie ein zweites Sortierkriterium
angeben.

In Beispiel unserer customer-Daten sortieren wir diese anhand der Wohnorte in
absteigender Reihenfolge. Als zweites Sortierkriterium werden dann die
Straßennamen herangezogen.

```csharp
var queryLondonCustomers = from customer in customers
                           orderby customer.City, customer.Street descending
                           select customer;
```

```csharp        SortExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Student{
      public string Name;
      public int id;
      public string Subject{get; set;}
      public Student(){}

      public Student(string name){
         this.Name = name;
      }
    }

    class Program {
        public static void Main(string[] args){

          List<Student> students = new List<Student>{
            new Student("Max Müller"){Subject = "Technische Informatik", id = 1},
            new Student("Maria Maier"){Subject = "Softwareentwicklung", id = 2},
            new Student("Martin Morawschek"){Subject = "Höhere Mathematik I", id = 3},
            new Student("Katja Schulz"){Subject = "Technische Informatik", id = 4},
            new Student("Karl Tischer"){Subject = "Softwareentwicklung", id = 5},
          };

           var query = from s in students
                       orderby s.Subject descending
                       select s;

           foreach (var student in query){
             Console.WriteLine("{0,-22} - {1}", student.Subject, student.Name);
           }

        }
    }
}
```
@Rextester.eval(@CSharp)

### Ausgaben

Die select-Klausel generiert aus den Ergebnissen der Abfrage das Resultat und
definiert damit das Format jedes zurückgegebenen Elements. Dies kann

+ den vollständigen Datensatz umfassen,
+ lediglich eine Teilmenge der Member oder
+ einen völlig neuen Datentypen.

Wenn die select-Klausel etwas anderes als eine Kopie des Quellelements erzeugt,
wird dieser Vorgang als Projektion bezeichnet.

```csharp        SelectExample
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Rextester
{
    class Student{
      public string Name;
      public int id;
      public string Subject{get; set;}
      public Student(){}

      public Student(string name){
         this.Name = name;
      }
    }

    class Program {
        public static void Main(string[] args){

          List<Student> students = new List<Student>{
            new Student("Max Müller"){Subject = "Technische Informatik", id = 1},
            new Student("Maria Maier"){Subject = "Softwareentwicklung", id = 2},
            new Student("Martin Morawschek"){Subject = "Höhere Mathematik I", id = 3},
            new Student("Katja Schulz"){Subject = "Technische Informatik", id = 4},
            new Student("Karl Tischer"){Subject = "Softwareentwicklung", id = 5},
          };

           var query = from s in students
                       select new {Surname = s.Name.Split(' ')[0]};

           Console.WriteLine(query.GetType());

           foreach (var student in query){
             Console.WriteLine(student.Surname);
           }

        }
    }
}
```
@Rextester.eval(@CSharp)

Einen guten Überblick zu den Konzequenzen einer Projektion gibt die Webseite
https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/concepts/linq/type-relationships-in-linq-query-operations


## Aufgabe der Woche

Für die Vereinigten Staaten liegen umfangreiche Datensätze zur Namensgebung von
Neugeborenen seit 1880 vor. Eine entsprechende csv-Datei (comma separated file)
findet sich im Projektordner und /data, sie umfasst 258.000 Einträge. Diese sind
wie folgt gegliedert

```
1880,"John",0.081541,"boy"
1880,"William",0.080511,"boy"
1880,"James",0.050057,"boy"
```

Die erste Spalte gibt das Geburtsjahr, die zweite den Vornamen, die Dritte den
Anteil der mit diesem Vornamen benannten Kinder und die vierte das Geschlecht an.

Der Datensatz steht zum Download unter
https://osf.io/d2vyg/
bereit.

Lesen Sie aus den Daten die jeweils am häufigsten vergebenen Vornamen aus und
bestimmen Sie deren Anteil innerhalb des Jahrganges.

https://github.com/liaScript/CsharpCourse/tree/master/code/25_LINQII

Welche Möglichkeiten der Analyse sehen Sie?
