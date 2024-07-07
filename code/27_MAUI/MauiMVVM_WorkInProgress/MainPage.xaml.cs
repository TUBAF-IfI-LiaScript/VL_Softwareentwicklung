namespace MauiMVVM;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        StudentModel studentModel = new StudentModel("John");
        StudentViewModel studentViewModel = new StudentViewModel(studentModel);
		BindingContext = studentViewModel;
		studentViewModel.ChangePropertyAfterDelay(5);
    }
}
