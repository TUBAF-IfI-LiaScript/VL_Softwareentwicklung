using System.ComponentModel;

public class Student_MVVC: INotifyPropertyChanged
{
    public Command ChangeNameCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    // Bindable properties
    public string Name { get; set;}

    public Student_MVVC(string name)
    {
        Name = name;
    }

    // Business logic
    ChangeNameCommand = new Command 

    ChangeNameCommand = new Command(() =>
    {
        Name = Name + "!";
        OnPropertyChanged(nameof(Name));
    });


    public async Task ChangePropertyAfterDelay(int delayInSeconds)
    {
        await Task.Delay(delayInSeconds * 1000);
        Name = "Jane";
        Console.WriteLine("Property updated to: " + Name);
    }
}