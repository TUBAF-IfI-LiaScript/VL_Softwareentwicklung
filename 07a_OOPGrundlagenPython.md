<!--

author:   Sebastian Zug, Galina Rudolf, André Dietrich
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.0
language: de
narrator: Deutsch Female
comment:  Konzeptioneller Einstieg in die Objektorientierung anhand von Python — für Teilnehmer ohne C#-Hintergrund. Klassen, Objekte, Konstruktor, Methoden, Vererbung.
tags:
logo:

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/07a_OOPGrundlagenPython.md)

# OOP — Konzeptioneller Einstieg (Python)

| Parameter                | Kursinformationen                                                                                  |
| ------------------------ | -------------------------------------------------------------------------------------------------- |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                                    |
| **Teil:**                | `7a/27`                                                                                            |
| **Semester**             | @config.semester                                                                                   |
| **Hochschule:**          | @config.university                                                                                 |
| **Inhalte:**             | @comment                                                                                           |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/07a_OOPGrundlagenPython.md |
| **Autoren**              | @author                                                                                            |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------

> **Hinweis zum Format:** Diese Vorlesung ergänzt die Reihe 07–09 als Einstieg für Teilnehmer ohne C#-Hintergrund. Wir nutzen Python, weil die Syntax die Konzepte weniger verdeckt. In den Folgevorlesungen werden dieselben Ideen in C# vertieft.

### Lernziele

Nach der Vorlesung können Sie ...

- erklären, *welches Problem* Objektorientierung löst,
- den Unterschied zwischen **Klasse** und **Objekt** in eigenen Worten beschreiben,
- eine eigene Klasse mit Attributen, Methoden und Konstruktor in Python schreiben,
- mehrere Objekte zu einer größeren Struktur zusammensetzen (**Komposition**),
- mit **Vererbung**, **Überschreiben** und **`super()`** Code wiederverwenden,
- den Unterschied zwischen Klassenattributen und Instanzattributen benennen,
- die ersten Schritte der **Kapselung** anwenden (`_`-Konvention).

### Zeitplan (90 Minuten)

| Zeit       | Thema                                          |
| ---------- | ---------------------------------------------- |
| 0 – 10 min | Motivation: Warum OOP?                         |
| 10 – 20 min | Bauplan/Instanz, Begriffsbildung              |
| 20 – 35 min | Erste Klasse `Animal`, `__init__`, `self`     |
| 35 – 45 min | Klassen- vs. Instanzattribute, `__str__`      |
| 45 – 50 min | Pause                                          |
| 50 – 60 min | Komposition: `Farm` enthält `Animal`s         |
| 60 – 70 min | Kapselung mit `_`-Konvention                  |
| 70 – 85 min | Vererbung, Override, `super()`, Polymorphie   |
| 85 – 90 min | Brücke zu C#, Aufgabenüberblick               |

## Warum Objektorientierung?

### Ausgangsproblem

Stellen Sie sich vor, Sie verwalten einen kleinen Bauernhof in einem Programm. Drei Tiere sollen gespeichert werden — jeweils mit Name, Geräusch und Alter.

Ein erster, „naiver" Ansatz:

```python      LooseVariables.py
name1 = "Kitty"
sound1 = "Miau"
age1 = 5

name2 = "Wally"
sound2 = "Wuff"
age2 = 3

name3 = "Berta"
sound3 = "Muuh"
age3 = 8

print(f"{name1} ({age1} Jahre) macht {sound1}")
print(f"{name2} ({age2} Jahre) macht {sound2}")
print(f"{name3} ({age3} Jahre) macht {sound3}")
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Frage:** Was passiert, wenn der Bauernhof auf 50 Tiere wächst? Was, wenn sich „macht" in „sagt" ändert?

                                       {{1-2}}
*******************************************************************************

Probleme dieses Ansatzes:

1. **Daten driften auseinander.** `name1`, `sound1`, `age1` gehören zusammen — der Compiler weiß das aber nicht.
2. **Verhalten ist verstreut.** Die `print`-Zeile wiederholt sich. Eine Änderung muss überall nachgezogen werden.
3. **Es skaliert nicht.** 50 Tiere = 150 Variablen + 50 fast identische `print`-Zeilen.

> **Idee der Objektorientierung:**
> Wir bündeln *Daten* (Name, Geräusch, Alter) und *Verhalten* (Geräusch ausgeben) zu einer Einheit — einem **Objekt**.

*******************************************************************************

                                       {{2-3}}
*******************************************************************************

### Erster Lösungsversuch: Dictionaries und Funktionen

Bevor wir Klassen einführen, schauen wir uns an, wie weit man mit den bisher bekannten Mitteln kommt. Wir bündeln die Daten zu einem Tier in einem **Dictionary** und schreiben eine Funktion, die darauf arbeitet:

```python      DictAndFunction.py
def make_noise(animal):
    print(f"{animal['name']} ({animal['age']} Jahre) macht {animal['sound']}")

