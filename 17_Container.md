<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.10
language: de
narrator: Deutsch Female
comment:  Generelle Container und Datenkonzepte, Collections, Implementierung in Csharp und Anwendung der generischen Collections
tags:      
logo:     

import: https://github.com/liascript/CodeRunner
        https://raw.githubusercontent.com/liascript-templates/plantUML/master/README.md

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/17_Container.md)


# Collections


| Parameter                | Kursinformationen                                                                         |
| ------------------------ | ----------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                           |
| **Teil:**                | `17/27`                                                                                   |
| **Semester**             | @config.semester                                                                          |
| **Hochschule:**          | @config.university                                                                        |
| **Inhalte:**             | @comment                                                                                  |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/17_Container.md |
| **Autoren**              | @author                                                                                   |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Nachgefragt Records

```ascii
                                          C# Typen
                                              |
                          .----------------------------------------------.
                          |                                              |
                      Werttypen                                    Referenztypen
                          |                                              |
   .------+------+------+--+----+--------.      .-------+--------+-------+-------+--------.
   |      |      |      |       |        |      |       |        |       |       |        |
 Vordef. Enum  Structs record  Tupel    …    Klassen record    Inter-  Arrays Delegates  …
 Typen                 struct                (String) class    faces
   |
   |    ...............................................................
   |                      Benutzerdefinierte Typen
   |
   .----+------+-----------+-------------+----------.
   |           |           |             |          |
 Character  Ganzzahl  Gleitkommazahl    Bool       …
               |
        .------+---------.
        |                |
  mit Vorzeichen   vorzeichenlos                                                                    .
```

Die bisher behandelten Userdatentypen `struct` und `class` erfahren in C# 9.0 eine Erweiterung - `records`. Es wurden zwei Varianten integriert

+ `record` ist nur eine Abkürzung für eine `record class` - ein Referenztyp.
+ `record struct` ist ein Wertdatentyp.


```csharp    RecordsVsClass
using System;

public record PersonRecord(string FirstName, string LastName);

public class PersonClass
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class Program
{
  public static void Main()
  {
      // Darstellung mit Records
      var record_1 = new PersonRecord("Calvin", "Allen");
      var record_2 = new PersonRecord("Calvin", "Allen");

      Console.WriteLine(record_1);
      Console.WriteLine(record_1 == record_2);
      //record_1.FirstName = "Tralla";

      // Darstellung mit Klasseninstanzen

      var class_1 = new PersonClass(){
            FirstName = "John",
            LastName = "Doe"
        };
      var class_2 = new PersonClass(){
            FirstName = "John",
            LastName = "Doe"
        };

      Console.WriteLine(class_1);
      Console.WriteLine(class_1 == class_2);      
  }
}
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

- die Klassen-Instanzen werden nicht als gleich angesehen, obwohl die Daten in den Objekten gleich sind. Dies liegt daran, dass die beiden Variablen auf unterschiedliche Objekte verweisen.
- die Record-Instanzen werden als gleich angesehen. Dies liegt daran, dass Datensätze bei der Überprüfung auf Gleichheit nur Daten vergleichen.
- die Records implementieren verschiedene Methoden automatisch `ToString()`
- Records sind per default immutable!


> **Blick über den Tellerrand: Python `dataclass`**
>
> Wer aus Python kommt, kennt das Konzept bereits unter einem anderen Namen. Ein mit
> `@dataclass` dekoriertes Klassendefinition generiert ebenfalls automatisch
> `__init__`, `__repr__` (Pendant zu `ToString()`) und `__eq__` (Wertvergleich)  –
> exakt die Methoden, die ein C#-`record` mitbringt.

```python    DataclassExample.py
from dataclasses import dataclass

@dataclass
class PersonRecord:
    first_name: str
    last_name: str

record_1 = PersonRecord("Calvin", "Allen")
record_2 = PersonRecord("Calvin", "Allen")

print(record_1)              # repr wird automatisch erzeugt
print(record_1 == record_2)  # True - Vergleich über die Daten
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

Die Konzepte ähneln sich, unterscheiden sich aber in den Details:

