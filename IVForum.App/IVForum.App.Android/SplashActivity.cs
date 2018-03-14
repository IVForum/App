using Android.App;

namespace IVForum.App.Droid
{
	[Activity(Label = "IVForum", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = false, NoHistory = true)]
	public class SplashActivity : Activity
	{
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}