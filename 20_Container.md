<!--

author:   Sebastian Zug, Galina Rudolf & André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.2
language: de
narrator: Deutsch Female

import: https://github.com/liascript/CodeRunner

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/20_Container.md)

# Generics

| Parameter                | Kursinformationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                                                                                                            |
| **Semester**             | `Sommersemester 2021`                                                                                                                                                                      |
| **Hochschule:**          | `Technische Universität Freiberg`                                                                                                                                                          |
| **Inhalte:**             | `Containertypen und deren Implementierung in C#`                                                                                                                                |
| **Link auf den GitHub:** | [https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/20_Container.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/20_Container.md) |
| **Autoren**              | @author                                                                                                                                                                                    |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Containerklassen

                              {{0-1}}
********************************************************************************

In der vergangen Vorlesung haben wir über die Vorteile von generischen Speicherstrukturen am Beispiel der Liste gesprochen. Allerdings ist die Möglichkeit durch die Struktur hindurchzuiterieren nicht immer die günstigste. In dieser Vorlesung wollen wir alternative Konzepte und deren Implementierung im C# Framework untersuchen.

Beginnen wir zunächst mit einem Vergleich der listenähnlichen Konstrukte. Diese sind in den Namespaces `System.Collections` und `System.Collections.Generic` enthalten. Um zu vermeiden, dass diese beständig mitgeführt werden, betten wir sie mit `using` in unseren Code ein.

```csharp      ContainerTypes.cs
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
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
      }
  }
}
```
@Rextester.eval(@CSharp)

Worin liegt der Unterschied zu den bereits bekannten `Array` Implementierung?

| Feature             | Array                           | `ArrayList`                             | `Array<T>`          |
| ------------------- | ------------------------------- | --------------------------------------- | ------------------- |
| Generisch?          | nein                            | nein                                    | ja                    |
| Anzahl der Elemente | feste Größe                     | variabel                                | variabel            |
| Datentyp            | muss heterogen sein (typsicher) | kann variieren (nicht streng typisiert) | muss heterogen sein |
| null                | nicht akzeptiert                | wird akzeptiert                         | wird akzeptiert     |
| Dimensionen         | multidimensional `array[X][Y]`  | -                                       | -                   |

Die Methoden von `ArrayList` sind zum Beispiel unter https://docs.microsoft.com/de-de/dotnet/api/system.collections.arraylist?view=netcore-3.1 zu finden.

********************************************************************************

                              {{1-2}}
********************************************************************************

Es existieren noch weitere Typen, die spezifischere Aufgaben umsetzen. Diese können entweder als sequenzielle oder als assoziative Container klassifiziert werden.