| Aspekt              | C# `record`                                  | Python `@dataclass`                                      |
| ------------------- | -------------------------------------------- | --------------------------------------------------------- |
| Aktivierung         | eigenes Schlüsselwort `record`               | Dekorator `@dataclass` über einer `class`                |
| Wertgleichheit      | per default (`==` vergleicht Inhalte)        | per default (`__eq__` vergleicht Inhalte)                |
| String-Ausgabe      | `ToString()` automatisch                     | `__repr__` automatisch                                   |
| Unveränderlichkeit  | per default immutable (`init`-only)          | nur via `@dataclass(frozen=True)`                        |
| Wert- vs. Referenz  | `record class` (Ref) bzw. `record struct` (Wert) | immer Referenztyp (Objekt auf dem Heap)             |
| Typprüfung          | statisch zur Compile-Zeit                    | Typannotationen sind Hinweise, zur Laufzeit nicht erzwungen |

> Merke: Beide nehmen dem Entwickler den Boilerplate-Code für datenhaltende Klassen
> ab. C# trifft dabei die strengere, statisch geprüfte Variante.


## Nachgefragt — Anwendung Generics

In der vergangenen Vorlesung haben wir generische Typen mit _einem_ Typparameter
kennengelernt (`LinkedList<T>`, `Stack<T>`). Bevor wir uns die fertigen Collections
des .NET-Frameworks ansehen, schlagen wir die Brücke und bauen selbst einen
_assoziativen_ Container - ein Dictionary, das Schlüssel auf Werte abbildet. Es
nutzt gleich **zwei** Typparameter: `TKey` für den Schlüssel und `TValue` für den Wert.

Die Idee ist bewusst einfach gehalten: Wir speichern die Einträge intern als Liste von
Schlüssel-Wert-Paaren ([`KeyValuePair<TKey, TValue>`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.keyvaluepair-2?view=net-9.0) -
ein vordefinierter `struct` aus `System.Collections.Generic`, der genau einen Schlüssel
und einen Wert über die Eigenschaften `.Key` und `.Value` zusammenfasst) und suchen linear. Das echte
`Dictionary<TKey, TValue>` arbeitet stattdessen mit einer Hash-Tabelle und ist damit
deutlich schneller - das Prinzip der generischen Parametrisierung ist aber dasselbe.

```csharp      EigenesDictionary
using System;
using System.Collections.Generic;

// Ein einfaches, generisches Dictionary mit zwei Typparametern:
// TKey für den Schlüssel, TValue für den Wert.
public class MyDictionary<TKey, TValue>
{
    // Intern halten wir die Einträge als Liste von Schlüssel-Wert-Paaren.
    private List<KeyValuePair<TKey, TValue>> items = new List<KeyValuePair<TKey, TValue>>();

    // Indexer: erlaubt den Zugriff über myDict[key]
    public TValue this[TKey key]
    {
        get
        {
            foreach (var pair in items)
                if (pair.Key.Equals(key))
                    return pair.Value;
            throw new KeyNotFoundException($"Schlüssel '{key}' nicht gefunden.");
        }
        set
        {
            // Existiert der Schlüssel bereits, überschreiben wir den Wert ...
            for (int i = 0; i < items.Count; i++)
                if (items[i].Key.Equals(key))
                {
                    items[i] = new KeyValuePair<TKey, TValue>(key, value);
                    return;
                }
            // ... andernfalls fügen wir ein neues Paar an.
            items.Add(new KeyValuePair<TKey, TValue>(key, value));
        }
    }

    public int Count => items.Count;

    public bool ContainsKey(TKey key)
    {
        foreach (var pair in items)
            if (pair.Key.Equals(key))
                return true;
        return false;
    }
}

public class Program
{
    public static void Main()
    {
        // Dank der Typparameter funktioniert derselbe Container für
        // string->int genauso wie für jede andere Typkombination.
        var telefonbuch = new MyDictionary<string, int>();
        telefonbuch["Peter"] = 1234;
        telefonbuch["Paula"] = 5234;
        telefonbuch["Peter"] = 9999;   // überschreibt den vorhandenen Eintrag

        Console.WriteLine("Einträge: " + telefonbuch.Count);
        Console.WriteLine("Peter:    " + telefonbuch["Peter"]);
        Console.WriteLine("Enthält 'Paula'? " + telefonbuch.ContainsKey("Paula"));
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Beachten Sie, dass dieselbe Implementierung ohne jede Änderung auch für andere
Typkombinationen funktioniert - etwa `MyDictionary<int, Animal>`. Genau diese
Wiederverwendbarkeit ist der Kern der Generics. Im nächsten Abschnitt sehen wir, wie
das .NET-Framework diese Idee mit `Dictionary<TKey, TValue>`, `List<T>` & Co. zu Ende
denkt.


## Collections

> **Merke:** Sogenannte Container sind ein zentrales Element jeder Klassenbibliothek. Sie erlauben die Abbildung verschiedener Entitäten in einem Objekt. Im Kontext von C# wird dabei von _Collections_ gesprochen.

In der vergangen Vorlesung haben wir über die Vorteile von generischen Speicherstrukturen am Beispiel der Liste gesprochen. Allerdings ist die Möglichkeit durch die Struktur hindurchzuiterieren nicht immer die günstigste. In dieser Vorlesung wollen wir alternative Konzepte und deren Implementierung im C# Framework untersuchen.

Beginnen wir zunächst mit einem Vergleich einiger listenähnlichen Konstrukte. Diese sind in den Namespaces `System.Collections` und `System.Collections.Generic` enthalten.


```csharp      ContainerTypes.cs
using System;
using System.Collections;
using System.Collections.Generic;

