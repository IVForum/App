using IVForum.App.Models;

using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class Alert
    {
		public static void Send<T>(T data)
		{
			DependencyService.Get<IMessage>().LongAlert(data.ToString());
		}
    }
}
