using Android.App;
using Android.Widget;

using IVForum.App.Android.Shared;
using IVForum.App.Models;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace IVForum.App.Android.Shared
{
	public class MessageAndroid : IMessage
	{
		public void LongAlert(string message)
		{
			Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
		}

		public void ShortAlert(string message)
		{
			Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
		}
	}
}