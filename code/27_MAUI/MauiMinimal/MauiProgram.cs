using Microsoft.Maui;
using Microsoft.Maui.Controls;

// Minimal Example
// ---------------------------------

public class App : Application
{
    public App()
    {
        //Hauptseite der App als C#-Code
        MainPage = new ContentPage 
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to our new lecture on .NET MAUI!"
                    },
                    new Button
                    {
                        Text = "Click me!",
                        Command = new Command(() => { 
                            MainPage.DisplayAlert("Alert", "You clicked the button!", "OK");
                        })
                    }
                }
            }
        };
    }
}

public static class MauiProgram
{
    //gibt Instanz der MauiApp zurück
    public static MauiApp CreateMauiApp()
    {
        //erstellt Builder-Objekt
        var builder = MauiApp.CreateBuilder();
        //konfiguriert Builder-Objekt: App als Haupteinstiegspunkt zu verwenden
        builder .UseMauiApp<App>();
        return builder.Build();
    }
}
