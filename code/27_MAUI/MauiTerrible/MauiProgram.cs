using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

// THIS CODE IS A MOTIVATION EXAMPLE FOR MVC OR MVVM PATTERNS. IT IS NOT
// A RECOMMENDED PRACTICE TO USE THIS CODE IN PRODUCTION!
// GUI CONTROLS SHOULD NOT BE DIRECTLY BOUND TO DATA MODELS!

public class App : Application
{
    public App()
    {
        Student data = new Student("John");
        data.ChangePropertyAfterDelay(5);

        var entry = new Entry { Placeholder = "Enter a new name here!" };

        var label = new Label { 
            HorizontalTextAlignment = TextAlignment.Center,
            Text = $"Current Name is {data.Name}" 
        };

        var button = new Button { Text = "Submit" };

        button.Clicked += (sender, args) =>
        {
            data.Name = entry.Text;
        };

        data.NameHasChanged += () => label.Text = $"Current Name is {data.Name}";

        MainPage = new ContentPage
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = { entry, button, label }
            }
        };
    }
}

public class Student
{
    public event Action NameHasChanged;
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                NameHasChanged?.Invoke();
            }
        }
    }

    public Student(string name)
    {
        Name = name;
    }

    public async Task ChangePropertyAfterDelay(int delayInSeconds)
    {
        await Task.Delay(delayInSeconds * 1000);
        Name = "Jane";
        Console.WriteLine("Property updated to: " + Name);
    }
}

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();
        return builder.Build();
    }
}
