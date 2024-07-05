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


