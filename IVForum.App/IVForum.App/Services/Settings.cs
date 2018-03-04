using IVForum.App.Models;
using IVForum.App.Views.Main;

using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class Settings
	{
		public static void Save(string key, object value)
		{
			if (!Contains(key))
			{
				Application.Current.Properties.Add(key, value);
				Application.Current.SavePropertiesAsync();
			}
		}

		public static bool Contains(string key)
		{
			try
			{
				return Application.Current.Properties.ContainsKey(key);
			}
			catch (System.Exception)
			{
				return false;
			}
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
				if (Contains(key))
				{
					Application.Current.Properties.Remove(key);
				}
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
			Remove("loggedin", "token", "user");
		}

		public static User GetLoggedUser()
		{
			User user = JsonService.Deserialize<User>((string)GetValue("user"));
			return user;
		}

		public static Page GetStartupPage()
		{
			if (Contains("loggedin"))
			{
				return new Main();
			}
			return new StartupTabbedPage();
		}

		public static void Cleanup()
		{
			Application.Current.Properties.Clear();
			Application.Current.SavePropertiesAsync();
		}
	}
}