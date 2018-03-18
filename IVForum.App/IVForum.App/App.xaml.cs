using IVForum.App.Services;

using Xamarin.Forms;

namespace IVForum.App
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
		}

		protected override void OnStart()
		{
			MainPage = Settings.GetStartupPage();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
			Current.SavePropertiesAsync();
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
			Settings.LoginWithExistingUser();
		}
	}
}
