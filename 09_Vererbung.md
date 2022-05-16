<!--

author:   Sebastian Zug, Galina Rudolf, André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.4
language: de
narrator: Deutsch Female

import: https://github.com/liascript/CodeRunner

icon: https://upload.wikimedia.org/wikipedia/commons/d/de/Logo_TU_Bergakademie_Freiberg.svg
-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/09_Vererbung.md)

# Vererbung

| Parameter                | Kursinformationen                                                                                                                                                                          |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                                                                                                            |
| **Semester**             | `Sommersemester 2022`                                                                                                                                                                      |
| **Hochschule:**          | `Technische Universität Freiberg`                                                                                                                                                          |
| **Inhalte:**             | `Implementierung der Vererbung in C#`                                                                                                                                |
| **Link auf den GitHub:** | [https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/09_Vererbung.md](https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/09_Vererbung.md) |
| **Autoren**              | @author                                                                                                                                                                                    |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Auf Nachfrage ...

> Hinweis auf die Lauffähigkeit der _Fat Arrow Syntax_ unter C# 7.0. siehe [Beispiel aus Vorlesung 7](https://liascript.github.io/course/?https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/07_OOPGrundlagenI.md#5)


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

![Vererbungsbeispiel](./img/08_OOP_Csharp/Vererbungsbeispiel.png "Beispiel einer Vererbungshierarchie in UML Notation [^WikiInheri] ")

*****************************************************************************


                                      {{1-2}}
*****************************************************************************

**Umsetzung in C#**

```csharp    Vererbung
using System;
using System.Reflection;
using System.ComponentModel.Design;

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
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

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
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

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

Die Konzepte von `internal` setzen diese Überlegung fort und kontrollieren den Zugriff über Assembly-Grenzen.

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
                "^"                   :  |                                  \   +-------------------------+
                 |                    :  |                                  /   | Assembly - Programm     |
                 |                    :  |                                 '    +-------------------------+
  +------------------------------+    :  |                                 |
  | Programm                     |    :  |                                 |
  +------------------------------+    :  |                                 |
  | ✛ Maier: Fußballspieler      |    :  |                                 |
  +------------------------------+    :  |                                 |
  | ✛ Main()                     |    :  |                                 |
  +------------------------------+    :  |                                 |
                                      : -'                                -'
```

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
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)


*****************************************************************************

### Klasse

Auch für Klassen selbst können Zugriffsattribute das Verhalten bestimmen:

+ Jede Klasse kann entweder als `public` oder `internal` deklariert sein (Standard: `internal`)
+ Klassen können mit `sealed` versiegelt werden. Damit ist das Erben davon ausgeschlossen (Bsp.: System.String)

## Polymorphie in C#

                                   {{0-2}}
******************************************************************************

Strukturieren Sie die Klassen "Zug", "GüterZug", "PersonenZug" und "ICE" in einer
sinnvolle Vererbungshierarchie. Wie setzen Sie diese in C# Code um?

******************************************************************************

                                  {{1-2}}
******************************************************************************

```csharp    Constructors
using System;
using System.Reflection;
using System.ComponentModel.Design;

class Zug
{
  string nummer;
  public Zug()
  {
    Console.WriteLine("Zug-ctor");
  }
  public Zug(string nummer)
  {
    this.nummer = nummer;
    Console.WriteLine("Spezifischer Zug-ctor");
  }
}

class PersonenZug : Zug
{
  public PersonenZug() : base("Freiberg")
  {
    Console.WriteLine("PersonenZug-ctor");
  }
}

class Ice : PersonenZug
{
  public Ice()
  {
    Console.WriteLine("ICE-ctor");
  }
}

class GueterZug : Zug
{
  public GueterZug()
  {
    Console.WriteLine("GueterZug-ctor");
  }
}


public class Program
{
  public static void Main(string[] args)
  {
    Console.WriteLine("Generiere neuen ICE ");
    Ice ice = new Ice();
    Console.WriteLine("Generieren neuen Güterzug");
    GueterZug gueter = new GueterZug();
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

> **Merke:** Konstruktoren werden nicht geerbt! Jede Unterklasse deklariert (implizit) eigene Konstrukturen.

Die Konstruktoren der Basisklasse können jeweils mit `base()` aufgerufen werden. Erfolgt dies nicht, wird der parameterlose Konstruktor der Basisklasse automatisch aufgerufen.

Die Ausgabe des oben aufgeführten Beispiels illustriert diese Aufrufhierachie. Entfernen Sie dem `base` Aufruf in Zeile 23 und erklären Sie den Unterschied.


******************************************************************************

                                   {{2-3}}
******************************************************************************

In diesem Fall ist `Zug` die Basisklasse und `PersonenZug`, `GueterZug` und `ICE` sind
abgeleitete Klassen.

Eine Variablen vom Basisdatentyp kann immer eine Instanz einer abgeleiteten
Klasse zugewiesen werden. Entsprechend unterscheidet man dann zwischen dem
statischen und dem dynamischen Typ der Variablen. Der statische Typ ist
immer der, der auch deklariert wurde. Der dynamische Typ wird durch die aktuelle
Referenz einer Instanz einer abgeleiteten Klasse von Zug bestimmt und ist
veränderlich.

| Zuweisung                  | statischer Typ von Zug | dynamischer Typ von Zug |
| -------------------------- | ---------------------- | ----------------------- |
| `Zug RB51 = new Zug()`     | Zug                    | Zug                     |
| `RB51 = new PersonenZug()` | Zug                    | PersonenZug             |
| `RB51 = new Ice`           | Zug                    | ICE                     |

******************************************************************************

### Laufzeitprüfung

Entsprechend brauchen wir eine Typprüfung, die untersucht, ob die Variable von
einem bestimmten dynamischen Typ  oder einem daraus abgeleiteten Typ ist.


+ der dynamische Typ einer Klasse kann zur Laufzeit geprüft werden
+ Typtest liefert bei null-Werten immer `false`

```csharp    Typprüfung
using System;
using System.Reflection;
using System.ComponentModel.Design;

