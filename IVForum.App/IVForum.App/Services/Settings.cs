using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class Settings
	{
		public static async void Save(string key, object value)
		{
			Application.Current.Properties.Add(key, value);
			await Application.Current.SavePropertiesAsync();
		}

		public static bool Contains(string key)
		{
			return Application.Current.Properties.ContainsKey(key);
		}

		public static void Remove(string key)
		{
			Application.Current.Properties.Remove(key);
			Application.Current.SavePropertiesAsync();
		}

		public static object GetValue(string key)
		{
			Application.Current.Properties.TryGetValue(key, out object value);
			return value;
		}
	}
}