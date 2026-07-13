//using MauiMVVM.Views;

namespace MauiMVVM;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
}