class Zug
{
  public Zug()
  {
    Console.WriteLine("Zug-ctor");
  }
}

class PersonenZug : Zug
{
  public PersonenZug() : base()
  {
    Console.WriteLine("PersonenZug-ctor");
  }
}

class Ice : PersonenZug
{
  public Ice()
  {
    Console.WriteLine("ICE-ctor");
  }
}

public class Program
{
  public static void Main(string[] args)
  {
    Zug IC239 = new Ice();
    Console.WriteLine("IC239 ist ein Zug? " + (IC239 is Zug)); // true
    Console.WriteLine("IC239 ist ein PersonenZug? " + (IC239 is PersonenZug)); // true
    Console.WriteLine("IC239 ist ein Ice? " + (IC239 is Ice)); // true
    IC239 = null;
    Console.WriteLine("IC239 ist ein Ice? " + (IC239 is Ice)); // false
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)


### Grundidee der Polymorphie

                             {{0-1}}
*******************************************************************************

Objekte einer Basisklasse können somit Instanzen einer abgeleiteten Klassen
umfassen. Damit lassen sich ähnlich einem Container sehr unterschiedliche
Objekte einer Vererbungslinie bündeln. Welche Frage ergibt sich dann?

**Wir haben schon gesehen, dass die Vererbung unter anderem Funktionen umfasst. Auf welche Klassenmember greife ich überhaupt zurück?**

*******************************************************************************

