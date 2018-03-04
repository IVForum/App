using IVForum.App.Models;
using IVForum.App.Properties;
using IVForum.App.ViewModels;
using IVForum.App.ViewModels.Personal.Projects;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IVForum.App.Services
{
	public class ApiService
    {
		private static string MediaType = Res.MediaType;
		private static HttpClient client;

		static ApiService()
		{
			client = new HttpClient()
			{
				BaseAddress = new Uri(Routes.Base)
			};

			if (!client.DefaultRequestHeaders.Contains("Authorization"))
			{
				if (Settings.Contains("token"))
				{
					string tokenString = (string) Settings.GetValue("token");

					Token token = JsonService.Deserialize<Token>(tokenString);

					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Auth_Token);
				}
				else
				{
					if (Settings.Contains("user"))
					{
						string userString = (string) Settings.GetValue("user");
						User user = JsonService.Deserialize<User>(userString);

						LoginViewModel model = new LoginViewModel
						{
							Email = user.Email,
							Password = user.Password
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

				HttpResponseMessage response = await client.PostAsync(Routes.AccountRegister, GetStringContent(modelString));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var tokenString = await response.Content.ReadAsStringAsync();

					Settings.Save("token", tokenString);

					Token token = JsonService.Deserialize<Token>(tokenString);
					User user = await RequestUserDetails(token.Id);

					if (user != null)
					{
						string userString = JsonService.Serialize(user);
						Settings.Save("user", userString);
						return true;
					}
				}

				return false;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
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
					User user = await RequestUserDetails(token.Id);

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

				HttpResponseMessage response = await client.PostAsync(Routes.AccountLogin, new StringContent(modelString, Encoding.UTF8, "application/json"));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string answer = await response.Content.ReadAsStringAsync();

					Settings.Save("token", answer);

					Token token = JsonService.Deserialize<Token>(answer);
					User user = await RequestUserDetails(token.Id);

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

		public static async Task<User> RequestUserDetails(string userId)
		{
			EnsureTokenExists();
			HttpResponseMessage response = await client.GetAsync(Routes.AccountGetUser);

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
					response = await client.GetAsync(Routes.AccountGetUser);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string userString = await response.Content.ReadAsStringAsync();
						User user = JsonService.Deserialize<User>(userString);
						Settings.Save("user", userString);
						return user;
					}
				}
				return null;
			}
		}

		private static void EnsureTokenExists()
		{
			if (!client.DefaultRequestHeaders.Contains("Authorization"))
			{
				if (Settings.Contains("token"))
				{
					string tokenString = (string)Settings.GetValue("token");

					Token token = JsonService.Deserialize<Token>(tokenString);

					client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Auth_Token);
				}
			}
		}

		public static async Task<bool> UpdateUser(User model)
		{
			EnsureTokenExists();

			string modelString = JsonService.Serialize(model);

			HttpResponseMessage response = await client.PostAsync(Routes.AccountUpdate, GetStringContent(modelString));

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return true;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					response = await client.PostAsync(Routes.AccountUpdate, GetStringContent(modelString));
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
						return true;
				}

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
		#endregion

		#region Forums
		public static async Task<List<Forum>> RequestForums()
		{
			try
			{
				EnsureTokenExists();

				HttpResponseMessage response = await client.GetAsync(Routes.ForumGetAll);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string forumsString = await response.Content.ReadAsStringAsync();
					return JsonService.Deserialize<List<Forum>>(forumsString);
				}
				return null;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return null;
			}
		}

		public static async Task<List<Forum>> RequestForums(Guid userId)
		{
			string path = Routes.AccountPersonalForums + userId.ToString();
			HttpResponseMessage response = await client.GetAsync(path);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string forumsString = await response.Content.ReadAsStringAsync();
				List<Forum> forums = JsonService.Deserialize<List<Forum>>(forumsString);
				return forums;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					response = await client.GetAsync(Routes.Base + Routes.AccountPersonalForums + userId);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string forumsString = await response.Content.ReadAsStringAsync();
						List<Forum> forums = JsonService.Deserialize<List<Forum>>(forumsString);
						return forums;
					}
				}

				return null;
			}
		}

		public static async Task<List<Forum>> RequestSubscribedForums(Guid userId)
		{
			string route = Routes.ForumSubscribed + userId.ToString();
			HttpResponseMessage response = await client.GetAsync(route);

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string forumsString = await response.Content.ReadAsStringAsync();
				List<Forum> forums = JsonService.Deserialize<List<Forum>>(forumsString);
				return forums;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					response = await client.GetAsync(Routes.ForumSubscribed + userId.ToString());
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string forumsString = await response.Content.ReadAsStringAsync();
						List<Forum> forums = JsonService.Deserialize<List<Forum>>(forumsString);
						return forums;
					}
				}

				return new List<Forum>();
			}
		}

		public static async Task<bool> CreateForum(CreateNewViewModel model)
		{
			string modelString = JsonService.Serialize(model);
			HttpResponseMessage response = await client.PostAsync(Routes.ForumCreate, GetStringContent(modelString));

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return true;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					response = await client.PostAsync(Routes.ForumCreate, GetStringContent(modelString));
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						return true;
					}
				}

				return false;
			}

		}
		#endregion

		#region Projects
		public static async Task<List<Project>> RequestProjects()
		{
			try
			{
				EnsureTokenExists();

				HttpResponseMessage response = await client.GetAsync(Routes.ProjectGetAll);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string forumsString = await response.Content.ReadAsStringAsync();
					return JsonService.Deserialize<List<Project>>(forumsString);
				}
				return null;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return null;
			}
		}

		public static async Task<bool> UpdateProject(Project model)
		{
			string content = JsonService.Serialize(model);
			var result = await client.PostAsync(Routes.Base + Routes.ProjectUpdate, GetStringContent(content));

			if (result.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return true;
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					result = await client.PostAsync(Routes.Base + Routes.ProjectUpdate, GetStringContent(content));
					if (result.StatusCode == System.Net.HttpStatusCode.OK)
						return true;
				}
				return false;
			}
		}

		public static async Task<bool> DeleteProject(Project model)
		{
			try
			{
				string content = JsonService.Serialize(model);
				var response = await client.PostAsync(Routes.Base + Routes.ProjectDelete, GetStringContent(content));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
				{
					for (int i = 0; i < 3; i++)
					{
						response = await client.PostAsync(Routes.Base + Routes.ProjectDelete, GetStringContent(content));
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
						{
							return true;
						}
					}
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		#endregion



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
