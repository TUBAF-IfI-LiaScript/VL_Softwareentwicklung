using System.ComponentModel;
using System.Runtime.CompilerServices;

public class MainViewModel : INotifyPropertyChanged
{
    string datum = " ";

    public string Datum
    {
        get => datum;
        set
        {
            datum = value;
            NotifyPropertyChanged();
        }
    }

    public MainViewModel()
    {
        datum = "heute";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
