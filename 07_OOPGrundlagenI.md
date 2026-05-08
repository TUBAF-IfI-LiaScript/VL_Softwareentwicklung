<!--

author:   Sebastian Zug, Galina Rudolf, André Dietrich & `Florian2501`
email:    sebastian.zug@informatik.tu-freiberg.de
version:  2.0.0
language: de
narrator: Deutsch Female
comment:  Klassen in C# — Übergang von Python-OOP zu C#: Klasse als Hauptarbeitseinheit, this, Konstruktoren, Wert- vs. Referenztypen, struct als Werttyp-Spezialfall, Sichtbarkeitsmodifizierer im Überblick
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/07_OOPGrundlagenI.md)

# Klassen in C#

| Parameter                | Kursinformationen                                                                              |
| ------------------------ | ---------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                |
| **Teil:**                | `7/27`                                                                                         |
| **Semester**             | @config.semester                                                                               |
| **Hochschule:**          | @config.university                                                                             |
| **Inhalte:**             | @comment                                                                                       |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/07_OOPGrundlagenI.md |
| **Autoren**              | @author                                                                                        |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

## Brücke: von Python-OOP (07a) zu C#

In Vorlesung 07a haben wir die OOP-Konzepte anhand von Python eingeführt: Klasse als Bauplan, Objekt als Instanz, Konstruktor, Methoden, Vererbung, Kapselung. Diese Vorlesung übersetzt diese Konzepte nach **C#** — und zeigt, wo C# *strenger* oder *reicher* ist als Python.

| Konzept           | Python (07a)                          | C# (ab heute)                                       |
| ----------------- | ------------------------------------- | --------------------------------------------------- |
| Klassendefinition | `class Animal:`                       | `public class Animal { ... }`                       |
| Konstruktor       | `def __init__(self, name): ...`       | `public Animal(string name) { ... }`                |
| `self` / `this`   | `self.name`                           | `this.name`                                         |
| Objekt erzeugen   | `kitty = Animal("Kitty")`             | `Animal kitty = new Animal("Kitty");`               |
| Vererbung         | `class Dog(Animal):`                  | `public class Dog : Animal { ... }`                 |
| Eltern aufrufen   | `super().__init__(...)`               | `: base(...)` bzw. `base.MakeNoise()`               |
| Kapselungs-Hinweis | `_age` (Konvention)                  | `private int age;` (Compiler-erzwungen)             |

> **Lernziele dieser Vorlesung:** Sie können eine C#-Klasse mit Konstruktor und Methoden anlegen, das Verhalten von `this` benennen, **Wert- vs. Referenztypen** unterscheiden, einen `struct` als Werttyp-Spezialfall einsetzen und die Sichtbarkeitsmodifizierer von C# nennen.

> **Was kommt in 08 / 09?** — 08 vertieft die *Klassenelemente* (Properties, statische Mitglieder, Operator-Überladung). 09 behandelt **Vererbung in C#** mit ihrer expliziten `virtual`/`override`-Mechanik.

## Aus Python wird C#

Hier dieselbe `Animal`-Klasse wie in 07a — einmal Python, einmal C# nebeneinander:

```python      AnimalPython.py
class Animal:
    def __init__(self, name, sound):
        self.name = name
        self.sound = sound

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


kitty = Animal("Kitty", "Miau")
kitty.make_noise()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

```csharp      AnimalCSharp
using System;

public class Animal
{
    public string name;
    public string sound;

    public Animal(string name, string sound)
    {
        this.name = name;
        this.sound = sound;
    }