public class Animal
{
    public string name;
    public Animal(string name){
        this.name = name;
    }
}

public class Program{
    public static void Main(string[] args){
        Animal[] arrayOfAnimals = new Animal[3]
        {
            new Animal("Beethoven"),
            new Animal("Kitty"),
            new Animal("Wally"),
        };
        ArrayList listOfAnimals = new ArrayList()
        {
            new Animal("Beethoven"),
            new Animal("Kitty"),
            new Animal("Wally"),
        };
        List<Animal> genericlistOfAnimals = new List<Animal>()
        {
            new Animal("Beethoven"),
            new Animal("Kitty"),
            new Animal("Wally"),
        };
        foreach (Animal pet in listOfAnimals){
            Console.WriteLine(pet.name);
        }
        listOfAnimals.RemoveAt(1);
        listOfAnimals.Add(new Animal("Flipper"));
        Console.WriteLine();
        foreach (Animal pet in listOfAnimals){
            Console.WriteLine(pet.name);
        }
        Console.WriteLine("\n");
    }
}
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)


Worin liegt der Unterschied zu den bereits bekannten `Array` Implementierung?

| Feature             | Array                          | `ArrayList`                             | `List<T>`        |
| ------------------- | ------------------------------ | --------------------------------------- | ----------------- |
| Generisch?          | nein                           | nein                                    | ja                |
| Anzahl der Elemente | feste Größe                    | variabel                               | variabel          |
| Datentyp            | muss homogen sein (typsicher)  | kann variieren (nicht streng typisiert) | muss homogen sein |
| null                | nicht akzeptiert               | wird akzeptiert                         | wird akzeptiert   |
| Dimensionen         | multidimensional `array[X][Y]` | -                                       | -                 |

Die Methoden von `ArrayList` sind zum Beispiel unter https://learn.microsoft.com/de-de/dotnet/api/system.collections.arraylist?view=net-8.0 zu finden.

Dabei setzen die vielfältigen Methoden Anforderungen an die im Container gespeicherten Werte.

