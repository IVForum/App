using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class Settings
    {
		public static void Save(string key, object value)
		{
			Application.Current.Properties[key] = value;
		}

		public static bool ContainsKey(string key)
		{
			return Application.Current.Properties.ContainsKey(key);
		}

		public static object GetValue(string key)
		{
			return Application.Current.Properties[key];
		}

		public static void Remove(string key)
		{
			Application.Current.Properties.Remove(key);
		}
    }
}
