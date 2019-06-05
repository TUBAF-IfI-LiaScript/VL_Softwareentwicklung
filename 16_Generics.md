<!--

author:   Sebastian Zug & André Dietrich
email:    zug@ovgu.de   & andre.dietrich@ovgu.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/LiaTemplates/Rextester/master/README.md
import: https://raw.githubusercontent.com/LiaTemplates/WebDev/master/README.md
-->

# Vorlesung Softwareentwicklung - 16 - Generics

--------------------------------------------------------------------
Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

https://github.com/liaScript/CsharpCourse/blob/master/16_Generics.md

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 16](https://liascript.github.io/course/?https://raw.githubusercontent.com/liaScript/CsharpCourse/master/16_Generics.md#1)

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

*1. Welche Möglichkeiten eröffnen generische Datentypen?*

Generische Klassen und Methoden vereinen Wiederverwendbarkeit, Typsicherheit und
Effizienz.

*2. Wie werden generische Datentypen in C# umgesetzt?*

```
public class LinkedList<T>{
  public void Add(T value){...}
  public T this[int index]{...}
}

LinkedList <float> list1= new LinkedList<float>(3.14);
LinkedList <ExampleClass> list1= new LinkedList<ExampleClass>(myExampleClass);
```

*3. Welche Probleme können bei der Verwendung generischen Typen entstehen?*

*4. Welche Bedingungen sind bei der Vererbung von generischen Typen zu beachten?*

*5. Welche Aufgabe haben die Beschränkungen? Welche Prüfungen lassen sich damit beispielsweise umsetzen?*

*6. Wie werden generische Typen in UML-Klassendiagrammen dargestellt?*


---------------------------------------------------------------------
## 0. Realisierung von CI Techniken unter GitLab

https://gitlab.com/Sebastian_Zug/CsharpCIExample

Da mit der Version

https://gitlab.com/Sebastian_Zug/CsharpCIExample/commit/917ace1650739813049dcfef5cd8cdb20f0c3959

ein fehlerhafter Test implementiert wurde, scheiterte dessen automatische
Evaluation.

https://gitlab.com/Sebastian_Zug/CsharpCIExample/-/jobs/224741162


## 1. Motivation

Versuchen wir nach dem Exkurs zur UML Modellierung und den Werkzeugen, den roten Faden der C# Programmierung wieder aufzunehmen. Nehmen wir an, dass Sie ohne die entsprechenden .NET-Bibliotheken eine Liste für `int`-Werte implementieren sollen.

```csharp  
using System;

namespace Rextester
{
  public class Node{
      public Node next;
      public int value;
      public Node(Node next, int value){
          this.next = next;
          this.value = value;
      }
  }

  public class LinkedList{
      public Node head;
      public LinkedList(int initial) {
          head = new Node(null, initial);
      }

      public void Add(int value){
          Node current = head;
          while (current.next != null){
              current = current.next;
          }
          current.next = new Node(null, value);
      }

      public int this[int index]{
          get {
              Node current = head;
              int count = 0;
              while (current != null){
                  if (count == index){
                     return current.value;
                  }
                  current = current.next;
                  count++;
              }
              return 1;
          }
       }
    }

    public class Program{
        public static void Main(string[] args){
          LinkedList linkedList = new LinkedList(121);
          linkedList.Add(140);
          linkedList.Add(280);
          linkedList.Add(309);
          int i = 2;
          Console.WriteLine("Der Wert des {0:D}. Eintages lautet {1:D}.", i, linkedList[i]);
        }
    }
}
```
@Rextester.eval(@CSharp)

Was sind die Nachteile in diesem Konstruktion aus? Welche Lösungsansätze sehen Sie?

Die Lösung ist typabhängig, die Speicherung eines anderen Datentypen macht eine
Neuimplementierung notwendig. Damit entstünde aber ein überwiegend redundanter
Code, der eine konsistente Realisierung und Wartung erheblich erschwert.

Lösungsansatz könnte die Arbeit mit dem allgemeinen `object`-Datentyp sein. Mittels
Boxing und Unboxing würden die spezifischen Datentypen auf diesen abgebildet.

> (Wiederholung )Merke: Alle C# Datentypen sind von `Òbject` abgeleitet.

```csharp
int i = 123;
object o = i;  // The following line boxes i.

o = 123;
i = (int)o;    // unboxing
```

Nachteilig daran ist, dass

+ diese Operation Laufzeit kostet,
+ beim Auslesen der Daten eine externe (außerhalb unserer Liste liegende) Cast-Operation erforderlich macht. `float x = (float) linkedList[i]`,
+ die Klasse würde alle Datentypen akzeptieren. Unter Umständen ist das aber nicht gewünscht weil zum Beispiel mit Zahlenwerten arithmetische Operationen ausgeführt werden sollen. Eine Beschränkung ist aber nicht möglich.

## 2. Generische Typen

"Generics" sind seit der Version 2.0 Elemente der .Net-Sprachen und der Common Language Runtime (CLR). Sie definieren das Konzept der Typparameter, wodurch Klassen und Methoden keiner konkreten Zuordnung zu einem Datentyp unterworfen werden. Platzhalter übernehmen die generische Repräsentation des Typen, die Ersetzung erfolgt zur Laufzeit.

```csharp   GenericsUsage
// Generische Klassenspezifikation
public class LinkedList<T>{
  public void Add(T value){
    ...
  }
  public T this[int index]{
    ...
  }
}

// Instanzierung mit verschiedenen Datentypen
LinkedList<float> list1 = new LinkedList<float>(3.14);
LinkedList<ExampleClass> list2 = new LinkedList<ExampleClass>(myExampleClass);
LinkedList<ExampleStruct> list3 = new LinkedList<ExampleStruct>(myExampleStruct);
```

Die Vorteile des Konzepts sind offensichtlich:

+ Der Compiler kann eine gezielte Typprüfung durchführen.
+ Die Operationen sind effektiver, weil keine Typumwandlungen realisiert werden müssen.
+ Programme werden lesbarer.

Hinsichtlich der Namenswahl für die generischen Typen sind sie frei, sollten aber berücksichtigen, dass für den Leser ggf. unklar ist, wie welcher konkrete Datentype realisiert werden kann. Die Einbuchstabenvariante "T" sollte nur genutzt werden, wenn in Bezug auf einen Container die Bedeutung wirklich klar ist.

```csharp      StackExample
using System;

namespace Rextester
{
    public class Stack<T>{
      int position = 0;
      T[] data = new T[100];

      public void Push(T newObj){
        if (position < 100){
          data[position++] = newObj;
        }
        else{
          Console.WriteLine("Array size exceeded!");
        }
      }
      public T Pop(){
        return data[position--];
      }
      public override string ToString(){
        string output = "";
        for (int i=0; i<position; i++){
           output = output + " " + data[i].ToString();
        }
        return output;
      }
    }

    public class Program{
        public static void Main(string[] args){
          var myStack = new Stack<int>();
          myStack.Push(3);
          myStack.Push(12);
          //myStack.Push("Hallo!");
          Console.WriteLine(myStack);

        }
    }
}
```
@Rextester.eval(@CSharp)

Generische Klassen und Methoden vereinen Wiederverwendbarkeit, Typsicherheit und Effizienz so, wie es ihre nicht generischen Gegenstücke nicht können. Generics werden am häufigsten für Auflistungen und deren Methoden verwendet.

### Vererbung bei generischen Typen

In der UML werden generische Typen über eine sepeate Box in der oberen linken Ecke
der Klassendarstellung im Klassendiagramm realisiert.

![UseCaseOnlineShopII](http://www.plantuml.com/plantuml/png/BSqn2i9048NXVaunfNOUG2I2ZNLEC6uUEBYxaPaN2yMx6yNoUppuXwG5brObRzxl5jQqLCiyak6NXJYNkO_-XExawXEqU9GAaTzBHt1_Cjf1NwBgFH7SV8Vjoa2R7_ZpBGFwj9O-)<!-- width="30%" -->

Generischen Typen können wie andere Typen von einer Klasse erben und Interfaces
Implementieren. Die Basisklassen können dabei selbst wieder generische sein.

| Ableitung               | Die generische Klasse A erbt ...                 | Bemerkung      |
| ----------------------- | ------------------------------------------------ | -------------- |
| `class A<X>: B {}`      | ... vom konkreten Typ B.                         |                |
| `class A<X>: B<int> {}` | ... vom konkretisierten generischen Typ B        |                |
| `class A<X>: B<X> {}`   | ... vom generischen Typ mit gleichem Platzhalter |                |
| `class A: B<X> {...}`   |                                                  | nicht erlaubt! |

Beispiele

```csharp  
class BaseNode { }
class BaseNodeGeneric<T> { }

class NodeConcrete<T> : BaseNode { }  // concrete type
class NodeClosed<T> : BaseNodeGeneric<int> { }  //closed constructed type
class NodeOpen<T> : BaseNodeGeneric<T> { }  //open constructed type
```

Spannend wird die Typparameterisierung für generische Klassen, die von offenen konstruierten Typen erben. Hier müssen für sämtliche Basisklassen-Typparameter Typargumente bereitgestellt werden.

```
class BaseNodeMultiple<T, U> { }

//No error
class Node4<T> : BaseNodeMultiple<T, int> { }

//No error
class Node5<T, U> : BaseNodeMultiple<T, U> { }

//Generates an error
//class Node6<T> : BaseNodeMultiple<T, U> { }
```
Im letzten Fall kann ausgehend von der Spezifikation von `Node6<int> A = new Node6<int>();` der Compiler nicht auf die konkrete Realisierung von `U` schließen.

### Beschränkungen

Sobald Operationen über den generischen Daten umgesetzt werden sollen, ergeben sich
zusätzliche Herausforderungen. Welches Ergebnis gibt eine Vergleichsoperation zurück,
wenn Sie auf eine `String`-Instanz angewandt wird. Welche Bedeutung hat der `+`-Operator `+` für Instanzen unserer Animal/Cat/Dog Klassen?

```csharp          GenericCompare
using System;

namespace Rextester
{
    public class Program{
      static int Plus<Element>(Element x, Element y){
         return (x + y);
      }

      public static void Main(string[] args){
        int a = 1;
        int b = 2;
        Console.WriteLine(Plus<int>(a, b));   
        }
    }
}
```
@Rextester.eval(@CSharp)

Folglich ist es notwendig die Allgemeinheit der generischen Methoden oder Klassen zu
beschränken. Man definiert Beschänkungen oder *Constraints*, die die Breite der verwendbaren Datentypen einschränken. Die Typprüfung bezieht diese Informationen dann ein.

| Beschränkung                | Das Typargument muss ...                                |
| --------------------------- | ------------------------------------------------------- |
| `where T : struct`          | ... ein Werttyp sein.                                   |
| `where T : class`           | ... ein Verweistyp sein.                                |
| `where T : <Basisklasse>`   | ... die Basisklasse sein oder von ihr abgeleitete sein. |
| `where T : <Schnittstelle>` | ... die Schnittstelle sein oder diese  implementieren.  |


Das folgende Beispiel setzt die Möglichkeiten der Beschränkung konsequent um und lässt nur
`Employee` selbst oder abgeleitete Typen zu. Damit wird sichergestellt, dass die Methoden,
die in GenericList verwendet werden, im Parametertypen auch existieren.

```csharp          Constraints
public class Employee
{
    public Employee(string s, int i) => (Name, ID) = (s, i);
    public string Name { get; set; }
    public int ID { get; set; }
}

public class GenericList<T> where T : Employee {

    // hier werden Methoden oder Felder der Klasse Employee genutzt.

}
```


********************************************************************************

## Anhang

**Referenzen**



**Autoren**

Sebastian Zug, André Dietrich