```csharp      ArrayExamples.cs
using System;

public class Point
{
    public int x;
    public int y;
    public Point(int x, int y){
        this.x = x;
        this.y = y;
    }
}

public class ArrayExamples  {

  // Return true if X times Y is greater than 100000.
  private static bool ProductGT10(Point p)
  {
      return p.x * p.y > 100000;
  }

  public static void Main()
  {
     // Example 1 - Setzen
     String[,] myArr2 = new String[5,5];
     myArr2.SetValue( "one-three", 1, 3 );
     Console.WriteLine( "[1,3]:   {0}", myArr2.GetValue( 1, 3 ) );

     // Example 2 - Sortieren
     String[] words = { "The", "QUICK", "BROWN", "FOX", "jumps",
                        "over", "the", "lazy", "dog" };
     Array.Sort(words, 1, 3);
     foreach (var word in words){
         Console.Write(word + " ");
     }
     Console.WriteLine("\n");

     // Example 3 - Suchen
     // Create an array of five Point structures.
     Point[] points = { new Point(100, 200),
         new Point(150, 250), new Point(250, 375),
         new Point(275, 395), new Point(295, 450) };
     // Find the first Point structure for which X times Y
     // is greater than 100000.
     Point first = Array.Find(points, ProductGT10);
     // Display the first structure found.
     Console.WriteLine("Found: X = {0}, Y = {1}", first.x, first.y);
  }
}
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)




### Einordnung

                              {{0-3}}
********************************************************************************

Neben den genannten existieren weitere Typen, die spezifischere Aufgaben umsetzen. Diese können entweder als sequenzielle oder als assoziative Container klassifiziert werden.

Container (in der C#-Welt sprechen wir von Collections) können durch die folgenden drei Eigenschaften charakterisiert werden:

1. __Zugriff__, d.h. die Art und Weise, wie auf die Objekte des Containers zugegriffen wird. Im Falle von Arrays erfolgt der Zugriff über den Array-Index. Im Falle von Stapeln (_Stack_) erfolgt der Zugriff nach der LIFO-Reihenfolge (last in, first out) und im Falle von Warteschlangen (_Queue_) nach der FIFO-Reihenfolge (first in, first out);
2. __Speicherung__, d.h. die Art und Weise, wie die Objekte des Containers gelagert werden;
3. __Durchlaufen__, d.h. die Art und Weise, wie die Objekte des Containers iteriert werden.

********************************************************************************

                                        {{1-3}}
***************************************************************************

Von den Containerklassen wird entsprechend erwartet, dass sie folgende Methoden implementieren:

+ einen leeren Container erzeugen (Konstruktor);
+ Einfügen von Objekten in den Container;
+ Objekte aus dem Container löschen;
+ alle Objekte im Container löschen;
+ auf die Objekte im Container zugreifen;
+ auf die Anzahl der Objekte im Container zugreifen.

***************************************************************************

                                        {{2-3}}
***************************************************************************
> __Sequenzielle-Container__ speichern jedes Objekt unabhängig voneinander. Auf Objekte kann direkt oder mit einem Iterator zugegriffen werden.

> Ein __assoziativer Container__ verwendet ein assoziatives Array, eine Karte oder ein Wörterbuch, das aus Schlüssel-Wert-Paaren besteht, so dass jeder Schlüssel höchstens einmal im Container erscheint. Der Schlüssel wird verwendet, um den Wert, d.h. das Objekt, zu finden, falls es im Container gespeichert ist.

***************************************************************************

                                        {{3-4}}
***************************************************************************

Welche Container-Typen sind programmiersprachenunabhängig gängig?

| Typ        | Unmittelbarer Zugriff      | Beschreibung                                      |
| ---------- | -------------------------- | ------------------------------------------------- |
| Dictionary | via Key                    | Wert-Schlüssel Paar                               |
| Liste      | via Index                  | Folge von Elementen mit einem Index als Schlüssel |
| Queue      | nur jeweils erstes Objekt  | FIFO (First-In-First-Out) Speicher                |
| Stack      | nur jeweils letztes Objekt | LIFO (Last-In-First-Out) Speicher                 |
| Set        |                            | Werte ohne Duplikate                              |
| ...        |                            |                                                   |

***************************************************************************

                            {{4-5}}
********************************************************************************

Fragenkatalog für die Auswahl von Collections:

| Frage                                                                      | Mögliche Lösungen                                                         |
| -------------------------------------------------------------------------- | ------------------------------------------------------------------------- |
| Sollen Elemente nach dem Auslesen verworfen werden?                        | `Queue<T>`, `Stack<T>`                                                    |
| Benötigen Sie Zugriff auf die Elemente in einer bestimmten Reihenfolge?    | `Queue<T>` vs. `LinkedList<T>`                                            |
| Wird die Collection in einer nebenläufigen Anwendung eingesetzt?           |                                                                           |
| Benötigen Sie Zugriff auf jedes Element über den Index?                    | `ArrayList`, `StringCollection`  und `List<T>` vs. assoziativer Container |
| Sollen die Dateninhalte unveränderlich sein?                               | `ImmutableArray<T>`, `ImmutableList<T>`                                   |
| Erfolgt die Indizierung anhand der Position oder anhand eines Schlüssels?  |                                                                           |
| Müssen Sie die Elemente abweichend von ihrer Eingabereihenfolge sortieren? | `SortedList<TKey,TValue>`                                                 |
| Soll der Container nur Zeichenfolgen annehmen?                             | `StringCollection`                                                        |

********************************************************************************

### Performance

Spannend wird es, wenn die
**Wahl des Containers die Komplexitätsklasse der Operation verändert**. Betrachten
wir dazu die wohl häufigste Frage an einen Container: _"Enthältst du Element x?"_

| Container             | Suchstrategie          | Aufwand    |
| --------------------- | ---------------------- | ---------- |
| `List<T>`             | lineare Suche          | $O(n)$     |
| `SortedList<K,V>`     | binäre Suche (sortiert)| $O(\log n)$|
| `HashSet<T>`          | Hash-Lookup            | $O(1)$     |

Hier geht es nicht mehr um den Faktor 2 oder 5, sondern um den Unterschied
zwischen Sekunden und Mikrosekunden – und der wächst mit der Datenmenge. Das
folgende Beispiel füllt drei Container mit `N` Elementen und führt anschließend
`M` zufällige Lookups durch:

```csharp     Program.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
  static void Main()
  {
    int N = 100_000;   // Anzahl der Elemente im Container
    int M =  50_000;   // Anzahl der Suchanfragen

    // --- Container befüllen ---------------------------------------
    var list = new List<int>();
    var sorted = new SortedSet<int>();      // intern als Baum, O(log n) Lookup
    var hash = new HashSet<int>();
    for (int i = 0; i < N; i++)
    {
        list.Add(i);
        sorted.Add(i);
        hash.Add(i);
    }

    // Wonach gesucht wird (deterministisch, damit fair vergleichbar)
    var queries = new int[M];
    for (int i = 0; i < M; i++)
        queries[i] = (i * 2654435761L % N) > 0 ? (int)(i * 2654435761L % N) : 0;

    // --- Messung --------------------------------------------------
    Console.WriteLine($"N = {N:N0} Elemente, M = {M:N0} Suchanfragen\n");

    Measure("List<int>     O(n)   ", () => {
        foreach (var q in queries) _ = list.Contains(q);
    });
    Measure("SortedSet<int> O(log n)", () => {
        foreach (var q in queries) _ = sorted.Contains(q);
    });
    Measure("HashSet<int>   O(1)   ", () => {
        foreach (var q in queries) _ = hash.Contains(q);
    });
  }

  static void Measure(string name, Action action)
  {
    var sw = Stopwatch.StartNew();
    action();
    sw.Stop();
    Console.WriteLine($"{name} : {sw.Elapsed.TotalMilliseconds,10:N2} ms");
  }
}
```
```xml   -project.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

