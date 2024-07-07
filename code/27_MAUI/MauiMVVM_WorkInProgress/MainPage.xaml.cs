namespace MauiMVVM;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
		StudentViewModel studentViewModel = new StudentViewModel("John");
		BindingContext = studentViewModel;
		studentViewModel.ChangePropertyAfterDelay(5);
    }
}
