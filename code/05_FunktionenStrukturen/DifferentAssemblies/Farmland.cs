using System;
using System.Collections;
using System.Collections.Generic;

namespace Farm
{
  internal struct Animal{
    public string name;
    public string sound;

    public Animal(string name, string sound = "Miau"){
      this.name = name;
      this.sound = sound;
    }
    public override string ToString() => "My name ist " + name + ", " + sound + "!";
  }
}
