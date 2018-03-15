using IVForum.App.Data.Models;
using IVForum.App.Views.Main;

using System;

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
			try
			{
				return Application.Current.Properties.ContainsKey(key);
			}
			catch (Exception)
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

		public static User GetLoggedUser()
		{
			User user = JsonService.Deserialize<User>((string)GetValue("user"));
			return user;
		}

		public static Page GetStartupPage()
		{
			if (Contains("loggedin"))
			{
				LoginWithExistingUser();
				return new Main();
			}
			return new StartupTabbedPage();
		}

		public static async void LoginWithExistingUser()
		{
			if (Contains("loggedin"))
			{
				User model = GetLoggedUser();
				var result = await AccountService.Login(model);
			}
		}

		public static void Logout()
		{
			Application.Current.Properties.Clear();
			Application.Current.SavePropertiesAsync();
		}
	}
}