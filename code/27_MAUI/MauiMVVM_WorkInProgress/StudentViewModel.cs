using System.ComponentModel;

public class StudentViewModel : INotifyPropertyChanged
{
    private StudentModel studentModel;
    public Command ChangeNameCommand { private set; get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public StudentViewModel(StudentModel model)
    {
        studentModel = model;

        ChangeNameCommand = new Command(execute: () =>
        {
            Name = studentModel.ToUpper();
        });

        ChangePropertyAfterDelay(5);
    }

    public string Name
    {
        get { return studentModel.Name; }
        set
        {
            studentModel.Name = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
        }
    }

    public async Task ChangePropertyAfterDelay(int delayInSeconds)
    {
        await Task.Delay(delayInSeconds * 1000);
        Name = "Jane";
    }
}