    public void MakeNoise()
    {
        Console.WriteLine($"{this.name} macht {this.sound}");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Animal kitty = new Animal("Kitty", "Miau");
        kitty.MakeNoise();
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Auffälligkeiten:**
>
> - **`this` ersetzt `self`** und ist *kein* Pflichtparameter — die Sprache kennt das aktuelle Objekt implizit.
> - **`new` ist Pflicht** beim Erzeugen eines Objekts — wir kommen darauf gleich zurück.
> - **Sichtbarkeitsmodifizierer (`public`)** sind explizit — Standard ist in C# privat.

### Was bedeutet `this`?

Wie `self` in Python ist `this` der Verweis auf das aktuelle Objekt — nur dass C# ihn nicht als Parameter übergibt, sondern automatisch bereitstellt.

```csharp      ThisExample
using System;

public class Animal
{
    public string name;
    public string sound;

    public Animal(string name, string sound)
    {
        this.name = name;       // 'name' (Feld) <- 'name' (Parameter)
        this.sound = sound;
    }

    public void Rename(string name)
    {
        this.name = name;       // 'this' löst die Verwechslung auf
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Animal kitty = new Animal("Kitty", "Miau");
        kitty.Rename("Tom");
        Console.WriteLine(kitty.name);
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Merke:** `this` wird in C# vor allem dann sichtbar geschrieben, wenn ein Methoden- oder Konstruktorparameter denselben Namen hat wie ein Feld. Sonst kann es weggelassen werden.

### `this` ist mehr als Tippersparnis

`this` löst nicht nur Namenskonflikte auf. Zwei weitere Verwendungsmuster lohnen einen Blick — wir werden sie später wiedertreffen.

**(a) Die eigene Instanz weiterreichen.** Ein Objekt kann sich *selbst* an ein anderes Objekt übergeben — etwa zur Anmeldung in einer Liste, an einen Beobachter, an einen Logger:

```csharp      ThisAsArgument
using System;
using System.Collections.Generic;

public class Farm
{
    private List<Animal> animals = new List<Animal>();

    public void Register(Animal a)
    {
        animals.Add(a);
        Console.WriteLine($"Farm hat {a.name} aufgenommen.");
    }
}

public class Animal
{
    public string name;

    public Animal(string name, Farm farm)
    {
        this.name = name;
        farm.Register(this);          // <- "ich, dieses Tier"
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Farm farm = new Farm();
        Animal kitty = new Animal("Kitty", farm);
        Animal wally = new Animal("Wally", farm);
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

**(b) Method Chaining (Fluent API).** Wenn eine Methode `this` zurückgibt, lassen sich Aufrufe *aneinanderhängen*. Genau dieses Muster nutzen Sie später bei LINQ und vielen Builder-APIs.

```csharp      FluentInterface
using System;

public class AnimalBuilder
{
    private string name = "?";
    private string sound = "?";
    private int age = 0;

    public AnimalBuilder WithName(string name)   { this.name = name;   return this; }
    public AnimalBuilder WithSound(string sound) { this.sound = sound; return this; }
    public AnimalBuilder WithAge(int age)        { this.age = age;     return this; }

    public void Print()
    {
        Console.WriteLine($"{name} ({age} Jahre) macht {sound}");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        new AnimalBuilder()
            .WithName("Kitty")
            .WithSound("Miau")
            .WithAge(5)
            .Print();
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Ausblick:** `this` taucht später noch in zwei weiteren Rollen auf — bei der Definition von **Indexern** (`public int this[int i] { ... }`, VL 08) und bei sogenannten **Extension Methods** (VL 26 / LINQ). Dieselbe Idee, immer „dieses konkrete Objekt".

## Konstruktoren in C#

Der Standardkonstruktor hilft aber nur bei der Vermeidung von uninitalisierten
Werten. In der Regel wollen wir den Feldern aber konkrete Werte zuordnen.

<!-- --{{1}}-- Idee des Beispiels:
       + Deklaration eines parameterlosen Konstruktors
       + Deklaration eines Konstruktors mit einzelnem Parameter
       + Überladen des Konstruktors
-->
```csharp               IndividualConstructor
using System;

public class Animal
{
  public string name;
  public string sound;

  public Animal(string name, string sound) {
  	this.name = name;
    this.sound = sound;
  }

  public void MakeNoise() {
    Console.WriteLine($"{this.name} makes {this.sound}");
  }
}

public class Program
{
  static void Main(string[] args){
    Animal dog = new Animal();
    dog.MakeNoise();
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> *Anmerkung:* Konstruktoren sind Methoden und folglich steht das gesamte Spektrum
> der Variabilität bei deren Definition zur Verfügung (Überladen, vordefinierte
> Variablen, Parameterlisten, usw.)

| Konstruktor                                  | Aufruf                                      |
| -------------------------------------------- | ------------------------------------------- |
| Aufruf des (impliziten) Standardkonstruktors | `Animal kitty = new Animal()`               |
| `public Animal(name, sound)`                 | `Animal kitty = new Animal("kitty","Miau")` |
| `public Animal(name, sound = "Miau")`        | `Animal kitty = new Animal("kitty")`        |


### `new` als Operator

Wir haben `new` bisher beiläufig benutzt — Zeit, einen Moment innezuhalten. Der `new`-Operator macht **drei Dinge in einem einzigen Schritt**:

1. **Speicher reservieren** — bei einer `class` auf dem **Heap**, bei einem `struct` *inline* dort, wo die Variable lebt (Stack oder als Feld eines anderen Objekts).
2. **Konstruktor aufrufen** — der Speicher wird mit den vom Konstruktor geforderten Werten gefüllt.
3. **Wert oder Referenz zurückgeben** — bei `class` eine *Referenz* auf das Heap-Objekt, bei `struct` den fertig initialisierten *Wert*.

```csharp
Animal a = new Animal("Kitty", "Miau");     // Heap-Allokation, ctor läuft, Referenz zurück
Point  p = new Point(3, 4);                 // Stack-Initialisierung, ctor läuft, Wert zurück
```

### Moderne `new`-Varianten

Der Klassiker `new <Typname>(...)` ist nur *eine* Form. Im modernen C# stehen Ihnen mehrere Varianten zur Verfügung:

```csharp      NewVariants
using System;

public class Animal
{
    public string name;
    public string sound;
    public int age;

    public Animal() { }
    public Animal(string name, string sound) { this.name = name; this.sound = sound; }
}

public class Program
{
    static void Main(string[] args)
    {
        // (1) Klassisch — Typ wird wiederholt
        Animal a = new Animal("Kitty", "Miau");

        // (2) Target-typed new (ab C# 9) — der linke Typ reicht
        Animal b = new("Wally", "Wuff");

        // (3) Object Initializer — Felder/Properties direkt nach dem ctor setzen
        Animal c = new Animal { name = "Berta", sound = "Muuh", age = 8 };

        // (4) Kombination: parameterloser ctor + Object Initializer
        Animal d = new() { name = "Rex", sound = "Wuff" };

        // (5) Arrays mit new
        int[] zahlen = new[] { 1, 2, 3, 4, 5 };

        Console.WriteLine($"{a.name}, {b.name}, {c.name} ({c.age}), {d.name}, [{string.Join(",", zahlen)}]");
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

> **Object Initializer** sind besonders praktisch, wenn eine Klasse viele Felder hat und Sie nicht für jede Kombination einen eigenen Konstruktor schreiben wollen. Der Compiler ruft erst den (parameterlosen) Konstruktor auf und führt dann die Zuweisungen aus.

### Überladen von Konstruktoren

Anders als Python erlaubt C# **mehrere Konstruktoren** mit unterschiedlichen Parameterlisten — *Überladung*. Der Compiler wählt anhand der Argumente aus.

```csharp      ConstructorOverload
using System;

public class Animal
{
    public string name;
    public string sound;
    public int age;

    public Animal()                                      // (1) parameterlos
    {
        this.name = "Unbekannt";
        this.sound = "?";
        this.age = 0;
    }

    public Animal(string name, string sound)             // (2) ohne Alter
    {
        this.name = name;
        this.sound = sound;
        this.age = 0;
    }

    public Animal(string name, string sound, int age)    // (3) vollständig
    {
        this.name = name;
        this.sound = sound;
        this.age = age;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Animal a = new Animal();
        Animal b = new Animal("Kitty", "Miau");
        Animal c = new Animal("Berta", "Muuh", 8);
        Console.WriteLine($"{a.name}, {b.name}, {c.name}");
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Python-Vergleich:** In Python gibt es nur *einen* `__init__` — dort lösen wir das mit Default-Werten oder `*args`. In C# entscheidet die Signatur (Anzahl + Typen der Parameter) den Aufruf.

### Fat Arrow

In Ergänzung sei auch noch auf die kompakte _Fat Arrow_ Darstellung im Zusammenhang mit Konstruktoren, die ja Funktionen wie alle anderen sind verwiesen. Wenn nur
eine Anweisung ausgeführt wird kann dies in einer Zeile realisiert werden.

```csharp                                      Constructors.cs9
using System;

public struct Animal
{
  public string name;
  public Animal(string name) => this.name = name;
  public void MakeNoise() {
  	Console.WriteLine("{0} makes Miau", name);
  }
}

public class Program
{
  static void Main(string[] args){
    Animal cat = new Animal("Kitty");
    cat.MakeNoise();
  }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

### Konstruktor-Verkettung mit `this(...)`

Wenn mehrere Konstruktoren ähnliche Arbeit leisten, kann ein Konstruktor einen anderen aufrufen — das vermeidet Doppelung:

```csharp      ConstructorChain
using System;

public class Animal
{
    public string name;
    public string sound;
    public int age;

    public Animal(string name, string sound) : this(name, sound, 0) { }

    public Animal(string name, string sound, int age)
    {
        this.name = name;
        this.sound = sound;
        this.age = age;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Animal a = new Animal("Kitty", "Miau");
        Console.WriteLine($"{a.name} ({a.age} Jahre)");
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Aufgabe:** Übersetzen Sie das `Cat`-Beispiel mit `super().__init__(...)` aus 07a nach C# — denken Sie an `: base(...)` (kommt in 09 ausführlich).

## ... und structs?

Kurzer Blick zurück auf die Datentypen in C#.

```ascii
                                     C# Typen
                                         |
                       .------------------------------------.
                       |                                    |
                   Werttypen                           Referenztypen
                       |                                    |
         .-------+-----+---+--------.        .-------+---------+-------.
         |       |         |        |        |       |         |       |
     Vordefi-  Enumer-  Structs   Tupel   Klassen  Inter    Arrays  Delegates
 nierte Typen  ation                     (String) -faces
         |     
         |      <----------------------------------------------------------->
         |           Klassenbibliotheksbasierte / Benutzerdefinierte Typen
         |
         .----+------+-----------+-------------.
         |           |           |             |
     Character    Ganzzahl   Gleitkommazahl   Bool
                     |
             .------+---------.
             |                |
     mit Vorzeichen     vorzeichenlos                                                                 .
```

### Ein kleiner Werttyp

Ein `struct` ist ein **benutzerdefinierter Werttyp** für *kleine, in sich abgeschlossene* Daten — typische Beispiele: `Point`, `Color`, `DateTime`, `Vector3`. Eine `struct`-Variable enthält den Wert direkt; eine `class`-Variable enthält nur die Referenz auf ein Objekt im Heap.

```csharp      StructExample
using System;

public class Point
//public struct Point
{
    public double x;
    public double y;

    public Point(double x, double y) { this.x = x; this.y = y; }

    public double DistanceTo(Point other)
    {
        double dx = this.x - other.x;
        double dy = this.y - other.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(3, 4);
        Console.WriteLine($"Abstand: {p1.DistanceTo(p2)}");
        Console.WriteLine(p1 == p2);
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

| Faustregel    | `class`                  | `struct`                              |
| ------------- | ------------------------ | ------------------------------------- |
| Wann?         | Standardfall             | nur kleine, unveränderliche Werte     |
| Vererbung     | ja                       | **nein**                              |
| Identität     | über Referenz            | über Wert                             |
| `null`        | erlaubt                  | nur als `Nullable<T>` / `T?`          |

> **Merke:** Im Zweifel `class`. `struct` ist eine Optimierung für klar abgegrenzte Wert-Konzepte.

### `record` und `record struct` — Wert*semantik* für Daten

Manchmal wollen wir die Bequemlichkeit einer Klasse, *aber* die Vergleichs-Semantik eines Werts: zwei Tiere mit gleichem Namen, Sound und Alter sollen als „gleich" gelten — egal, ob es sich um *zwei Objekte* oder *eine Referenz* handelt. Genau dafür gibt es seit C# 9/10 **Records**:

```csharp      RecordExample
using System;

public record Animal(string Name, string Sound, int Age);

public class Program
{
    static void Main(string[] args)
    {
        var a = new Animal("Kitty", "Miau", 5);
        var b = new Animal("Kitty", "Miau", 5);

        Console.WriteLine(a == b);            // True — wertvergleichend!
        Console.WriteLine(a);                 // automatisch lesbares ToString

        var c = a with { Age = 6 };           // nicht-zerstörende Kopie
        Console.WriteLine(c);
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

Records sind *kein neuer Familienzweig*, sondern syntaktischer Zucker — sie kommen in zwei Geschmacksrichtungen:

| Variante        | Familie     | Verwendung                                                       |
| --------------- | ----------- | ---------------------------------------------------------------- |
| `record class`  | Referenztyp | Standard-Record (auch nur als `record` schreibbar)               |
| `record struct` | Werttyp     | Wenn Sie zusätzlich die Werttyp-Speicherung wollen               |

Der Compiler erzeugt für jeden Record automatisch:

- einen Konstruktor mit allen Parametern,
- Properties statt nackter Felder,
- ein wertvergleichendes `Equals`/`GetHashCode`,
- eine `ToString()`-Implementierung,
- den `with`-Operator.

> **Ausblick:** *Properties*, *Operator-Überladung* und *Equals/GetHashCode-Verträge* — also genau das, was der Record-Compiler hier für Sie schreibt — folgen ausführlich in **VL 08**. Vererbung von Records: in **VL 09**.

### Und C-`struct`s?

`struct` als Schlüsselwort kennen Sie aus dem C Kontext. Der C#-`struct` heißt zwar genauso, ist aber konzeptionell ein anderes Tier. Die Tabelle zeigt vier Familien nebeneinander:

| Achse                        | C `struct`               | C# `struct`               | C# `class`              | C# `record`               |
| ---------------------------- | ------------------------ | ------------------------- | ----------------------- | ------------------------- |
| **Speicher / Kopiersemantik** | Wert (Stack), Kopie     | Wert (Stack), Kopie       | Referenz (Heap)         | Referenz (Heap)           |
| **Daten + Verhalten?**       | nur Daten                | Daten + Methoden          | Daten + Methoden        | Daten + Methoden (synthetisiert) |
| **Identität via**            | manuell (Feld für Feld)  | Wert (eingebaut)          | Referenz                | **Wert** (synthetisiert)  |
| **Vererbung**                | nein                     | nein                      | ja                      | ja (nur record→record)    |
| **Sichtbarkeit**             | alles offen              | `private`/`public`/...    | `private`/`public`/...  | `private`/`public`/...    |
| **Initialisierung**          | manuell / Designated Init | Konstruktor + Default     | Konstruktor + Default   | Compiler-generiert        |

Aus den Spalten lassen sich **drei konzeptionelle Sprünge** herauspräparieren — und genau diese Sprünge erklären die Sprachgeschichte:

1. **C-`struct` → C#-`struct`: vom Datencontainer zur kapselbaren Einheit.**
   Der entscheidende Schritt ist *Daten + Verhalten + Sichtbarkeit*. Der C-`struct` ist ein Speicherplan; jede Funktion, die damit arbeitet, liegt extern und kennt das Innere. Der C#-`struct` zieht das Verhalten *in den Typ hinein* und erlaubt, Inneres zu *verbergen*. Das ist die eigentliche OOP-Schwelle — nicht der Werttyp-Charakter (den teilen beide).

2. **C#-`struct` → C#-`class`: vom Wert zur Identität.**
   Hier wechselt nicht das Konzept „Daten + Verhalten", sondern das Konzept *Identität*. Zwei `struct`s sind gleich, wenn ihre Felder gleich sind — sie *sind* ihr Inhalt. Zwei `class`-Instanzen sind nur dann gleich, wenn sie *dasselbe* Objekt sind, sie haben eine Identität *jenseits* ihres aktuellen Inhalts. Das ist der philosophisch tiefste Sprung — und der Grund, warum **Vererbung** erst mit Klassen sinnvoll wird (eine abgeleitete Klasse erweitert ein Identitäts-tragendes Objekt).

3. **C#-`class` → C#-`record`: Wertvergleich zurückgewinnen, ohne Werttyp werden zu müssen.**
   Records lösen einen Konflikt: Heap-Speicherung und Vererbung (also Klasse), *aber* Wertvergleich. Records **entkoppeln** Identitätssemantik vom Speichermodell — beides war bei `struct` und `class` zwangsverbunden.


## Sichtbarkeitsmodifizierer

C# trennt — anders als Python mit der `_`-Konvention — **strikt** zwischen sichtbar und nicht sichtbar. Der Compiler erzwingt das.

| Modifizierer         | Bedeutung                                                            |
| -------------------- | -------------------------------------------------------------------- |
| `public`             | uneingeschränkt sichtbar                                             |
| `private`            | nur innerhalb der eigenen Klasse                                     |
| `protected`          | innerhalb der Klasse + abgeleiteter Klassen                          |
| `internal`           | innerhalb der eigenen Assembly (Standard auf Klassen-Ebene)          |
| `protected internal` | `protected` *oder* `internal`                                        |
| `private protected`  | `protected` *und* `internal`                                         |

```csharp      VisibilityDemo
using System;

public class Animal
{
    public string name;       // von außen lesbar/schreibbar
    private int age;          // nur innerhalb von Animal

    public Animal(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    public void Birthday()
    {
        this.age = this.age + 1;        // erlaubt
        Console.WriteLine($"{name} ist jetzt {age} Jahre alt.");
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Animal kitty = new Animal("Kitty", 5);
        kitty.Birthday();
        // kitty.age = 100;       // <- Compiler-Fehler: 'age' ist private
        kitty.name = "Tom";        // ok, weil public
    }
}
```
@LIA.eval(`["main.cs"]`, `mcs main.cs`, `mono main.exe`)

> **Python-Vergleich:** In 07a war `_age` eine Bitte. In C# wird `private int age;` vom Compiler durchgesetzt.

> **Vertiefung:** Vorlesung 08 zeigt **Properties** als idiomatische C#-Lösung für „kontrollierter Zugriff auf private Felder" (statt expliziter Getter/Setter wie in 07a). Vorlesung 09 erklärt `protected` und die Wechselwirkungen mit Vererbung.

## Beispiel: `internal` in Aktion

`public` und `private` sind in einem einzigen `.cs`-File leicht zu sehen. `internal` braucht eine **zweite Assembly**, damit der Effekt überhaupt sichtbar wird — innerhalb *einer* Assembly verhält sich `internal` wie `public`. Dazu steigen wir kurz in ein echtes Mini-Projekt unter [`code/08_OOP/assemblies_dotnet/`](code/08_OOP/assemblies_dotnet/) ein.

```ascii
assemblies_dotnet/
│
├── assemblies_dotnet.slnx               <- Solution: Klammer um beide Projekte (XML, ab .NET 9)
│
├── MyApp/                               <- Projekt 1: ausführbar
│   ├── MyApp.csproj                     (OutputType = Exe, ProjectReference -> MyClass)
│   └── Program.cs
│
└── MyClass/                             <- Projekt 2: Bibliothek
    ├── MyClass.csproj                   (kein OutputType -> Default: Library)
    └── Farmland.cs

       Nach 'dotnet build' entstehen:
       MyClass/bin/Debug/net9.0/MyClass.dll      <- Assembly 1
       MyApp/bin/Debug/net9.0/MyApp.dll          <- Assembly 2 (lädt MyClass.dll)
```

| Begriff       | Was es ist                                | Wofür                                                      |
| ------------- | ----------------------------------------- | ---------------------------------------------------------- |
| **Solution** (`.slnx`) | Verwaltungs-Container für mehrere Projekte | nur Tooling-Hilfe, baut selbst nichts. Klassisches Format `.sln` ist im Industrieumfeld noch verbreitet, das schlankere XML-Format `.slnx` ist seit .NET 9 verfügbar |
| **Projekt** (`.csproj`) | Beschreibung, was kompiliert wird     | erzeugt jeweils **eine** Assembly                         |
| **Assembly** (`.dll`/`.exe`) | physische Build-Ausgabe              | Deployment-Einheit + Sichtbarkeitsgrenze für `internal`   |

> **Häufige Verwechslung — bitte einmal explizit klarstellen:**
>
> | Achse        | Wer steuert?            | Sichtbarkeit                                  |
> | ------------ | ----------------------- | --------------------------------------------- |
> | **Namespace** | `namespace Farm { ... }` | logischer Name, hat *nichts* mit Sichtbarkeit zu tun |
> | **Assembly** | `.csproj` / Build       | physische Grenze; `internal` operiert *hier*  |
>
> `using Farm;` erlaubt den **Namespace**-Zugriff. `<ProjectReference>` erlaubt den **Assembly**-Zugriff. Beide sind unabhängig.

### Inhaltlicher Aufbau des Beispiels

In `MyClass/Farmland.cs` liegen zwei Typen mit unterschiedlicher Sichtbarkeit:

```csharp
namespace Farm
{
    internal struct Animal                       // <- nur in MyClass.dll sichtbar
    {
        public string name;
        public string sound;

        public Animal(string name, string sound = "Miau") { ... }
        public override string ToString() => $"Mein Name ist {name}, {sound}!";
    }

    public class FarmFacade                      // <- öffentliche Schnittstelle
    {
        private List<Animal> animals = new();

        public void   Register(string name, string sound = "Miau") { ... }   // legt Animal an
        public string MorningCall() { ... }                                  // gibt Text zurück
    }
}
```

`FarmFacade` *darf* `Animal` benutzen, weil beide zur selben Assembly gehören. Von außen — etwa aus `MyApp.dll` — ist nur `FarmFacade` sichtbar; `Animal` bleibt unsichtbar, obwohl der Quellcode in derselben Repository-Datei offen lesbar steht.

> **Beachten Sie das Fassaden-Muster:** `MorningCall()` gibt nur einen `string` zurück, also einen Standardtyp. Der interne `Animal`-Typ überquert die Assembly-Grenze nie. Das ist *die* idiomatische Verwendung von `internal`.

### Drei Schritte, die Sie selbst ausprobieren

**Schritt 1 — bauen und ausführen:**

```bash
cd code/08_OOP/assemblies_dotnet
dotnet build                       # baut MyClass.dll, dann MyApp.dll
dotnet run --project MyApp
```

Erwartete Ausgabe:

```
Mein Name ist Kitty, Miau!
Mein Name ist Wally, Wuff!
Mein Name ist Berta, Muuh!
```

Die App ruft `FarmFacade` auf, registriert drei Tiere und holt sich den `MorningCall`-Text. Das funktioniert — `FarmFacade` ist `public`.

**Schritt 2 — den verbotenen Zugriff sehen:**

In `MyApp/Program.cs` ist im `Main` ein Experimentier-Block enthalten. Entfernen Sie die führenden `//` vor der Animal-Zeile:

```csharp
var cat = new Animal("Kitty");
```

`dotnet build` liefert:

```
error CS0122: 'Farm.Animal' is inaccessible due to its protection level
```

Der Compiler **sieht** den Typ `Animal` (er steht ja im Quellcode der referenzierten DLL), erlaubt aber den Zugriff nicht — `internal` blockt über die Assembly-Grenze hinweg.

**Schritt 3 — die Grenze öffnen:**

In `MyClass/Farmland.cs` das `internal` durch `public` ersetzen:

```csharp
public struct Animal { ... }
```

`dotnet build` läuft wieder durch. Genau dieser Toggle macht den Unterschied erfahrbar: Eine Bibliothek entscheidet, *was* sie nach außen anbietet — und versteckt den Rest.

### Was Sie aus dem Beispiel mitnehmen sollten

| Erkenntnis                                                                                  | Warum es wichtig ist                                       |
| ------------------------------------------------------------------------------------------- | ---------------------------------------------------------- |
| Ein .NET-Programm kann aus mehreren Projekten und Assemblies bestehen                       | Realität jenseits der „eine `Main`-Datei"-Vorlesungssicht  |
| `internal` operiert auf **Assembly-Grenze**, nicht auf Namespace-Grenze                     | Häufigste Verwechslung — einmal richtig verstehen          |
| **Öffentliche Fassade + interne Implementierung** ist *das* Standardmuster für Bibliotheken | Genau dafür existiert `internal`                           |
| `dotnet build` erkennt Abhängigkeitsreihenfolge und baut nur, was sich geändert hat         | Modulare Builds = schnellere Iteration                     |

> **Vertiefung in 08:** Wir schauen uns dort an, wie diese Struktur per `dotnet new sln` / `dotnet new console` / `dotnet new classlib` / `dotnet add reference` von Hand erzeugt wird — und warum NuGet-Pakete im Kern dasselbe Konzept sind, nur mit zusätzlicher Distribution.

## Aufgaben

1. **Übersetzen.** Übertragen Sie das `Animal`-Beispiel aus 07a (mit `make_noise`) nach C#. Erzeugen Sie drei Instanzen und rufen Sie `MakeNoise()` in einer Schleife auf.

2. **Konstruktor-Überladung.** Schreiben Sie für `Animal` drei Konstruktoren: parameterlos, nur Name, vollständig (Name, Sound, Age). Verwenden Sie `: this(...)`-Verkettung, um Code-Duplikate zu vermeiden.

3. **`struct` vs. `class` erleben.** Modellieren Sie `Position { x, y }` einmal als `struct` und einmal als `class`. Schreiben Sie eine Methode `Move(Position p, int dx, int dy)`, die `p.x` und `p.y` ändert. Was beobachten Sie? Erklären Sie den Unterschied.

4. **Privatheit.** Machen Sie `age` aus dem `Animal`-Beispiel `private` und stellen Sie eine öffentliche Methode `GetAge()` zur Verfügung. Wo greift der Compiler ein, wenn Sie versuchen, von außen direkt zuzugreifen?