Container (in der C#-Welt sprechen wir von Collections) können durch die folgenden drei Eigenschaften charakterisiert werden:

1. Zugriff, d.h. die Art und Weise, wie auf die Objekte des Containers zugegriffen wird. Im Falle von Arrays erfolgt der Zugriff über den Array-Index. Im Falle von Stapeln erfolgt der Zugriff nach der LIFO-Reihenfolge (last in, first out) und im Falle von Warteschlangen nach der FIFO-Reihenfolge (first in, first out);
2. Speicherung, d.h. die Art und Weise, wie die Objekte des Containers gelagert werden;
3. Durchlaufen, d.h. die Art und Weise, wie die Objekte des Containers iteriert werden.

Von den Containerklassen wird entsprechend erwartet, dass sie folgende Methoden implementieren:

+ einen leeren Container erzeugen (Konstruktor);
+ Einfügen von Objekten in den Container;
+ Objekte aus dem Container löschen;
+ alle Objekte im Container löschen;
+ auf die Objekte im Container zugreifen;
+ auf die Anzahl der Objekte im Container zugreifen.

Sequenzielle-Container speichern jedes Objekt unabhängig voneinander. Auf Objekte kann direkt oder mit einem Iterator zugegriffen werden.

Ein assoziativer Container verwendet ein assoziatives Array, eine Karte oder ein Wörterbuch, das aus Schlüssel-Wert-Paaren besteht, so dass jeder Schlüssel höchstens einmal im Container erscheint. Der Schlüssel wird verwendet, um den Wert, d.h. das Objekt, zu finden, falls es im Container gespeichert ist.

Welche Container-Typen sind programmiersprachenunabhängig gängig?

| Typ        | Unmittelbarer Zugriff      | Beschreibung                                      |
| ---------- | -------------------------- | ------------------------------------------------- |
| Dictionary | via Key                    | Wert-Schlüssel Paar                               |
| Liste      | via Index                  | Folge von Elementen mit einem Index als Schlüssel |
| Queue      | nur jeweils erstes Objekt  | FIFO (First-In-First-Out) Speicher                |
| Stack      | nur jeweils letztes Objekt | LIFO (Last-In-First-Out) Speicher                 |
| Set        |                            | Werte ohne Dublikate                              |
| ...        |                            |                                                   |

********************************************************************************

                            {{2-3}}
********************************************************************************

Und wie sieht es mit der Performance aus? Der Beitrag des Autors `Serj-Tm` auf Stackoverflow vergleicht in einem Codebeispiel unterschiedliche Operationen für verschiedene Container-Typen.

| Array            | `List<T>`        | Penalties | Method    |
| ---------------- | ---------------- | --------- | --------- |
| 00:00:01.3932446 | 00:00:01.6677450 | 1 vs  1,2 | Generate  |
| 00:00:00.1856069 | 00:00:01.0291365 | 1 vs  5,5 | Sum       |
| 00:00:00.4350745 | 00:00:00.9422126 | 1 vs  2,2 | BlockCopy |
| 00:00:00.2029309 | 00:00:00.4272936 | 1 vs  2,1 | Sort      |

********************************************************************************

## Containerimplementierung in Csharp

Um die Konzepte der Implementierung der Container in C# zu verstehen versuchen wir uns nochmal an einem eigenen Konstrukt. Wir systematisieren dazu die Idee der verlinkten Liste aus der vorangegangen Veranstaltung und fokussieren uns zunächst auf die Möglichkeit mit den C#-Bordmitteln über dieser Liste zu iterieren.

Zur Erinnerung, für die Möglichkeit der Iteration über einer Datenstruktur mittels `foreach` bedarf es der Implementierung der Interfaces `IEnumerable` und `IEnumerator`. Wir verbleiben dabei auf der generischen Seite.

```csharp      GenericList.cs
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rextester
{
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
}
```
@Rextester.eval(@CSharp)

Die Methoden für das Handling der Daten beschränken sich aber auf ein `add()`, hier braucht es noch deutlich mehr um anwendbar zu sein. Um diese Funktionalität umzusetzen greift die C#-Collections Implementierung auf eine ganze Reihe von Interfaces zurück, die den einzelnen Containern die notwendige Funktion geben.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
````````````

                Non-generic                             Generic

            .----------------.                     .----------------.
            | IEnumerator    |                     | IEnumerator﹤T﹥ |
            .----------------.                     .----------------.

            .----------------.                     .----------------.
            | IEnumerable    | ⊲------------------ | IEnumerable﹤T﹥ |
            .----------------.                     .----------------.
                    ∆                                       ∆
                    |                                       |
            .----------------.                     .----------------.
            | ICollection    |                     | ICollection﹤T﹥ |
            .----------------.                     .----------------.
                    ∆                                       ∆
                    |                                       |
        .-----------+----------.                  .---------+----------.
        |                      |                  |                    |
 .----------------.  .----------------.   .----------------.  .----------------.
 | IDictionary    |  | IList          |   | IDictionary﹤T﹥ |  | IList﹤T﹥       |
 .----------------.  .----------------.   .----------------.  .----------------.
````````````

An dieser Stelle greift das Interface `ICollection` und definiert die Methoden `Add`, `Clear`, `Contains`, `CopyTo` und `Remove`. Mit `Contains` kann geprüft werden, ob ein bestimmter Wert im Container enthalten ist. `CopyTo` extrahiert die Werte des Containers in ein Array. Dabei können bestimmte Ranges definiert werden. Die anderen Methoden sind selbsterklärend.

| Schnittstelle | Spezifizierte Funktionen        |
| ------------- | ------------------------------- |
| IEnumerable   | GetEnumerator()                 |
| ICollection   | Count(), Add(), Remove()        |
| IList         | IndexOf(), Insert(), RemoveAt() |
| IDictionary   | Keys(), Values(), TryGetValue() |

Folgendes Klassendiagramm zeigt die Teile der in C# implementierten Collection-Types
und deren Relationen zu den entsprechenden Interfaces.

![Collections](https://www.plantuml.com/plantuml/png/VP5FIyD04CNlyoaMl8afVe0GAlw1OZr8nVjsCzeXcrsPp1uKFxma4tN9jhqDx_VcCRnP3s9PKkzXw2XyMBQzSTuEmuq8qpu9RbmCE_f2Smq7Qj4uOkTHvoUKGsnrVY3qBS2qR3Rt8VN8LYAR-gKnTKr1aD-imwOn2zFUOsdwzTn6xz49nN3QiwL19deStz6qR_dJr8zNvdMPCll-KYxU6J7CwdF2XAMy4-kwKjvIwB0zdbIUiOYCBBfxZeyfImvvavTLazSFUODLzQrmDaFMZSBC3Tfh8KEsirgDy5-0xCWJR0mjMQQE8sYHIrMVeK9saJwZaDSOsjJx7m00)<!-- size="350px" -->


                                     {{1-2}}
********************************************************************************

Im Folgenden sollen Beispiele für die aufgeführten Datenstrukturen dargestellt werden.

| C# Collection | Bezeichnung                                   | Bedeutung                                                                                                        |                                                                                                              |
| ------------- | --------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| List          | unsortiertes Datenfeld indizierbarer Elemente | Im Unterschied zum Array "beliebig" erweiterbar                                                                  | [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1?view=netframework-4.8)  |
| SortedList    | sortiertes Datenfeld                          | Abbildung der Reihenfolge über einen numerischen Schlüssel                                                       |    [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.sortedlist-2?view=netframework-4.8)                                                                                                          |
| Stack         | LIFO Datenstruktur                            |                                                                                                                  | [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.stack-1?view=netframework-4.8) |
| Queue         | FIFO Datenstruktur                            |                                                                                                                  | [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.queue-1?view=netframework-4.8) |
| Dictionary     | assoziatives Datenfeld                        | ... Datenstruktur mit nicht-numerischen (fortlaufenden ) Schlüsseln, um die enthaltenen Elemente zu adressieren. |  [Link](https://docs.microsoft.com/de-de/dotnet/api/system.collections.generic.dictionary-2?view=netframework-4.8)                                                                                                            |

********************************************************************************

## Anwendung der Generic Collections

**List<T>**

```csharp      ListExample
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Rextester
{
    public class Program{
      public static void Main(string[] args){
        // Initialisieren mit Basiswerten, Ergänzungen der Liste
        var animals = new List<string>() { "bird", "dog" };
        animals.Add("cat");
        animals.Add("lion");
        // Fügt mehrere Objekte in die Liste ein
        animals.InsertRange(1, new string[] { "frog", "snake" });
        foreach (string value in animals)
        {
            Console.WriteLine("RESULT: " + value);
        }
        Console.WriteLine("In der Liste finden sich " + animals.Count + " Elemente");
        Console.WriteLine("Für die Liste reservierter Speicher (Einträge) " + animals.Capacity);
        Console.WriteLine("lion findet sich an " + animals.IndexOf("lion") + " Stelle");
        animals.Remove("lion");
        Console.WriteLine("In der Liste finden sich nun " + animals.Count + " Elemente");
      }
    }
}
```
@Rextester.eval(@CSharp)

**Dictionary<T, U>**

```csharp      DictionaryExample
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Rextester
{
    public class Program{
      public static void Main(string[] args){
         Dictionary<string, int> Telefonbuch = new Dictionary<string, int>();
         Telefonbuch.Add("Peter", 1234);
         Telefonbuch.Add("Paula", 5234);

         foreach( string s in Telefonbuch.Keys   )
         {
            Console.Write("Key = {0}", s);
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
}
```
@Rextester.eval(@CSharp)

**HashSet<T>**

```csharp      DictionaryExample
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Rextester
{
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
}
```
@Rextester.eval(@CSharp)


## Aufgaben der Woche

- [ ] Erklären Sie, warum `Array` keine `Add`-Methode umfasst, obwohl es das Interface `IList` implementiert, dass wiederum diese einschließt. Tipp: Rufen Sie Ihr wissen um die explizite Methodenimplementierung noch mal auf.
- [ ] Die Erläuterung zu den Beschränkungen beim Einsatz von Generics im Dokumemt 19 basiert auf der nicht generischen Implementierung des Interfaces `IComparable`. Ersetzen Sie diese im Codebeispiel durch die  generische Variante.
- [ ] Evaluieren Sie verschiedene Container in Bezug auf Methoden zum Einfügen, Löschen, etc. Generieren Sie dazu entsprechende künstliche Objekte, die Sie manipulieren "Füge 100.000 int Werte in eine Liste ein.". Messen Sie die dafür benötigten Zeiten.
