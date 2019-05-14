using System;

public class Fußballspieler : Person {
  private int rückennummer;
  protected int GeschosseneTore = 0;

  public int Rückennummer{
    set {if (value < 100) rückennummer = value;
         else Console.WriteLine("Fehler, Rückennummer ungültig");}
    get {return rückennummer;}
  }

  internal void SendMessage(){
    if (this.GeschosseneTore == 0) {this.SendEmail("Wohl nicht Dein Tag?");}
    else {this.SendEmail("Nicht Dein Tag?");}
  }
}
