using IVForum.App.Models;
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
		private static string BaseUrl = Properties.Res.BaseUrl;
		private static string MediaType = Properties.Res.MediaType;
		private static HttpClient client = new HttpClient() { BaseAddress = new System.Uri(BaseUrl) };

		private static void AuthorizeRequest(string token)
		{
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
		}

		public static async Task<bool> RequestSignUp(SignUpViewModel sender)
		{
			try
			{
				string modelString = JsonConvert.SerializeObject(sender, Formatting.Indented);

				HttpResponseMessage response = await client.PostAsync("account/register", new StringContent(modelString, Encoding.UTF8, MediaType));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var answer = await response.Content.ReadAsStringAsync();

					Token token = JsonConvert.DeserializeObject<Token>(answer);

					Settings.Save("token_auth", token.Auth_Token);
					Settings.Save("user_id", token.Id);

					return true;
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

					Token token = JsonConvert.DeserializeObject<Token>(answer);

					Settings.Save("token_auth", token.Auth_Token);
					Settings.Save("user_id", token.Id);

					return true;
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
					var answer = await response.Content.ReadAsStringAsync();

					Token token = JsonConvert.DeserializeObject<Token>(answer);

					Settings.Save("token_auth", token.Auth_Token);
					Settings.Save("user_id", token.Id);
					Settings.Save("expire_date", token.Expires_In);

					return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		private static LoginViewModel GetLoginViewModel(SignUpViewModel model)
		{
			return new LoginViewModel
			{
				Email = model.Email,
				Password = model.Password
			};
		}

		public static async Task<User> RequestUser()
		{
			try
			{
				string token = (string)Settings.GetValue("token_auth");
				AuthorizeRequest(token);

				HttpResponseMessage response = await client.GetAsync("account/get");

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var answer = await response.Content.ReadAsStringAsync();

					User user = JsonConvert.DeserializeObject<User>(answer);

					return user;
				}

				return null;
			}
			catch
			{
				return null;
			}
		}

		public static async Task RequestForums()
		{
			string token = (string) Settings.GetValue("token_auth");
			AuthorizeRequest(token);

			string user = (string) Settings.GetValue("user_id");

			HttpResponseMessage response = await client.GetAsync("forums/get/" + user);
		}

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
	}
}
