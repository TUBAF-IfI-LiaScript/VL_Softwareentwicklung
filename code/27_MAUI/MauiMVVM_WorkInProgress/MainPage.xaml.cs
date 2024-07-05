namespace MauiMVVM;

public partial class MainPage : ContentPage
{
	Student dataset = new Student("John");
	public MainPage()
	{
		InitializeComponent();
		LabelName.Text = dataset.Name;

		dataset.NameHasChanged += () => LabelName.Text = $"Current Name is {data.Name}";
	}	

    void Button_Clicked(System.Object sender, System.EventArgs e)
	{
		dataset.Name = EntryName.Text;
		//LabelName.Text = data.Name;
	}
}