kitty = {"name": "Kitty", "sound": "Miau", "age": 5}
wally = {"name": "Wally", "sound": "Wuff", "age": 3}
berta = {"name": "Berta", "sound": "Muuh", "age": 8}

make_noise(kitty)
make_noise(wally)
make_noise(berta)
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

Das ist schon deutlich besser. Aber drei Probleme bleiben:

1. **Keine Garantie, dass die Daten zusammenpassen.** Niemand hindert uns daran, ein Dictionary ohne `sound` zu bauen — der Fehler erscheint erst beim Aufruf.
2. **Daten und Funktionen sind getrennt.** Wer das Dictionary benutzt, muss wissen, *welche* Funktionen dazu gehören. Das steht nirgends im Code.
3. **Keine Bauplan-Beschreibung.** Was ein „Tier" ist, ergibt sich nur indirekt aus den Schlüsseln, die zufällig benutzt werden.

> **Frage:** Wie könnte eine Sprache uns helfen, Daten und passende Funktionen *zusammenzuhalten* und einen *Bauplan* zu beschreiben, an den sich alle Tiere halten müssen?

*******************************************************************************

### Analogie: Bauplan und Häuser

<!--
style="width: 100%; max-width: 700px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
       Bauplan (Klasse)                          Instanzen (Objekte)
       +------------------+
       |   Haus           |                +--------+ +--------+ +--------+
       |------------------|     -->        | Haus 1 | | Haus 2 | | Haus 3 |
       | - Grundfläche    |                | 120 m² | | 80 m²  | | 200 m² |
       | - Stockwerke     |                | 2 Et.  | | 1 Et.  | | 3 Et.  |
       | - Farbe          |                | rot    | | weiß   | | gelb   |
       +------------------+                +--------+ +--------+ +--------+
                                                                                  .
```

| Begriff             | Bedeutung                                                                |
| ------------------- | ------------------------------------------------------------------------ |
| **Klasse**          | Bauplan — beschreibt, *welche* Eigenschaften und *welches* Verhalten ein Objekt hat |
| **Objekt** / Instanz | Konkretes Exemplar nach diesem Bauplan — mit eigenen Werten              |
| **Attribut** / Feld | Eine Eigenschaft (z. B. `name`, `farbe`)                                 |
| **Methode**         | Ein Verhalten / eine Funktion, die das Objekt ausführen kann             |

> **Merke:** Eine Klasse beschreibt einen Typ. Ein Objekt ist ein konkreter Vertreter dieses Typs.

## Die erste Klasse

### Aufbau

In Python schreiben wir den Bauplan mit dem Schlüsselwort `class`:

```python
class Animal:
    # Attribute werden im Konstruktor definiert
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    # Methode
    def make_noise(self):
        print(f"{self.name} ({self.age} Jahre) macht {self.sound}")
```

Drei Bestandteile sind neu und brauchen eine Erklärung:

- **`__init__`** — der **Konstruktor**. Wird *einmal* beim Erzeugen eines Objekts aufgerufen. Hier werden die Attribute initialisiert.
- **`self`** — der Verweis auf *dieses konkrete Objekt*. (In C# heißt das später `this`.) Er muss als erster Parameter jeder Methode stehen.
- **`self.name = name`** — legt das Attribut `name` *am Objekt* ab. Ohne `self.` wäre `name` nur eine lokale Variable in der Funktion.

### Objekte erzeugen und nutzen

```python      FirstClass.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def make_noise(self):
        print(f"{self.name} ({self.age} Jahre) macht {self.sound}")

# Drei Instanzen aus demselben Bauplan
kitty = Animal("Kitty", "Miau", 5)
wally = Animal("Wally", "Wuff", 3)
berta = Animal("Berta", "Muuh", 8)

