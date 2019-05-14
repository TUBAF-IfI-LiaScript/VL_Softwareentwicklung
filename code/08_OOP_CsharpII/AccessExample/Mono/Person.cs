using System;

public class Person
{
  public int Geburtsjahr = 1972;
  public string Name = "Lukas Podolski";
  string email = "LukasPodolski@gmx.de";     // implizit private

  public int BerechneAlter(){
     return DateTime.Now.Year - this.Geburtsjahr;
  }

  protected void SendEmail(string text){
     Console.WriteLine("MailTo - {0} - {1}", email, text);
  }
}