> **Beobachtung:** Alle drei Container speichern _dieselben_ Daten und liefern
> beim `Contains` _dasselbe_ Ergebnis. Trotzdem liegen zwischen `List` und
> `HashSet` typischerweise mehrere Größenordnungen. Verdoppeln Sie `N` und
> beobachten Sie, wie sich die Zeiten verändern: bei `List` ungefähr ×2, beim
> `HashSet` kaum. Genau das ist der Unterschied zwischen $O(n)$ und $O(1)$ –
> und der Grund, warum die Auswahl des passenden Containers (vgl. Fragenkatalog
> oben) wichtiger ist als ein konstanter Geschwindigkeitsfaktor.


## Containerimplementierung in Csharp

Um die Konzepte der Implementierung der Container in C# zu verstehen, versuchen wir uns nochmal an einem eigenen Konstrukt. Wir systematisieren dazu die Idee der verlinkten Liste aus der vorangegangen Veranstaltung und fokussieren uns zunächst auf die Möglichkeit mit den C#-Bordmitteln über dieser Liste zu iterieren.

Zur Erinnerung, für die Möglichkeit der Iteration über einer Datenstruktur mittels `foreach` bedarf es der Implementierung der Interfaces `IEnumerable` und `IEnumerator`. Wir verbleiben dabei auf der generischen Seite.

```csharp      GenericList.cs
using System;
using System.Collections;
using System.Collections.Generic;

public class GenericList<T> : IEnumerable<T>
{
	protected Node head;
	protected Node current = null;
	// Nested class is also generic on T
	protected class Node
	{
		public Node next;
		private T data;
		public Node(T t){
			next = null;
			data = t;
		}
		public Node Next {
			get { return next; }
			set { next = value; }
		}
		public T Data {
			get { return data; }
			set { data = value; }
		}
	}

	public GenericList(){
		head = null;
	}

	public void Add(T t)  {
		Node n = new Node(t);
		n.Next = head;
		head = n;
	}

	// Implementation of the iterator
	public IEnumerator<T> GetEnumerator(){
		Node current = head;
		while (current != null)
		{
			yield return current.Data;
			current = current.Next;
		}
	}

	IEnumerator IEnumerable.GetEnumerator(){
		return GetEnumerator();
	}
}

public class Animal
{
	string name;
	int age;
	public Animal(string s, int i){
		name = s;
		age = i;
	}
	public override string ToString() => name + " : " + age;
}

class Program
{
	public static void Main(string[] args)
	{
		GenericList<Animal> animalList = new GenericList<Animal>();
    animalList.Add(new Animal("Beethoven", 8));
    animalList.Add(new Animal("Kitty", 4));
		foreach (Animal a in animalList)
		{
			System.Console.WriteLine(a.ToString());
		}
	}
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Achtung:** Das Beispiel implmentiert das Iteratorkonzept mittels [`yield`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/keywords/yield). Damit lässt sich einige Tipparbeit sparen, die bei der konventionellen Umsetzung anfallen würde, vgl [Link](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement).

Die Methoden für das Handling der Daten beschränken sich aber auf ein `Add()` und die Iteration - hier braucht es noch deutlich mehr, um anwendbar zu sein. Um diese Funktionalität umzusetzen, greift die C#-Collections Implementierung auf eine ganze Reihe von Interfaces zurück, die den einzelnen Containern die notwendige Funktion geben.


```text @plantUML
@startuml
skinparam classAttributeIconSize 0
hide circle
hide Method
hide Field