kitty.make_noise()
wally.make_noise()
berta.make_noise()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Beobachtung:** Aus *einem* Bauplan entstehen *beliebig viele* Objekte mit jeweils eigenen Werten. Das Verhalten (`make_noise`) ist nur *einmal* beschrieben.

### Was bedeutet `self`?

Jeder Methodenaufruf läuft auf einem konkreten Objekt. Python übergibt dieses Objekt automatisch als ersten Parameter:

```python
kitty.make_noise()
# entspricht intern:
# Animal.make_noise(kitty)
```

`self` ist also schlicht „das Objekt, auf dem die Methode gerade arbeitet". Die Methode kann darüber lesen (`self.name`) und schreiben (`self.age = self.age + 1`).

```python      SelfDemo.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def have_birthday(self):
        self.age = self.age + 1
        print(f"{self.name} ist jetzt {self.age} Jahre alt.")

kitty = Animal("Kitty", "Miau", 5)
kitty.have_birthday()
kitty.have_birthday()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Aufgabe:** Erweitern Sie die Klasse um eine Methode `rename(self, new_name)`, die den Namen ändert und das neue Geräusch ausgibt.

### Klassenattribute vs. Instanzattribute

Manche Eigenschaften gehören *zu jedem einzelnen Objekt* (jedes Tier hat einen anderen Namen). Andere Eigenschaften gehören *zur ganzen Klasse* — sie sind für alle Tiere gleich. Beispiel: Alle Tiere auf dem Planeten leben auf demselben Planeten.

```python      ClassAttribute.py
class Animal:
    planet = "Erde"          # Klassenattribut — gehört zur Klasse

    def __init__(self, name, sound):
        self.name = name      # Instanzattribut — gehört zum Objekt
        self.sound = sound

    def info(self):
        print(f"{self.name} lebt auf der {Animal.planet} und macht {self.sound}.")


kitty = Animal("Kitty", "Miau")
wally = Animal("Wally", "Wuff")

kitty.info()
wally.info()

# Wenn sich der Planet ändert, ändert er sich für ALLE Instanzen:
Animal.planet = "Mars"
kitty.info()
wally.info()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

| Art               | Wo definiert?                              | Wem gehört es?                    |
| ----------------- | ------------------------------------------ | --------------------------------- |
| Klassenattribut   | direkt unter `class Foo:`                  | der Klasse — geteilt von allen    |
| Instanzattribut   | im Konstruktor mit `self.x = ...`          | dem konkreten Objekt              |

> **Faustregel:** Im Zweifel Instanzattribut. Klassenattribute eignen sich für Konstanten oder wirklich gemeinsam genutzte Daten.

### Spezielle Methode `__str__`

Was passiert, wenn wir versuchen, ein Objekt direkt mit `print()` auszugeben?

```python      DefaultStr.py
class Animal:
    def __init__(self, name, sound):
        self.name = name
        self.sound = sound

kitty = Animal("Kitty", "Miau")
print(kitty)
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

Wir sehen etwas wie `<__main__.Animal object at 0x7f...>`. Das ist die Standard-Darstellung — nicht hilfreich. Mit der speziellen Methode `__str__` definieren wir selbst, wie unser Objekt als Text aussehen soll:

```python      CustomStr.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def __str__(self):
        return f"Animal({self.name}, {self.age} Jahre, sagt '{self.sound}')"


