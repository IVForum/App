using IVForum.App.Data;
using IVForum.App.Data.Shared;
using IVForum.App.Services;

using Xamarin.Forms;

namespace IVForum.App
{
	public partial class App : Application
	{
		private static AppDbContext dbContext;
		public static AppDbContext Database
		{
			get
			{
				if (dbContext is null)
				{
					string dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("IVForum.db");
					dbContext = new AppDbContext(dbPath);
				}
				return dbContext;
			}
		}

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
