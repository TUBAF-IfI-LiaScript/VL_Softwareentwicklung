namespace PersonManagment
{
  class Person{
    // *************** Felder ************************************************
    string name;                     // beachte Groß/ Kleinschreibung
    public int Geburtsjahr;          // der Variablennamen!

    // ************** Konstruktoren ******************************************
    void Person(string name, int geburtsjahr){
      this.name = name;
      Geburtsjahr = geburtsjahr;
    }

    // ************** Methoden ***********************************************
    int AktuellesAlter () => DateTime.Today.Year - Geburtsjahr;

    public string override ToString(){
       return name + " ist " + AktuellesAlter().ToString() + " Jahre alt.";
    }

    // ************* Operatoren **********************************************
    public static bool operator> (Person person1, Person Person2){
      // TODO Hausaufgabe
    }
  }
}

// mcs Person.cs Program.cs
// mono Programm.exe
