// Nutzen Sie die Switch-Pattern, um die individuellen Methoden Quack und MakeMuh
// aufzurufen

// Ergänzen Sie die individuellen Methoden für die Tierlaute durch eine
// übergreifende "Sound" Methode für Duck und Cow

// Unter welchen Bedingungen können Sie diese aufrufen? Was ist mit der Animalklasse?

using System;

public class Animal
{
  public string Name;
  public Animal(string name) {
    this.Name = name;
  }
  public void Eat() => Console.WriteLine("I'm eating");   // <- spezifische Methoden
  public void MakeNoise(){
      Console.WriteLine("Quack");
  }
}

public class Duck : Animal
{
  public Duck(string name) :base(name) { }
  public void Quack() => Console.WriteLine("Quack!");     // <- spezifische Methoden
  public void MakeNoise(){
      Console.WriteLine("Quack");
  }
}

public class Cow : Animal
{
  public Cow(string name) : base(name) { }
  public void MakeMuh() => Console.WriteLine("Muuh");  // <- spezifische Methoden
  public void MakeNoise(){
      Console.WriteLine("Muh");
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
       anim.MakeNoise();
  }
}
