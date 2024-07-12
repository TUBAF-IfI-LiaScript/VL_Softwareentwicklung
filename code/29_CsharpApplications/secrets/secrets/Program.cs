using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string APIsecret = config["ThingSpeak:ServiceAPIKey"];

Console.WriteLine(APIsecret);
