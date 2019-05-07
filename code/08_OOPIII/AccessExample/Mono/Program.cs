using System;

public class Program
{
  public static void Main(string[] args){
    Fußballspieler Stürmer = new Fußballspieler();
    Stürmer.Geburtsjahr = 1982;
    Stürmer.SendMessage();
  }
}

// mcs Person.cs Program.cs Fußballspieler.cs -out: OneAssembly.exe

// mcs -target:library Fußballspieler.cs Person.cs
// mcs -reference:Fußballspieler.dll Program.cs -out: AssemblyWithDLL.exe