package "Non-generic" {
  interface IEnumerator
  interface IEnumerable
  interface ICollection
  interface IDictionary
  interface IList

  IEnumerable ..> IEnumerator : GetEnumerator()
  IEnumerable <|-- ICollection
  ICollection <|-- IDictionary
  ICollection <|-- IList
}

package "Generic" {
  interface "IEnumerator<T>"  as IEnumeratorT
  interface "IEnumerable<T>"  as IEnumerableT
  interface "ICollection<T>"  as ICollectionT
  interface "IDictionary<K,V>" as IDictionaryT
  interface "IList<T>"        as IListT

  IEnumerableT ..> IEnumeratorT : GetEnumerator()
  IEnumerableT <|-- ICollectionT
  ICollectionT <|-- IDictionaryT
  ICollectionT <|-- IListT
}

IEnumerable <|-- IEnumerableT
@enduml
```

An dieser Stelle greift das Interface `ICollection` und definiert die Methoden `Add`, `Clear`, `Contains`, `CopyTo` und `Remove`. Mit `Contains` kann geprüft werden, ob ein bestimmter Wert im Container enthalten ist. `CopyTo` extrahiert die Werte des Containers in ein Array. Dabei können bestimmte Ranges definiert werden. Die anderen Methoden sind selbsterklärend.

| Schnittstelle | Spezifizierte Funktionen        |
| ------------- | ------------------------------- |
| IEnumerable   | GetEnumerator()                 |
| ICollection   | Count(), Add(), Remove()        |
| IList         | IndexOf(), Insert(), RemoveAt() |
| IDictionary   | Keys(), Values(), TryGetValue() |

Folgendes Klassendiagramm zeigt die Teile der in C# implementierten Collection-Types
und deren Relationen zu den entsprechenden Interfaces.

```text @plantUML
@startuml
skinparam classAttributeIconSize 0
hide circle
hide Method
hide Field

class IEnumerator <T> <<interface>> 
class IEnumerable <T> <<interface>>
class ICollection <T> <<interface>>
class IList <T>  <<interface>>
class IDictionary <K, V> <<interface>>

class List <T> #green
class Queue <T> #green
class Stack <T> #green
class LinkedList <T> #green
class Dictionary <K, V>  #green
class SortedList <K, V>  #green
class SortedDict <K, V>  #green

IEnumerable ..> IEnumerator : GetEnumerator()
ICollection --|> IEnumerable
IDictionary --|> ICollection
IList --|> ICollection

