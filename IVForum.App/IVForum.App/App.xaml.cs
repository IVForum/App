using IVForum.App.Views.Main;

using Xamarin.Forms;

namespace IVForum.App
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
			bool loggedin = Current.Properties.ContainsKey("loggedin");

			if (loggedin)
			{
				MainPage = new Main();
			}
			else
			{
				MainPage = new StartupTabbedPage();
			}
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
