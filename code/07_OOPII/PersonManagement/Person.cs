using System;

namespace PersonManagment
{
  class Person{
    // *************** Felder ************************************************
    string name;                     // beachte Groß/ Kleinschreibung
    public int Geburtsjahr;          // der Variablennamen!

    // ************** Konstruktoren ******************************************
    public Person(string name, int geburtsjahr){
      this.name = name;
      Geburtsjahr = geburtsjahr;
    }

    // ************** Methoden ***********************************************
    int AktuellesAlter () => DateTime.Today.Year - Geburtsjahr;

    public override string ToString(){
       return name + " ist " + AktuellesAlter().ToString() + " Jahre alt.";
    }

    // ************* Operatoren **********************************************
    public static bool operator> (Person person1, Person Person2){
      // TODO Hausaufgabe
      return true;
    }
    public static bool operator<(Person person1, Person Person2){
      // TODO Hausaufgabe
      return true;
    }
  }
}