Dictionary ..|> IDictionary
SortedDict ..|> IDictionary
SortedList ..|> IDictionary
List ..|> IList
LinkedList ..|> IList
Queue ..|> ICollection
Stack ..|> ICollection
@enduml
```

> **Zur Beziehung zwischen `IEnumerable` und `IEnumerator`:** Beachten Sie, dass diese
> Relation als gestrichelte _Abhängigkeit_ (`..>`) und nicht als Komposition gezeichnet
> ist. Ein Interface ist ein reiner Vertrag - es besitzt keine Felder und kann daher
> kein anderes Objekt im Sinne einer Komposition "enthalten". `IEnumerable` verspricht
> lediglich über die Methode `GetEnumerator()`, bei Bedarf einen `IEnumerator` zu
> _erzeugen_. Dieser Iterator ist ein eigenständiges, kurzlebiges Objekt (jeder
> `foreach`-Durchlauf fordert typischerweise einen neuen an); seine Lebensdauer hängt am
> Aufrufer, nicht am `IEnumerable`. Genau das drückt die Abhängigkeitsbeziehung aus.


                                     {{1-2}}
********************************************************************************

Im Folgenden sollen Beispiele für die aufgeführten Datenstrukturen dargestellt werden.

| C# Collection | Bezeichnung                                   | Bedeutung                                                                                                        |                                                                                                              |
| ------------- | --------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| List          | unsortiertes Datenfeld indizierbarer Elemente | Im Unterschied zum Array "beliebig" erweiterbar                                                                  | [Link](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1?view=net-9.0)  |
| SortedList    | sortiertes Datenfeld                          | Abbildung der Reihenfolge über einen numerischen Schlüssel                                                       |    [Link](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.sortedlist-2?view=net-9.0)                                                                                                          |
| Stack         | LIFO Datenstruktur                            |                                                                                                                  | [Link](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.stack-1?view=net-9.0) |
| Queue         | FIFO Datenstruktur                            |                                                                                                                  | [Link](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.queue-1?view=net-9.0) |
| Dictionary     | assoziatives Datenfeld                        | ... Datenstruktur mit nicht-numerischen (fortlaufenden ) Schlüsseln, um die enthaltenen Elemente zu adressieren. |  [Link](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0)                                                                                                            |

********************************************************************************

## Anwendung der Generic Collections

List<T>
=====================

```csharp      ListExample
using System;
using System.Reflection;
using System.Collections.Generic;
// Wird für die elementweise Verarbeitung benötigt
using System.Linq;

