using System.ComponentModel;

namespace MauiMVVM;

public class StudentViewModel: INotifyPropertyChanged
{
    public Command ChangeNameCommand { private set; get; }

    public event PropertyChangedEventHandler PropertyChanged;

    // Bindable properties
    private string _name { get; set;}

    public StudentViewModel(string name)
    {
        _name = name;

        ChangeNameCommand = new Command(
        execute: () =>
        {
            Name = Name.ToUpper();
        });

        ChangePropertyAfterDelay(5);
    }

    // Business logic
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
    }

    public async Task ChangePropertyAfterDelay(int delayInSeconds)
    {
        await Task.Delay(delayInSeconds * 1000);
        Name = "Jane";
        Console.WriteLine("Property updated to: " + Name);
    }
}