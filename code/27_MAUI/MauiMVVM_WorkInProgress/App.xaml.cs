//using MauiMVVM.Views;

namespace MauiMVVM;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new MainPage(); // traditional basic UI View with C# code
        //MainPage = new MVC(); // MVVM UI View with C# code
		//MainPage = new MVVC(); // MVVC UI View with C# code
	}
}
