using IVForum.App.Models;

using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class Settings
	{
		public static void Save(string key, object value)
		{
			Application.Current.Properties[key] = value;
			Application.Current.SavePropertiesAsync();
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

		public static void Remove(params string[] keys)
		{
			foreach (string key in keys)
			{
				Application.Current.Properties.Remove(key);
			}

			Application.Current.SavePropertiesAsync();
		}

		public static object GetValue(string key)
		{
			Application.Current.Properties.TryGetValue(key, out object value);
			return value;
		}

		public static void TryGetValue(string key, out object value)
		{
			Application.Current.Properties.TryGetValue(key, out value);
		}

		public static void Logout()
		{
			Remove("loggedin", "token", "user_email", "user_password");
		}

		public static User GetLoggedUser()
		{
			User user = JsonService.Deserialize<User>((string)GetValue("user"));
			return user;
		}
	}
}