kitty = Animal("Kitty", "Miau", 5)
print(kitty)
print(f"Mein Tier: {kitty}")
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Hintergrund:** Methoden, deren Namen in `__` eingerahmt sind (man spricht „Dunder-Methoden"), sind Haken, die Python automatisch aufruft. Wir werden in C# später ähnliche Mechanismen sehen — dort heißen sie `ToString()`, `Equals()` etc.

## Mehrere Objekte zusammenführen

Wir können Objekte selbst wieder in andere Objekte stecken. Eine `Farm` enthält eine Liste von `Animal`s:

```python      Farm.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


class Farm:
    def __init__(self, address):
        self.address = address
        self.animals = []          # leere Liste

    def add_animal(self, animal):
        self.animals.append(animal)

    def morning_call(self):
        print(f"Guten Morgen auf {self.address}!")
        for animal in self.animals:
            animal.make_noise()


farm = Farm("Biobauernhof Freiberg")
farm.add_animal(Animal("Kitty", "Miau", 5))
farm.add_animal(Animal("Wally", "Wuff", 3))
farm.add_animal(Animal("Berta", "Muuh", 8))

farm.morning_call()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Beobachtung:** Die `Farm` weiß nichts über die Innereien eines `Animal`. Sie ruft nur `make_noise()` auf und vertraut darauf, dass jedes Tier weiß, was zu tun ist. Genau das ist die Stärke von OOP.

## Kapselung — Was darf nach außen sichtbar sein?

### Das Problem

Bisher konnten wir auf jedes Attribut von außen direkt zugreifen — auch schreibend. Das ist gefährlich:

```python      NoEncapsulation.py
class Animal:
    def __init__(self, name, age):
        self.name = name
        self.age = age


kitty = Animal("Kitty", 5)
kitty.age = -100             # Unsinn, aber Python lässt es zu
print(f"{kitty.name} ist {kitty.age} Jahre alt.")
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

Niemand möchte ein Tier mit dem Alter `-100` haben. Wir brauchen eine Möglichkeit zu signalisieren: *„Bitte greift nicht direkt darauf zu — nutzt diese Methode."*

### Konvention: Unterstrich als „Bitte nicht anfassen"

Python hat — anders als C# oder Java — keine harten Sichtbarkeitsmodifizierer. Stattdessen gilt eine **Konvention**: Ein führender Unterstrich `_` markiert ein Attribut als *intern*.

```python      EncapsulationConvention.py
class Animal:
    def __init__(self, name, age):
        self.name = name
        self._age = age            # _age ist "intern"

    def get_age(self):
        return self._age

    def set_age(self, new_age):
        if new_age < 0:
            print(f"Ungültiges Alter: {new_age}")
            return
        self._age = new_age


kitty = Animal("Kitty", 5)
kitty.set_age(6)               # ok
print(f"{kitty.name} ist {kitty.get_age()} Jahre alt.")

kitty.set_age(-100)            # wird abgelehnt
print(f"{kitty.name} ist {kitty.get_age()} Jahre alt.")
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

| Schreibweise         | Bedeutung                                                                |
| -------------------- | ------------------------------------------------------------------------ |
| `name`               | öffentlich — jeder darf lesen/schreiben                                  |
| `_name`              | „bitte nicht von außen anfassen" — nur für die Klasse selbst             |
| `__name` (zwei `_`)  | erzwingt Namensumbenennung intern — selten gebraucht                     |

> **Merke:** Kapselung trennt das *Was* (öffentliches Verhalten) vom *Wie* (interne Umsetzung). Wer die Klasse benutzt, soll nur das Was sehen müssen — wir können das Wie ändern, ohne dass Aufrufer kaputt gehen.

> In C# werden wir dafür echte Modifizierer (`public`, `private`) und sogenannte **Properties** kennenlernen — Vorlesung 07/08.

## Vererbung

### Motivation

Auf unserem Bauernhof sollen verschiedene Tierarten leben. Ein **Hund** kann zusätzlich `fetch()` (Stöckchen apportieren), eine **Kuh** liefert `milk()`. Beide sind aber *immer noch Tiere* — sie haben Namen, Alter, Geräusch.

Eine naive Lösung wäre, die `Animal`-Felder in jeder neuen Klasse zu wiederholen. Das ist nicht nur lästig, sondern fehleranfällig.

> **Idee der Vererbung:**
> Eine neue Klasse („Kind") *übernimmt* alle Eigenschaften und Methoden einer bestehenden Klasse („Eltern") und *ergänzt* oder *verändert* sie nur dort, wo es nötig ist.

### Syntax in Python

```python
class Dog(Animal):       # Dog erbt von Animal
    ...
```

Die Klammer hinter dem Klassennamen nennt die **Basisklasse**. Alles, was `Animal` kann, kann `Dog` automatisch auch.

### Beispiel: Hund und Kuh

```python      Inheritance.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


class Dog(Animal):
    def fetch(self):
        print(f"{self.name} bringt das Stöckchen zurück.")


class Cow(Animal):
    def milk(self):
        print(f"{self.name} gibt heute 12 Liter Milch.")


rex = Dog("Rex", "Wuff", 4)
berta = Cow("Berta", "Muuh", 8)

rex.make_noise()    # geerbt von Animal
rex.fetch()         # eigene Methode

berta.make_noise()  # geerbt von Animal
berta.milk()        # eigene Methode
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

`Dog` und `Cow` mussten weder einen Konstruktor noch `make_noise` neu schreiben — beides kommt von `Animal`.

### Methoden überschreiben

Manchmal soll das Kind etwas *anders* machen als das Elternteil. Eine Katze macht nicht einfach `Miau` — sie schnurrt zusätzlich.

```python      Override.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


class Cat(Animal):
    def make_noise(self):
        # Wir überschreiben die Methode aus Animal
        print(f"{self.name} macht {self.sound} ... und schnurrt.")


kitty = Cat("Kitty", "Miau", 5)
kitty.make_noise()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Merke:** Die Methode mit dem gleichen Namen in der Kindklasse „gewinnt". Diesen Mechanismus nennen wir **Überschreiben** (engl. *override*).

### Auf die Eltern zurückgreifen — `super()`

Oft soll das Kind das Verhalten der Eltern *erweitern*, nicht *ersetzen*. Mit `super()` rufen wir die Methode der Basisklasse auf:

```python      Super.py
class Animal:
    def __init__(self, name, sound, age):
        self.name = name
        self.sound = sound
        self.age = age

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


class Cat(Animal):
    def __init__(self, name, age, fur_color):
        super().__init__(name, "Miau", age)   # Eltern-Konstruktor
        self.fur_color = fur_color            # neues Attribut

    def make_noise(self):
        super().make_noise()                  # Eltern-Verhalten
        print(f"  ({self.fur_color}es Fell, schnurrt zufrieden.)")


kitty = Cat("Kitty", 5, "schwarz")
kitty.make_noise()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

`super()` ist ein Verweis auf die Elternklasse. So nutzen wir bestehendes Verhalten und ergänzen es punktuell — ohne Code zu duplizieren.

### Eine gemeinsame Liste verschiedener Tiere

Weil `Dog` und `Cat` *beide* `Animal`s sind, dürfen sie in derselben Liste stehen — und beim Aufruf von `make_noise()` reagiert jedes auf seine eigene Art.

```python      Polymorphism.py
class Animal:
    def __init__(self, name, sound):
        self.name = name
        self.sound = sound

    def make_noise(self):
        print(f"{self.name} macht {self.sound}")


class Dog(Animal):
    def make_noise(self):
        print(f"{self.name} bellt laut: WUFF WUFF!")


class Cat(Animal):
    def make_noise(self):
        print(f"{self.name} schnurrt und macht leise {self.sound}.")


tiere = [
    Dog("Rex", "Wuff"),
    Cat("Kitty", "Miau"),
    Animal("Berta", "Muuh"),
]

for tier in tiere:
    tier.make_noise()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Das ist Polymorphie:** Derselbe Aufruf (`tier.make_noise()`) führt — abhängig vom tatsächlichen Typ des Objekts — zu unterschiedlichem Verhalten. Wir vertiefen das in den folgenden Vorlesungen.

### Mehrstufige Hierarchien

Vererbung kann auch über mehrere Stufen gehen. Ein `Puppy` ist ein `Dog`, und ein `Dog` ist ein `Animal`. Jede Stufe ergänzt etwas Eigenes.

```python      Hierarchy.py
class Animal:
    def __init__(self, name):
        self.name = name

    def describe(self):
        print(f"Ich heiße {self.name} und bin ein Tier.")


class Dog(Animal):
    def __init__(self, name, breed):
        super().__init__(name)
        self.breed = breed

    def describe(self):
        super().describe()
        print(f"Genauer: ein Hund der Rasse {self.breed}.")


class Puppy(Dog):
    def __init__(self, name, breed, weeks):
        super().__init__(name, breed)
        self.weeks = weeks

    def describe(self):
        super().describe()
        print(f"Und zwar erst {self.weeks} Wochen alt!")


bello = Puppy("Bello", "Labrador", 8)
bello.describe()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Beobachtung:** Jede Klasse fügt nur das hinzu, was an ihrer Stufe Sinn ergibt. Die Methode `describe` zeigt durch die `super()`-Kette das gemeinsame Wissen *plus* das spezifische Wissen jeder Stufe.

### Typprüfung mit `isinstance`

Manchmal müssen wir wissen, was für ein Objekt wir gerade vor uns haben. `isinstance(obj, Klasse)` antwortet — und berücksichtigt die Vererbungshierarchie:

```python      IsInstance.py
class Animal: pass
class Dog(Animal): pass
class Puppy(Dog): pass

bello = Puppy("Bello")  # type: ignore
print(isinstance(bello, Puppy))    # True
print(isinstance(bello, Dog))      # True — ein Puppy ist auch ein Dog
print(isinstance(bello, Animal))   # True — und auch ein Animal
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Merke:** Vererbung drückt eine **„ist-ein"-Beziehung** aus. Ein Puppy *ist ein* Hund. Ein Hund *ist ein* Tier. Wenn diese Beziehung in der echten Welt nicht stimmt, ist Vererbung das falsche Werkzeug — dann passt eher Komposition (siehe `Farm` ↔ `Animal`).

## Fallstudie: Ein kleiner Online-Shop

Zum Abschluss verbinden wir alle Konzepte in einem etwas größeren Beispiel. Ein Mini-Shop verwaltet **Produkte** (mit Preis und Bestand) und **digitale Produkte**, die zusätzlich eine Downloadgröße haben. Ein **Warenkorb** sammelt Bestellungen.

```python      MiniShop.py
class Product:
    def __init__(self, name, price, stock):
        self.name = name
        self._price = price        # intern, über Methoden zugreifen
        self._stock = stock

    def get_price(self):
        return self._price

    def is_available(self, amount):
        return self._stock >= amount

    def reserve(self, amount):
        if not self.is_available(amount):
            return False
        self._stock -= amount
        return True

    def __str__(self):
        return f"{self.name} ({self._price:.2f} €, {self._stock} auf Lager)"


class DigitalProduct(Product):
    def __init__(self, name, price, size_mb):
        # Digitale Produkte haben unbegrenzten "Bestand"
        super().__init__(name, price, stock=10**9)
        self.size_mb = size_mb

    def __str__(self):
        return f"{self.name} (digital, {self._price:.2f} €, {self.size_mb} MB)"


class Cart:
    def __init__(self):
        self.items = []   # Liste von (Produkt, Menge)

    def add(self, product, amount=1):
        if product.reserve(amount):
            self.items.append((product, amount))
            print(f"  + {amount} x {product.name} hinzugefügt")
        else:
            print(f"  ! {product.name} nicht ausreichend verfügbar")

    def total(self):
        return sum(p.get_price() * n for p, n in self.items)

    def show(self):
        print("Warenkorb:")
        for product, amount in self.items:
            print(f"  - {amount} x {product}")
        print(f"  = Summe: {self.total():.2f} €")


buch    = Product("OOP-Lehrbuch", 29.90, stock=3)
ebook   = DigitalProduct("OOP-Lehrbuch (PDF)", 14.90, size_mb=12)
stift   = Product("Bleistift", 0.80, stock=50)

cart = Cart()
cart.add(buch, 2)
cart.add(ebook, 1)
cart.add(stift, 5)
cart.add(buch, 5)        # mehr als verfügbar — wird abgelehnt
cart.show()
```
@LIA.eval(`["main.py"]`, `none`, `python3 main.py`)

> **Welche Konzepte stecken hier drin?**
>
> - **Klasse + Konstruktor** (`Product`, `__init__`)
> - **Kapselung** (`_price`, `_stock` mit Zugriffs-Methoden)
> - **Spezielle Methoden** (`__str__`)
> - **Vererbung + super()** (`DigitalProduct(Product)`)
> - **Komposition** (`Cart` enthält Liste von Produkten)
> - **Polymorphie** (`__str__` reagiert je nach Produkttyp anders)

## Zusammenfassung

| Begriff           | Python-Syntax                              | Bedeutung                                          |
| ----------------- | ------------------------------------------ | -------------------------------------------------- |
| Klasse            | `class Animal:`                            | Bauplan                                            |
| Objekt / Instanz  | `kitty = Animal("Kitty", "Miau", 5)`       | Konkreter Vertreter des Bauplans                   |
| Konstruktor       | `def __init__(self, ...):`                 | Wird beim Erzeugen *einmal* aufgerufen             |
| `self`            | erster Parameter jeder Methode             | „Dieses konkrete Objekt"                           |
| Attribut          | `self.name = name`                         | Eigenschaft *am Objekt*                            |
| Methode           | `def make_noise(self):`                    | Verhalten, das das Objekt ausführen kann           |
| Klassenattribut   | direkt unter `class Foo:`                  | gehört der Klasse, geteilt von allen Instanzen     |
| Kapselung         | `self._x` (Konvention)                     | „bitte nicht von außen anfassen"                   |
| `__str__`         | `def __str__(self): return ...`            | Wie das Objekt als Text aussieht                   |
| Vererbung         | `class Dog(Animal):`                       | Dog übernimmt alles von Animal                     |
| Überschreiben     | gleiche Methode in Kindklasse neu definieren | Kind reagiert anders als Eltern                  |
| `super()`         | `super().__init__(...)` / `super().make_noise()` | Auf Eltern-Verhalten zurückgreifen           |
| `isinstance`      | `isinstance(obj, Animal)`                  | Prüft Typ inkl. Vererbungskette                    |
| Polymorphie       | gleiche Methode, unterschiedliches Verhalten | Aufrufer muss konkreten Typ nicht kennen         |

## Brücke zur nächsten Vorlesung (C#)

Die Konzepte sind universell. In C# sehen Sie dieselben Ideen mit anderer Syntax:

| Konzept           | Python                            | C# (Vorschau)                                   |
| ----------------- | --------------------------------- | ----------------------------------------------- |
| Klasse            | `class Animal:`                   | `public class Animal { ... }`                   |
| Konstruktor       | `def __init__(self, name):`       | `public Animal(string name) { ... }`            |
| `self` / `this`   | `self.name`                       | `this.name`                                     |
| Objekt erzeugen   | `kitty = Animal("Kitty")`         | `Animal kitty = new Animal("Kitty");`           |
| Vererbung         | `class Dog(Animal):`              | `public class Dog : Animal { ... }`             |
| Eltern aufrufen   | `super().__init__(...)`           | `: base(...)` bzw. `base.MakeNoise()`           |

In den Vorlesungen 07, 08 und 09 gehen wir tiefer:

- **07** — `structs` und Klassen in C#, Sichtbarkeit (`public`/`private`), Wert- vs. Referenztypen
- **08** — Kapselung, Properties, Operatorenüberladung
- **09** — Vererbung in C#, abstrakte Methoden, Polymorphie im Detail

## Aufgaben

1. **Eigene Klasse modellieren.** Wählen Sie ein Objekt aus Ihrem Alltag (Buch, Fahrrad, Café-Bestellung) und schreiben Sie dafür eine Klasse mit mindestens drei Attributen und zwei Methoden. Erzeugen Sie zwei Instanzen.

2. **Vererbung üben.** Erweitern Sie das `Animal`-Beispiel um die Klasse `Bird`. Vögel haben zusätzlich ein Attribut `can_fly` (bool) und eine Methode `fly()`, die abhängig von `can_fly` „fliegt los" oder „kann nicht fliegen" ausgibt.

3. **Eltern erweitern.** Schreiben Sie eine Klasse `Puppy(Dog)`, deren `make_noise()` zuerst die Methode aus `Dog` aufruft und danach `... und wedelt mit dem Schwanz` ergänzt.

4. **Reflexion.** Erklären Sie in eigenen Worten (3–4 Sätze), warum die OOP-Variante des Bauernhof-Beispiels besser skaliert als die Variante mit losen Variablen vom Anfang.

5. **Kapselung anwenden.** Erweitern Sie die Klasse `Cart` aus der Fallstudie um eine Methode `remove(self, product)`, die ein Produkt aus dem Warenkorb entfernt und den reservierten Bestand wieder freigibt. Stellen Sie sicher, dass `_stock` nicht negativ und nicht über den ursprünglichen Wert hinaus wachsen kann.

6. **Hierarchie modellieren.** Modellieren Sie eine Klassenhierarchie für Verkehrsmittel: `Vehicle` (mit `name` und Methode `move()`), darunter `Car`, `Bicycle`, `Boat`. Jede Klasse soll `move()` passend überschreiben (z. B. „rollt", „tritt", „schwimmt"). Legen Sie eine Liste aus drei verschiedenen Verkehrsmitteln an und rufen Sie `move()` in einer Schleife auf.

7. **Quizfragen zur Selbstkontrolle.**
   - Was ist der Unterschied zwischen einer Klasse und einem Objekt?
   - Warum ist `self` als erster Parameter jeder Methode notwendig?
   - Wann verwenden Sie Vererbung, wann eher Komposition?
   - Was bewirkt der Aufruf `super().__init__(...)` im Konstruktor einer Kindklasse?
   - Warum ist die `_`-Konvention nur eine *Bitte*, kein technischer Schutz?