                            {{1-2}}
*******************************************************************************

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

Dynamische Bindung erlaubt den Aufruf von überschriebenen Methoden aus der
Basisklasse heraus, wobei das Überschreiben muss in der Basisklasse explizit
erlaubt werden muss.

*******************************************************************************

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

```csharp    Polymorphy.cs
using System;

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
    Animal[] animals = new Animal[3]; // <- Statischer Typ  Animal
    animals[0] = new Duck("Alfred");  // <- Dynamischer Typ Duck
    animals[1] = new Cow("Hilde");
    animals[2] = new Animal("Bernd");
    foreach (Animal anim in animals)
      anim.makeSound();
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

Die verschiedenen Tierklassen werden auf ihre Basisklasse gecastet, trotzdem
aber die individuelle Implementierung von makeSound ausgeführt. Damit erlaubt die
Polymorphie ein gleichartiges Handling unterschiedlicher Klassen, die über die
Vererbung miteinander verknüpft sind.

Interessant ist die Möglichkeit die ursprüngliche Implementierung der Methode
aus der Basisklasse weiterhin zu nutzen und zu erweitern:

```charp
class Horse : Animal
{
  public Horse(string name) : base(name) { }
  public override void makeSound()
  {
    base.makeSound();
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
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

Verdeckt werden können alle Klassenmember einer Basisklasse:

+ Felder
+ Properties und Indexer
+ Methoden usw.

Wenn kein Schlüsselwort angegeben ist, wird implizit `new` angenommen. Im oben
genannten Beispiel folgt daraus, dass die in `Cat` implementierte Ausgabe ausschließlich
von Objekten des statischen Typs `Cat` aufgerufen werden kann. Testen Sie die
Wirkung und ersetzen Sie `new` durch `override`.

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

Nun entwickelt der Hersteller eine neue Version von GraphicsClass und
integriert eine eigene Realisierung von `DrawRectangle`. Sobald Sie Ihre
Anwendung neu gegen die Bibliothek kompilieren, erhalten Sie vom Compiler eine
Warnung. Diese Warnung informiert Sie darüber, dass Sie das gewünschte Verhalten
der DrawRectangle-Methode in Ihrer Anwendung bestimmen müssen.  Welche
Möglichkeiten haben Sie - override oder new oder umbenennen? Welche Konsequenzen
ergeben sich daraus?

## Versiegeln von Klassen oder Membern

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

> **Merke:** Da Strukturen implizit versiegelt sind, können sie nicht geerbt werden.

```csharp    sealedMethods
using System;

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
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

## Casts über Klassen

Konvertierungen zwischen unterschiedlichen Datentypen lassen sich auch
auf Klassen anwenden, allerdings sind hier einige Besonderheiten zu beachten.

+ implizit auf die Basisklasse  (upcast)
+ explizit auf die abgeleitete Klasse (downcast)

gecastet werden. Zunächst ein Beispiel für einen *upcast* anhand unseres
Fußballbeispiels. Zugriffe auf Member, die  in der Basisklasse nicht enthalten
sind führen logischerweise zum Fehler.

```csharp    Upcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

public class Person {
  public int geburtsjahr;
  public string name;
}

public class Fußballspieler : Person {
  public byte rückennummer;
}

public class Program
{
  public static void Main(string[] args)
  {
    Fußballspieler champ = new Fußballspieler {geburtsjahr = 1956,
                                               name = "Maier",
                                               rückennummer = 13};
    Console.WriteLine("Felder in der Instanz '{0}' von '{1}'", champ.name, champ);
    var fields = champ.GetType().GetFields();
    foreach (FieldInfo field in fields){
      Console.WriteLine(" x " + field.Name);
    }
    Person human = champ;     // Castoperation Fußballspieler -> Person
    Console.WriteLine("human ist ein Fußballspieler? " + (human is Fußballspieler));
    //Console.WriteLine(human.rückennummer);
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

In umgekehrter Richtung vollzieht sich der *Downcast*, eine Instanz der
Basisklasse wird auf einen abgeleiteten Typ gemappt.

```csharp    Downcast
using System;
using System.Reflection;
using System.ComponentModel.Design;

public class Person {
  public int geburtsjahr;
  public string name;
}

public class Fußballspieler : Person {
  public byte rückennummer;
}

public class Program
{
  public static void Main(string[] args)
  {
    Fußballspieler champ = new Fußballspieler {geburtsjahr = 1956,
                                               name = "Maier",
                                               rückennummer = 13};
    Person mensch = champ;
    Fußballspieler champ2 = (Fußballspieler) mensch;
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)

### Beispiel

*Upcast* und *Downcast* ...  wozu brauche ich das den? Nehmen wir an, dass wir
eine Ausgabemethode für beide Typen - Person und Fußballspieler - benötigen.
Ja, es wäre möglich diese als Memberfunktion zu implementieren, problematisch
wäre aber dann, dass wir an unterschiedlichen Stellen im Code spezifische
Befehle für die Ausgabe in der Konsole zu stehen haben. Sollen die Log-Daten
nun plötzlich in eine Datei ausgegeben werden, müsste diese Anpassung überall
vollzogen werden. Entsprechend ist eine externe (statische) Logger-Klasse
wesentlich geeigneter diese Funktionalität zu kapseln. Allerdings wäre dann ein
überladen der entsprechenden Ausgabefunktion mit allen vorkommenden Typen notwendig.
Dies kann durch entsprechende Casts umgangen werden.

```csharp    UpCastExample
using System;
using System.Reflection;
using System.ComponentModel.Design;

public class Person
{
  public int geburtsjahr;
  public string name;
}

public class Fußballspieler : Person
{
  public byte rückennummer;
}

public static class Logger
{
  public static void printPerson(Person person){
      Console.WriteLine("{0} - {1}", person.name, person.geburtsjahr);
      if (person is Fußballspieler)
        Console.WriteLine("{0} - {1}", person.name, (person as Fußballspieler).rückennummer);
  }
}

public class Program
{
  public static void Main(string[] args)
  {
     Person Mensch = new Person {geburtsjahr = 1956,
                                name = "Maier"};
     Logger.printPerson(Mensch);
     Fußballspieler Champ = new Fußballspieler{geburtsjahr = 1967,
                                               name = "Müller",
                                               rückennummer = 13};
     Logger.printPerson(Champ);
  }
}
```
@LIA.eval(`["main.cs"]`, `mono main.cs`, `mono main.exe`)