public class Program{
  public static void Main(string[] args){
    // Initialisieren mit Basiswerten, Ergänzungen der Liste
    var animals = new List<string>() { "bird", "dog" };
    animals.Add("cat");
    animals.Add("lion");
    // Fügt mehrere Objekte in die Liste ein
    animals.InsertRange(1, new string[] { "frog", "snake" });
    animals.ForEach(name => Console.WriteLine(name.ToUpper()));
    animals.ForEach(Console.WriteLine);

    Console.WriteLine("In der Liste finden sich " + animals.Count + " Elemente");
    Console.WriteLine("Für die Liste reservierter Speicher (Einträge) " + animals.Capacity);
    Console.WriteLine("lion findet sich an " + animals.IndexOf("lion") + " Stelle");
    animals.Remove("lion");
    Console.WriteLine("In der Liste finden sich nun " + animals.Count + " Elemente");
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Dictionary<T, U>
=====================

```csharp      DictionaryExample
using System;
using System.Reflection;
using System.Collections.Generic;


public class Program{
  public static void Main(string[] args){
     Dictionary<string, int> Telefonbuch = new Dictionary<string, int>();
     Telefonbuch.Add("Peter", 1234);
     Telefonbuch.Add("Paula", 5234);
     foreach( string s in Telefonbuch.Keys   )
     {
        Console.Write("Key = {0}\n", s);
     }
     foreach (KeyValuePair<string, int> entry in Telefonbuch) {
         Console.WriteLine("Key = {0}, Value = {1}", entry.Key, entry.Value);
     }    
     // Enthält das Dictionary bestimmte Einträge?
     if (Telefonbuch.ContainsKey("Paula")){
         Console.WriteLine(Telefonbuch["Paula"]);
     }
     // Effektiver Zugriff
     int value;
     string key = "Peter";
     if (Telefonbuch.TryGetValue(key, out value))
     {
         Telefonbuch[key] = value + 1;
         Console.WriteLine("Wert von " + key + " " + Telefonbuch[key]);
     }
     // Mehrfache Nennung eines Eintrages
  }
}
```
```xml   -myproject.csproj
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
</Project>
```
@LIA.eval(`["Program.cs", "project.csproj"]`, `dotnet build -nologo`, `dotnet run -nologo`)

HashSet<T>
=====================

```csharp      HashSetExample
using System;
using System.Reflection;
using System.Collections.Generic;

public class Program{
  public static void Main(string[] args){
     HashSet<string> Telefonbuch1 = new HashSet<string>();
     Telefonbuch1.Add("Peter");
     Telefonbuch1.Add("Paula");
     Telefonbuch1.Add("Nadja");
     Telefonbuch1.Add("Paula");
     Console.Write("Telefonbuch 1: ");
     foreach(string s in Telefonbuch1){
        Console.Write(s + " ");
     }

     HashSet<string> Telefonbuch2 = new HashSet<string>();
     Telefonbuch2.Add("Klaus");
     Telefonbuch2.Add("Paula");
     Telefonbuch2.Add("Nadja");
     Console.Write("\nTelefonbuch 2: ");
     foreach(string s in Telefonbuch2){
        Console.Write(s + " ");
     }

     //Telefonbuch1.ExceptWith(Telefonbuch2);
     Telefonbuch1.UnionWith(Telefonbuch2);
     Console.Write("\nMerge       2: ");
     foreach(string s in Telefonbuch1){
        Console.Write(s + " ");
     }
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

```csharp
public class a : IEqualityComparer<a>
{
  public int GetHashCode(a obj) { /* Implementation */ }
  public bool Equals(a obj1, a obj2) { /* Implementation */ }
}
```

## Wert- und Referenztypen in Containern

Beim Arbeiten mit Collections wird die anfangs eingeführte Unterscheidung zwischen
Wert- und Referenztypen plötzlich praktisch relevant. Eine Collection speichert
Werttypen (`struct`, `int`, ...) **als Kopie**, Referenztypen (`class`) dagegen **als
Verweis** auf das eigentliche Objekt. Das hat eine wichtige Konsequenz: Was passiert,
wenn wir ein Element aus dem Container herausholen und verändern?

```csharp      WertVsReferenzImContainer
using System;
using System.Collections.Generic;

public struct PunktStruct { public int X; }   // Werttyp
public class  PunktClass  { public int X; }   // Referenztyp

public class Program
{
    public static void Main()
    {
        // --- Werttyp in der Liste ---
        var structListe = new List<PunktStruct> { new PunktStruct { X = 1 } };
        PunktStruct s = structListe[0];   // liefert eine KOPIE
        s.X = 99;                         // ändert nur die Kopie
        Console.WriteLine("struct in Liste: " + structListe[0].X);   // -> 1

        // --- Referenztyp in der Liste ---
        var classListe = new List<PunktClass> { new PunktClass { X = 1 } };
        PunktClass c = classListe[0];     // liefert die REFERENZ
        c.X = 99;                         // ändert das Objekt in der Liste
        Console.WriteLine("class in Liste:  " + classListe[0].X);    // -> 99
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Das Ergebnis überrascht auf den ersten Blick:

+ Bei der **`struct`-Liste** liefert `structListe[0]` eine _Kopie_. Die Änderung `s.X = 99`
  wirkt nur auf diese lokale Kopie - das Element in der Liste bleibt unverändert (`1`).
+ Bei der **`class`-Liste** liefert `classListe[0]` die _Referenz_ auf dasselbe Objekt.
  Die Änderung `c.X = 99` schlägt damit direkt auf den Listeneintrag durch (`99`).

> **Merke:** Wer einen Werttyp aus einem Container holt, verändert, und das Ergebnis
> zurückerwartet, muss das geänderte Element explizit wieder zurückschreiben
> (`structListe[0] = s;`). Bei Referenztypen entfällt das - dort teilen sich Variable und
> Container ohnehin dasselbe Objekt. Genau deshalb sind unveränderliche Werttypen
> (`readonly struct`, `record struct`) in Collections oft die robustere Wahl.


## Achtung!

Die heute besprochenen Inhalte finden sich in verschiedenen Formen in allen höheren Programmiersprachen wieder.

```python    SetInPython.py
# initialize my_set
my_set = {1, 3}
print(my_set)

# my_set[0]
# if you uncomment the above line
# you will get an error
# TypeError: 'set' object does not support indexing

# add an element
# Output: {1, 2, 3}
my_set.add(2)
print(my_set)

# add multiple elements
my_set.update([2, 3, 4])
print(my_set)

your_set = {4, 5, 6, 7, 8}
print(your_set)

print(my_set | your_set)
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

## Aufgaben der Woche

- [ ] Erklären Sie, warum `Array` keine `Add`-Methode umfasst, obwohl es das Interface `IList` implementiert, dass wiederum diese einschließt. Tipp: Rufen Sie Ihr wissen um die explizite Methodenimplementierung noch mal auf.
- [ ] Die Erläuterung zu den Beschränkungen beim Einsatz von Generics in Vorlesung 16 (Generics) basiert auf der nicht generischen Implementierung des Interfaces `IComparable`. Ersetzen Sie diese im Codebeispiel durch die  generische Variante.
- [ ] Evaluieren Sie verschiedene Container in Bezug auf Methoden zum Einfügen, Löschen, etc. Generieren Sie dazu entsprechende künstliche Objekte, die Sie manipulieren _"Füge 100.000 int Werte in eine Liste ein."_. Messen Sie die dafür benötigten Zeiten.
