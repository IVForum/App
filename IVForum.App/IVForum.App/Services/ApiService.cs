using IVForum.App.Models;
using IVForum.App.Properties;
using IVForum.App.Resources;
using IVForum.App.ViewModels;
using IVForum.App.ViewModels.Personal.Projects;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IVForum.App.Services
{
	public class ApiService
    {
		private static string BaseUrl = Routes.Base;
		private static string MediaType = Res.MediaType;
		private static HttpClient client = new HttpClient() { BaseAddress = new System.Uri(BaseUrl) };

		private static void AuthorizeRequest(string token)
		{
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		}

		static ApiService()
		{
			if (!client.DefaultRequestHeaders.Contains("Authorization"))
			{
				if (Settings.Contains("token"))
				{
					string tokenString = (string)Settings.GetValue("token");

					Token token = JsonService.Deserialize<Token>(tokenString);

					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Auth_Token);
				}
				else
				{
					if (Settings.Contains("user_email"))
					{
						string email = (string) Settings.GetValue("user_email");
						string pass = (string)Settings.GetValue("user_password");

						LoginViewModel model = new LoginViewModel
						{
							Email = email,
							Password = pass
						};

						var result = RequestLogin(model).GetAwaiter().GetResult();

						if (!result)
						{
							for (int i = 0; i < 3; i++)
							{
								result = RequestLogin(model).GetAwaiter().GetResult();
								if (result)
									break;
							}
						}
					}
				}
			}
		}

		#region Account
		public static async Task<bool> RequestSignUp(SignUpViewModel sender)
		{
			try
			{
				string modelString = JsonConvert.SerializeObject(sender, Formatting.Indented);

				HttpResponseMessage response = await client.PostAsync("account/register", new StringContent(modelString, Encoding.UTF8, MediaType));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var answer = await response.Content.ReadAsStringAsync();

					Settings.Save("token", answer);

					Token token = JsonService.Deserialize<Token>(answer);
					User user = await RequestLoggedUserDetails(token.Id);

					if (user != null)
					{
						string userString = JsonService.Serialize(user);
						Settings.Save("user", userString);
						return true;
					}
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		public static async Task<bool> RequestLogin(SignUpViewModel sender)
		{
			try
			{
				LoginViewModel model = GetLoginViewModel(sender);

				string modelString = JsonConvert.SerializeObject(model, Formatting.Indented);

				HttpResponseMessage response = await client.PostAsync("account/login", new StringContent(modelString, Encoding.UTF8, "application/json"));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var answer = await response.Content.ReadAsStringAsync();

					Settings.Save("token", answer);

					Token token = JsonService.Deserialize<Token>(answer);
					User user = await RequestLoggedUserDetails(token.Id);

					if (user != null)
					{
						string userString = JsonService.Serialize(user);
						Settings.Save("user", userString);
						return true;
					}
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		public static async Task<bool> RequestLogin(LoginViewModel sender)
		{
			try
			{
				string modelString = JsonConvert.SerializeObject(sender, Formatting.Indented);

				HttpResponseMessage response = await client.PostAsync("account/login", new StringContent(modelString, Encoding.UTF8, "application/json"));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string answer = await response.Content.ReadAsStringAsync();

					Settings.Save("token", answer);

					Token token = JsonService.Deserialize<Token>(answer);
					User user = await RequestLoggedUserDetails(token.Id);

					if (user != null)
					{
						string userString = JsonService.Serialize(user);
						Settings.Save("user", userString);
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public static async Task<User> RequestLoggedUserDetails(string userId)
		{
			HttpResponseMessage response =  await client.GetAsync(Routes.AccountGetUser);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string userString = await response.Content.ReadAsStringAsync();

				User user = JsonService.Deserialize<User>(userString);
				return user;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					response = await client.PostAsync(Routes.AccountGetUser, GetStringContent(userId));
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string userString = await response.Content.ReadAsStringAsync();
						User user = JsonService.Deserialize<User>(userString);
						return user;
					}
				}
				return null;
			}
		}

		public static bool UpdateUser()
		{
			return true;
		}

		private static LoginViewModel GetLoginViewModel(SignUpViewModel model)
		{
			return new LoginViewModel
			{
				Email = model.Email,
				Password = model.Password
			};
		}
		#endregion

		#region Forums
		public static async Task RequestForums()
		{
			string token = (string) Settings.GetValue("token_auth");
			AuthorizeRequest(token);

			string user = (string)Settings.GetValue("user_id");

			HttpResponseMessage response = await client.GetAsync("forums/get/" + user);
		}
		
		#endregion

		public static async Task RequestProjects()
		{
			client.DefaultRequestHeaders.Add("Bearer", "");
			HttpResponseMessage response = await client.GetAsync("projects");
			Debug.WriteLine(response.Content);
		}

		public static async Task<List<Forum>> RequestUserForums()
		{
			client.DefaultRequestHeaders.Add("Bearer", "");
			HttpResponseMessage response = await client.GetAsync("");
			Debug.WriteLine(response.Content);
			return Content.GetForums();
		}

		public static async Task RequestUserProjects()
		{
			client.DefaultRequestHeaders.Add("Bearer", "");
			HttpResponseMessage response = await client.GetAsync("");
			Debug.WriteLine(response.Content);
		}

		public static async Task PostProject(CreateProjectViewModel model)
		{
			client.DefaultRequestHeaders.Add("Bearer", "");
			string modelString = JsonService.Serialize(model);
			HttpResponseMessage response = await client.PostAsync("projects/create", GetStringContent(modelString));
		}

		private static StringContent GetStringContent(string serializedModel)
		{
			return new StringContent(serializedModel, Encoding.UTF8, MediaType);
		}

		public static async Task Startup()
		{
			if (Settings.Contains("loggedin"))
			{
				string email = (string)Settings.GetValue("email");
				string password = (string)Settings.GetValue("password");

				LoginViewModel model = new LoginViewModel()
				{
					Email = email,
					Password = password
				};

				var result = await RequestLogin(model);

				if (!result)
				{
					for (int i = 0; i < 3; i++)
					{
						result = await RequestLogin(model);
						if (result) break;
					}
				}
			}
		}

		#region AddViews
		public static async void AddView(Forum f)
		{
			try
			{
				var View = new
				{
					ForumId = f.Id
				};

				HttpResponseMessage response = await client.PostAsync(Routes.Base + Routes.ForumAddView, new StringContent(View.ToString(), Encoding.UTF8, MediaType));

				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					for (int i = 0; i < 3; i++)
					{
						response = await client.PostAsync(Routes.Base + Routes.ForumAddView, new StringContent(View.ToString(), Encoding.UTF8, MediaType));
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
							break;
					}
				}
			}
			catch
			{
				return;
			}

		}

		public static async void AddView(Project p)
		{
			try
			{
				var View = new
				{
					ProjectId = p.Id
				};

				HttpResponseMessage response = await client.PostAsync(Routes.Base + Routes.ForumAddView, new StringContent(View.ToString(), Encoding.UTF8, MediaType));

				if (response.StatusCode != System.Net.HttpStatusCode.OK)
				{
					for (int i = 0; i < 3; i++)
					{
						response = await client.PostAsync(Routes.Base + Routes.ForumAddView, new StringContent(View.ToString(), Encoding.UTF8, MediaType));
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
							break;
					}
				}
			}
			catch
			{
				return;
			}
		} 
		#endregion
	}
}
