<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md

-->

# Softwareentwicklung - 10 - OOP Implementierung in Csharp

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/10_OOP_Csharp.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/10_OOP_Csharp.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung10](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/10_OOP_Csharp.md#1)

---------------------------------------------------------------------

## 7 Fragen in 7 Minuten

**1. Jetzt sind Sie dran ...**

**2. Jetzt sind Sie dran ...**

**3. Jetzt sind Sie dran ...**

**4. Jetzt sind Sie dran ...**

**5. Jetzt sind Sie dran ...**

**6. Jetzt sind Sie dran ...**

**7. Jetzt sind Sie dran ...**

## Vererbung in C#

                                      {{0-1}}
*****************************************************************************

> Vererbung bildet neben Kapselung und Polymorphie die zentrale Säule des
> objektorientierten Programmierens. Die Vererbung ermöglicht die Erstellung
> neuer Klassen, die ein in exisitierenden Klassen definiertes Verhalten
> wieder verwenden, erweitern und ändern. [MS.NET Programmierhandbuch]

**Beispiele**

Die Klasse, deren Member vererbt werden, wird **Basisklasse** genannt, die erbende
Klasse als **abgeleitete Klasse** bezeichnet.

| Basisklasse | abgeleitete Klassen                 | Gemeinsamkeiten                                                  |
| ----------- | ----------------------------------- | ---------------------------------------------------------------- |
| Fahrzeug    | Flugzeug, Boot, Automobil           | Position, Geschwindigkeit, Zulassungsnummer, Führerscheinpflicht |
| Datei       | Foto, Textdokument, Datenbankauszug | Dateiname, Dateigröße, Speicherort                               |
| Nachricht   | Email, SMS, Chatmessage             | Adressat, Inhalt, Datum der Versendung                           |


![Vererbungsbeispiel](./img/11_OOP_CsharpII/Vererbungsbeispiel.png)<!-- width="70%" --> [WikiInheri](#7)

*****************************************************************************


                                      {{1-2}}
*****************************************************************************

**Umsetzung in C#**

```csharp    Vererbung
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;
  }

  public class Fußballspieler : Person {
    public byte rückennumemr;
  }

  public class Schiedsrichter : Person {
    public bool assistent = true;
  }

  public class Program
  {
    public static void Main(string[] args){
      Person Mensch = new Person {geburtsjahr = 1956, name = "Löw"};
      Console.WriteLine("{0,4} - {1}", Mensch.geburtsjahr, Mensch.name );

      Console.WriteLine("Felder in der Instanz '{0}' von '{1}'", Mensch.name, Mensch);
      var fields = Mensch.GetType().GetFields();
      foreach (FieldInfo field in fields){
         Console.WriteLine(" x " + field.Name);
      }
    }
  }
}
```
@Rextester.eval(@CSharp)

> *Merke*: Im Unterschied zu Klassen ist für Structs unter C# keine Vererbung möglich!

In C# kann jede Klassendefinition nur eine Basisklasse referenzieren. Im Sinne
einer realitätsnahen Modellierung wären Mehrfachvererbungen aber
durchaus zielführend. Ein Amphibienfahrzeug leitet sich aus den Basisklassen
Wasserfahrzeug und Landfahrzeug ab, ein Touchpad integriert die Member von
Eingabegerät und Ausgabegerät. C# verzichtet  drauf um Mehrdeutigkeiten und Fehler
ausschließen zu können, die aus gleichnamige Membern hervorgehen.

*****************************************************************************
                                  {{2-3}}
*****************************************************************************

** ... und wie erfolgt die Initialisierung?**

Konstruktoren werden nicht vererbt, jedoch

+ kann mit dem Schlüsselwort `base` auf die Konstruktoren der Basisklasse zurückgegriffen werden.
+ wird sofern aus der abgeleiteten Klasse kein expliziter Aufruf erfolgt, der Standardkonstruktor der Basisklasse aufgerufen.

Ein Beispiel für den impliziten Aufruf des Standardkonstruktors:

<!-- --{{0}}-- Idee des Codefragments:
  * Fügen Sie einen leeren Standardkonstruktor mit einer Ausgabe in Fußballspieler ein
    public Fußballspieler(){
       Console.WriteLine("ctor of Fußballspieler");
     }
  * nutzen Sie nun base um den zweiten in Person exisitierenden Konstruktor zu
    adressieren.
       public Fußballspieler() : base(1)
-->
```csharp    ImplicitConstructorCall
using System;
using System.Reflection;
using System.ComponentModel.Design;

namespace Rextester
{
  public class Person {
    public int geburtsjahr;
    public string name;

    public Person(){
      geburtsjahr = 1984;
      name = "Orwell";
      Console.WriteLine("ctor of Person");
    }

    public Person(int auswahl){
      if (auswahl == 1) {name = "Micky Maus";}
      else {name = "Donald Duck";}
    }
  }

  public class Fußballspieler : Person {
    public byte rückennummer;
  }

  public class Program
  {
    public static void Main(string[] args){
      Fußballspieler champ = new Fußballspieler();
      Console.WriteLine("{0,4} - {1}", champ.geburtsjahr, champ.name );

    }
  }
}
```
@Rextester.eval(@CSharp)

*****************************************************************************

## Zugriffsmechanismen

Wer darf auf welche Methoden, Properties, Variablen usw. zurückgreifen? Mit der Einführung
der Vererbung steigt die Komplexität der Sichtbarkeitsregeln nochmals an.

<!--
style="width: 100%; max-width: 720px; display: block; margin-left: auto; margin-right: auto;"
-->
````ascii
| Zugriffsmodifizierer | Innerhalb eines Assemblys       || Außerhalb eines Assemblys      |
|                      | Vererbung      | Instanzierung  || Vererbung     | Instanzierung  |
| -------------------- | -------------- | -------------- || ------------- | -------------- |
| `public`             | ja             | ja             || ja            | ja             |
| `private`            | nein           | nein           || nein          | nein           |
| `protected`          | ja             | nein           || ja            | nein           |
| `internal`           | ja             | ja             || nein          | nein           |
| `ìnternal protected` | ja             | ja             || ja            | nein           |
````

`protected` definiert eine differenzierten Zugriff für geerbte und Instanz-Methoden. Während
bei geerbten Elementen uneingeschränkt zugegriffen werden kann, bleiben diese bei der
bloßenn Anwendung geschützt.

Die Konzepte von `internal` setzen diese Überlegung fort und kontrollieren den Zugriff über Assemblygrenzen.

### Member der Klasse

<!--
style="width: 100%; max-width: 720px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
                                      :  Variante I                       Variante II
                                      :  Übergreifendes gemeinsames       Separate Assemblies via
                                      :  Assembly                         dll-Referenz
                                      :
  +------------------------------+    : -.
  | Person                       |    :  |
  +------------------------------+    :  |
  | ✛ Geburtsjahr : int          |    :  |                                     +-------------------------+
  | ✛ Name : string              | ---:--|-------------------------------------| Person.dll              |
  | - email : string             |    :  |                                     +-------------------------+
  +------------------------------+    :  |                                                  |
  | ✛ BerechneAlter()            |    :  .    +------------------------+                    |
  | # SendEmail()                |    :   \   |  Assembly - Programm   |                    |
  +------------------------------+    :   /   +------------------------+                    |
                 ∆                    :  '                                                  |
                 |                    :  |                                                  |
                 |                    :  |                                                  |
  +------------------------------+    :  |                                -.                |
  | Fußballspieler               |    :  |                                 |                |
  +------------------------------+    :  |                                 |                |
  | - rückennummer: int          |    :  |                                 |                |
  | # geschosseneTore : int      |    :  |                                 |                |
  +------------------------------+    :  |                                 |                |
  | «property» Rückennummer: int |    :  |                                 |                |
  | - SendMessage()              |    :  |                                 |                |
  +------------------------------+    :  |                                 .                |
                 ^                    :  |                                  \   +-------------------------+
                 ┊                    :  |                                  /   | Assembly - Programm     |
                 ┊                    :  |                                 '    +-------------------------+
  +------------------------------+    :  |                                 |
  | Programm                     |    :  |                                 |
  +------------------------------+    :  |                                 |
  | ✛ Maier: Fußballspieler      |    :  |                                 |
  +------------------------------+    :  |                                 |
  | ✛ Main()                     |    :  |                                 |
  +------------------------------+    :  |                                 |
                                      : -'                                -'

```


```csharp    Accesscontrol
using System;

namespace Rextester
{
  public class Person
  {
    public int Geburtsjahr = 1972;
    public string Name = "Lukas Podolski";
    string email = "LukasPodolski@gmx.de";

    public int BerechneAlter(){
       return DateTime.Now.Year - this.Geburtsjahr;
    }

    protected void SendEmail(string text){
       Console.WriteLine("MailTo - {0} - {1}", email, text);
    }
  }

  public class Fußballspieler : Person
  {
    private int rückennummer;
    protected int GeschosseneTore = 0;

    public int Rückennummer{
      set {if (value < 100) rückennummer = value;
           else Console.WriteLine("Fehler, Rückennummer ungültig");}
      get {return rückennummer;}
    }

    internal void SendMessage(){
      if (this.GeschosseneTore == 0) {this.SendEmail("Wohl nicht Dein Tag?");}
      else {this.SendEmail("Super gemacht!");}
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Fußballspieler Stürmer = new Fußballspieler();
      Stürmer.Geburtsjahr = 1982;
      //Stürmer.GeschosseneTore = 12;    // Compilerfehler
      Stürmer.SendMessage();
    }
  }
}
```
@Rextester.eval(@CSharp)


                              {{2-3}}
*****************************************************************************

Kriterien der Zugriffsattribute:

+ innerhalb/außerhalb einer Klasse
+ innerhalb der Vererbungshierachie einer Klasse / außerhalb ("nutzt")
+ innerhalb des Assemblys / außerhalb

Für Methoden, Membervariablen etc. ist das klar, aber macht es Sinn geschützte
private Konstruktoren zu definieren?

Private Konstruktoren werden verwendet, um die Instanziierung einer Klasse zu
verhindern, die ausschließlich statische Elemente hat. Ein Beispiel dafür ist
die `Math` Klasse, die Methoden definiert, die ohne eine Instanz der Klasse
aufgerufen werden. Wenn alle Methoden in der Klasse statisch sind, wäre es ggf.
sinnvoll die gesamte Klasse statisch anzulegen.

```csharp    privateConstructors
using System;

namespace Rextester
{
  public class Counter
  {
      private Counter() { }
      public static int currentCount;
      public static int IncrementCount()
      {
          return ++currentCount;
      }
  }

  public class Program
  {
    public static void Main(string[] args){
      Counter myCounter = new Counter();
      //Console.WriteLine()
    }
  }
}
```
@Rextester.eval(@CSharp)


*****************************************************************************

### Klasse

Auch für Klassen selbst können Zugriffsattribute das Verhalten bestimmen:

+ Jede Klasse kann entweder als `public` oder `internal` deklariert sein (Standard: `internal`)
+ Klassen können mit `sealed` versiegelt werden. Damit ist das Erben davon ausgeschlossen (Bsp.: System.String)


## Polymorphie in C#

> **Merke** Polymorphie bezeichnet die Tatsache, dass Klassenmember ausgehend
> von Ihrer Nutzung ein unterschiedliches Verhalten erzeugen.

Das heißt, die Methoden der Klassen einer Vererbungshierarchie können auf
verschiedenen Ebenen gleiche Signatur, aber unterschiedliche Implementierungen
haben. Welche der Methoden für ein gegebenes Objekt aufgerufen wird, wird erst
zur Laufzeit bestimmt (dynamische Bindung).

> **Merke** Dynamische Bindung bezeichnet die Tatsache, dass bei Aufruf einer
> überschriebenen Methode über eine Basisklassenreferenz oder ein
> Interface trotzdem die Implementierung der abgeleiteten Klasse zum
> Einsatz kommt.

Abgeleitete Klassen können aus der Basisklasse geerbte Methoden neu deklarieren.
Dabei kann gewählt werden, ob die Methode verdeckt oder überschrieben werden soll.
In beiden Fällen wird die ursprüngliche Methode durch eine neue ersetzt.

Dynamische Bindung erlaubt den Aufruf von überschriebenen Methoden aus der
Basisklasse heraus, wobei das Überschreiben muss in der Basisklasse explizit
erlaubt werden muss.

Siehe Beispielcode Polymorphie

### Überschreiben von Methoden

In C# können abgeleitete Klassen Methoden mit dem gleichen Namen wie
Basisklassen-Methoden enthalten. Diese Methoden müssen dann in der Basisklasse
mittels `virtual` als explizit überschreibbar deklariert werden:

```csharp
public virtual void makeSound() => Console.WriteLine("I'm an Animal");
```

Zum Überschreiben wird das Schlüsselwort `override` genutzt, welches ein
erneutes Deklarieren ermöglicht:

```csharp
public override void makeSound() => Console.WriteLine("Quack!");
```

Dabei müssen beide Methoden die gleiche Signatur haben, d.h. sie sollen die den gleichen Namen und eine identische Parameterliste besitzen. Ansonsten ist es nur Überladung!

```csharp    privateConstructors
using System;

namespace Rextester
{
  class Animal
  {
    public string Name;
    public Animal(string name){
      Name = name;
    }
    public virtual void makeSound(){
      Console.WriteLine("I'm an Animal");
    }
  }

  class Duck : Animal
  {
    public Duck(string name) : base(name) { }
    public override void makeSound(){
      Console.WriteLine("{0} - Quack ({1})", Name, this.GetType().Name);
    }
  }
  class Cow : Animal
  {
    public Cow(string name) : base(name) { }
    public override void makeSound(){
      Console.WriteLine("{0} - Muh ({1})", Name, this.GetType().Name);
    }
  }
  public class Program
  {
    public static void Main(string[] args){
      Animal[] animals = new Animal[3];
      animals[0] = new Duck("Alfred");
      animals[1] = new Cow("Hilde");
      animals[2] = new Animal("Bernd");
      foreach (Animal anim in animals)
        anim.makeSound();
    }
  }
}
```
@Rextester.eval(@CSharp)

Die verschiedenen Tierklassen werden auf ihre Basisklasse gecastet, trotzdem
aber die individuelle Implementierung von Sound ausgeführt. Damit erlaubt die
Polymorphie ein gleichartiges Handling unterschiedlicher Klassen, die über die
Vererbung miteinander verknüpft sind.

Interessant ist die Möglichkeit die ursprüngliche Implementierung der Methode
aus der Basisklasse weiterhin zu nutzt und zu erweitern:

```charp
class Horse : Animal
{
  public Horse(string name) : base(name) { }
  public override void makeSound()
  {
    base.Speak();
    Console.WriteLine("Ich ziehe Kutschen");
  }
}
```

Dazu kann die Methode aus der Basisklasse über `base.<Methodenname>` aufgerufen
werden


### Verdecken von Methoden

Sollen die spezifischen Methoden aber nur im Kontext der Klasse realisierbar
sein, so werden sie vor der Basisklasse "verdeckt". Dazu ist das Schlüsselwort
`new` erforderlich. In diesem Fall wird keine dynamische Bindung realisiert,
sondern die Methode der Basisklasse aufgerufen.

```csharp    newOperator
using System;

namespace Rextester
{
  class Animal
  {
    public string Name;
    public Animal(string name){
      Name = name;
    }
    public virtual void makeSound(){
      Console.WriteLine("I'm an Animal");
    }
  }

  class Cat : Animal
  {
    public Cat(string name) : base(name) { }
    public new void makeSound(){
      Console.WriteLine("{0} - Miau ({1})", Name, this.GetType().Name);
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Cat myCat = new Cat("Kity");
      myCat.makeSound();
      Animal myCatAsAnimal = new Cat("KatziTatzi");
      myCatAsAnimal.makeSound();
    }
  }
}
```
@Rextester.eval(@CSharp)

Verdeckt werden können alle Klassenmember einer Basisklasse:

+ Felder
+ Properties und Indexer
+ Methoden usw.

Wenn kein Schlüsselwort angegeben ist, wird implizit `new` angenommen. Allerdings
ist das explizite Verdecken hat nur äußerst selten eine sinnvolle Anwendung.

Das folgende Beispiel entstammt dem C# Programmierhandbuch und kann unter
[Link](https://docs.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/versioning-with-the-override-and-new-keywords) nachgelesen werden.

Nehmen wir an, dass Ihre Software eine Grafikbibliothek nutzt, die folgende
Funktionen bietet:

```csharp
class GraphicsClass
{
    public virtual void DrawLine() { }
    public virtual void DrawPoint() { }
}
```

Sie haben darauf aufbauend eine umfangreiches Framework geschieben und in einer
Klasse, die von GraphicsClass erbt eine Methode `DrawRectangle` implementiert.

```csharp
class YourDerivedGraphicsClass : GraphicsClass
{
    public void DrawRectangle() { }
}
```

Nun eintwickelt der Hersteller eine neue Version von GraphicsClass und
integriert eine eigene Realisierung von `DrawRectangle`. Sobald Sie Ihre
Anwendung neu gegen die Bibliothek kompilieren, erhalten Sie vom Compiler eine
Warnung. Diese Warnung informiert Sie darüber, dass Sie das gewünschte Verhalten
der DrawRectangle-Methode in Ihrer Anwendung bestimmen müssen.  Welche
Möglichkeiten haben Sie - override oder new oder umbenennen? Welche Konsequenzen
ergeben sich daraus?

### Versiegeln von Klassen oder Membern

Die Mechanismen der Vererbung und Polymorphie können aber auch aufgehoben werden,
wenn ein Schutz notwendig ist. Das Schlüsselwort `sealed` ermöglicht es sowohl
Klassen von der Rolle als Basisklasse auszuschließen als auch das Überschreiben
von Methoden zu verhindern.

```csharp
class A {}
sealed class B : A {}
```

Im Beispiel erbt die Klasse B von der Klasse A, allerdings kann keine Klasse von
der Klasse B erben.

Da Strukturen implizit versiegelt sind, können sie nicht geerbt werden.

```csharp    sealedMethods
using System;

namespace Rextester
{
  sealed public class Animal
  {
    public string Name;
    public Animal(string name){
      Name = name;
    }
    public virtual void makeSound(){
      Console.WriteLine("I'm a Crocodile");
    }
  }

  class Cat : Animal
  {
    public Cat(string name) : base(name) { }
    public sealed override void makeSound(){   // sealed schützt die Cat.makeSound methode
      Console.WriteLine("{0} - Miau ({1})", Name, this.GetType().Name);
    }
  }

  class Tiger : Cat
  {
    public Tiger(string name) : base(name) { }
    public override void makeSound(){
      Console.WriteLine("{0} - Grrrr ({1})", Name, this.GetType().Name);
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Tiger evilTiger = new Tiger("Shir Khan");
      evilTiger.makeSound();
    }
  }
}
```
@Rextester.eval(@CSharp)

### Abstrakte Klassen / Abstrakte Methoden

Mit `virtual` werden einzelne Methoden spezifiziert, die durch die abgeleiteten
Klassen implmentiert werden. Die Basisklasse hält aber eine "default" Implementierung
bereit. Letztendich kann man diesen Gedanken konsequent weiter treiben und die
Methoden der Basisklasse auf ein reines Muster reduzieren, dass keine eigenen Implementierungen
umfasst.

Diese Aufgabe übernehmen abstrakte Klassen und abstrakte Methoden. Eine abstrakte
Klasse:

+ kann nicht instanziiert werden
+ kann abstrakte Methoden umfassen
+ ist oft als Startpunkt(e) einer Vererbungshierarchie gedacht sind.

Innerhalb der Klasse können abstrakte Methoden integriert werden, die

+ implizit als virtuelle Methode implementiert angelegt werden
+ entsprechend keinen Methodenkörper umfassen

Eine nicht abstrakte Klasse, die von einer abstrakten Klasse abgeleitet wurde,
muss Implementierungen aller geerbten abstrakten Methoden und Accessoren
enthalten.

```csharp    abstractClass
using System;

namespace Rextester
{
  public abstract class Animal
  {
    public string Name;
    public Animal(string name){
      Name = name;
    }
//    public virtual void makeSound(){
//      Console.WriteLine("I'm an Animal");
//    }

    public abstract void makeSound();
  }

  public class Corcodile : Animal{

    public Corcodile(string name) : base(name){
      Name = name;
    }

    public override void makeSound(){
      Console.WriteLine("I'm a Crocodile");
    }
  }

  public class Program
  {
    public static void Main(string[] args){
      Corcodile A = new Corcodile("Tuffy");
      A.makeSound();
    }
  }
}
```
@Rextester.eval(@CSharp)

Warum macht es keinen Sinn eine abstrakte Klasse als `sealed` zu deklarieren?

## Beispiel der Woche ...

Das Beispiel der Woche findet sich unter folgendem Link

https://github.com/liaScript/CsharpCourse/tree/master/code/08_OOP_CsharpII/AbstractClasses
