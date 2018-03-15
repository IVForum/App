using IVForum.App.Data.Models;
using IVForum.App.Data.Shared;
using IVForum.App.Properties;
using IVForum.App.ViewModels;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IVForum.App.Services
{
	public class ApiService
    {
		private const string MediaType = "application/json";
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

						var result = Account.Login(model).GetAwaiter().GetResult();

						if (!result.IsSuccess)
						{
							for (int i = 0; i < 3; i++)
							{
								result = Account.Login(model).GetAwaiter().GetResult();
								if (result.IsSuccess)
									break;
							}
						}
					}
				}
			}
		}

		private static void EnsureTokenExists()
		{
			client.DefaultRequestHeaders.Clear();

			string tokenString = (string)Settings.GetValue("token");

			Token token = JsonService.Deserialize<Token>(tokenString);

			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Auth_Token);
		}
		private static async Task<bool> EnsureAuthorized(HttpStatusCode statusCode)
		{
			if (statusCode != HttpStatusCode.Unauthorized)
			{
				return true;
			}
			else
			{
				var result = await AccountService.Login(Settings.GetLoggedUser());

				if (result.IsSuccess)
				{
					EnsureTokenExists();
					return true;
				}

				return false;
			}
		}
		private static async Task<HttpResult> CheckHttpResultResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				return new HttpResult(true, response.StatusCode);
			}
			string message = await response.Content.ReadAsStringAsync();
			return new HttpResult(false, response.StatusCode, message);
		}
		private static async Task<List<Project>> CheckProjectListResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string forumsString = await response.Content.ReadAsStringAsync();
				return JsonService.Deserialize<List<Project>>(forumsString);
			}
			return new List<Project>();
		}
		private static async Task<List<Forum>> CheckForumListResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string forumsString = await response.Content.ReadAsStringAsync();
				return JsonService.Deserialize<List<Forum>>(forumsString);
			}
			return new List<Forum>();
		}
		private static async Task<User> CheckUserResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string userString = await response.Content.ReadAsStringAsync();
				User user = JsonService.Deserialize<User>(userString);
				return user;
			}

			return null;
		}
		private static async Task<List<Bill>> CheckBillListResponse(HttpResponseMessage response)
		{
			if (response.StatusCode == HttpStatusCode.OK)
			{
				string forumsString = await response.Content.ReadAsStringAsync();
				return JsonService.Deserialize<List<Bill>>(forumsString);
			}
			return new List<Bill>();
		}

		internal class Account
		{
			public static async Task<HttpResult> SignUp(SignUpViewModel sender)
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
							return new HttpResult(true, response.StatusCode);
						}
					}

					return new HttpResult(false, response.StatusCode, await response.Content.ReadAsStringAsync());
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> Login(SignUpViewModel sender)
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
							return new HttpResult(true, response.StatusCode);
						}
					}

					return new HttpResult(false, response.StatusCode, response.Content.ToString());
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> Login(LoginViewModel model)
			{
				try
				{
					string route = Routes.AccountLogin;
					string modelString = JsonService.Serialize(model);

					HttpResponseMessage response = await client.PostAsync(route, GetStringContent(modelString));

					if (response.StatusCode == HttpStatusCode.OK)
					{
						string answer = await response.Content.ReadAsStringAsync();
						Settings.Save("token", answer);
						Token token = JsonService.Deserialize<Token>(answer);
						await RequestUserDetails(token.Id);
						return new HttpResult(true, response.StatusCode, answer);
					}
					return new HttpResult(false, response.StatusCode, await response.Content.ReadAsStringAsync());
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<User> Details(Guid userId)
			{
				try
				{
					string route = Routes.AccountGetUserById + userId.ToString();

					var response = await client.GetAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckUserResponse(response);
				}
				catch (Exception)
				{
					Alert.Send("Error de connexió");
					return null;
				}
			}
			public static async Task<User> RequestUserDetails(string userId)
			{
				try
				{
					EnsureTokenExists();
					string route = Routes.AccountGetUserById + userId;
					HttpResponseMessage response = await client.GetAsync(route);

					if (response.StatusCode == HttpStatusCode.OK)
					{
						string userString = await response.Content.ReadAsStringAsync();
						User user = JsonService.Deserialize<User>(userString);
						Settings.Save("user", userString);
						return user;
					}

					return null;
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					Alert.Send("Error de connexió");
					return null;
				}
			}
			public static async Task<HttpResult> Update(User model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.AccountUpdate;

					var response = await client.PutAsync(route, GetStringContent(modelString));

					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
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

			public static async Task<List<Forum>> Forums()
			{
				try
				{
					EnsureTokenExists();
					HttpResponseMessage response = await client.GetAsync(Routes.AccountPersonalForums);

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string forumsString = await response.Content.ReadAsStringAsync();
						return JsonService.Deserialize<List<Forum>>(forumsString);
					}

					return new List<Forum>();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Forum>();
				}
			}
			public static async Task<List<Project>> Projects()
			{
				try
				{
					EnsureTokenExists();

					User user = Settings.GetLoggedUser();
					string route = Routes.ProjectGetByUserId + user.Id.ToString();

					HttpResponseMessage response = await client.GetAsync(route);

					if (response.StatusCode == HttpStatusCode.OK)
					{
						string forumsString = await response.Content.ReadAsStringAsync();
						return JsonService.Deserialize<List<Project>>(forumsString);
					}

					return new List<Project>();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Project>();
				}
			}
			public static async Task<List<Forum>> Subscriptions()
			{
				try
				{
					EnsureTokenExists();

					User user = Settings.GetLoggedUser();
					string route = Routes.AccountSubscribedForumsByUserId + user.Id.ToString();

					HttpResponseMessage response = await client.GetAsync(route);

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						string forumsString = await response.Content.ReadAsStringAsync();
						return JsonService.Deserialize<List<Forum>>(forumsString);
					}
					return new List<Forum>();
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Forum>();
				}
			}
		}

		internal class Forums
		{
			static Forums() => EnsureTokenExists();

			public static async Task<List<Forum>> Get()
			{
				try
				{
					string route = Routes.ForumGet;

					var response = await client.GetAsync(route);

					return await CheckForumListResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Forum>();
				}
			}
			public static async Task<List<Forum>> Get(Guid userId)
			{
				try
				{
					string route = Routes.ForumGetByUserId + userId.ToString();
					
					var response = await client.GetAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckForumListResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Forum>();
				}
			}
			public static async Task<HttpResult> Create(Forum model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ForumCreate;

					var response = await client.PostAsync(route, GetStringContent(modelString));
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> Delete(Forum model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ForumDelete + model.Id.ToString();

					var response = await client.DeleteAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
					throw;
				}
			}
			public static async Task<HttpResult> AddProjectToForum(SubscriptionViewModel model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ForumSubscribeProject;

					var response = await client.PostAsync(route, GetStringContent(modelString));
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<List<Project>> Projects(Guid forumId)
			{
				try
				{
					string route = Routes.ForumProjects + forumId.ToString();

					var response = await client.GetAsync(route);
					return await CheckProjectListResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Project>();
				}
			}
			public static async Task<HttpResult> AddView(Forum model)
			{
				try
				{
					string route = Routes.ForumView + model.Id.ToString();

					var response = await client.PutAsync(route, null);
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new HttpResult(false, e.Message);
				}

			}
		}

		internal class Projects
		{
			static Projects() => EnsureTokenExists();

			public static async Task<List<Project>> Get()
			{
				try
				{
					string route = Routes.ProjectGet;
					var response = await client.GetAsync(route);

					return await CheckProjectListResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Project>();
				}
			}
			public static async Task<List<Project>> Get(Guid userId)
			{
				try
				{
					string route = Routes.ProjectGetByUserId + userId.ToString();
					var response = await client.GetAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckProjectListResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new List<Project>();
				}
			}
			public static async Task<HttpResult> Create(Project model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ProjectCreate;

					var response = await client.PostAsync(route, GetStringContent(modelString));
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> Update(Project model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ProjectUpdate;

					var response = await client.PutAsync(route, GetStringContent(modelString));
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> Delete(Project model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.ProjectDelete + model.Id.ToString();

					var response = await client.DeleteAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> AddView(Project model)
			{
				try
				{
					string route = Routes.ProjectView + model.Id.ToString();

					var response = await client.PutAsync(route, null);
					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					Debug.WriteLine(e);
					return new HttpResult(false, e.Message);
				}
			}
		}

		internal class Subscriptions
		{
			static Subscriptions() => EnsureTokenExists();

			public static async Task<bool> IsSubscribedToForum(string forumId)
			{
				try
				{
					string route = Routes.ForumSubscribed + forumId;
					HttpResponseMessage response = await client.GetAsync(route);
					return response.StatusCode == System.Net.HttpStatusCode.OK;
				}
				catch (Exception)
				{
					Alert.Send("Error de connexió");
					return false;
				}
			}
			public static async Task<HttpResult> IsSubscribedToForum(Guid forumId)
			{
				try
				{
					string route = Routes.AccountIsSubscribedToForum + forumId.ToString();

					var response = await client.GetAsync(route);

					string message = await response.Content.ReadAsStringAsync();

					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<HttpResult> SubscribeToForum(Forum model)
			{
				try
				{
					string modelString = JsonService.Serialize(model);
					string route = Routes.SubscriptionSubscribeToForum;

					var response = await client.PostAsync(route, GetStringContent(modelString));
					string message = await response.Content.ReadAsStringAsync();

					return await CheckHttpResultResponse(response);
				}
				catch (Exception e)
				{
					return new HttpResult(false, e.Message);
				}
			}
			public static async Task<List<Forum>> Forums()
			{
				try
				{
					EnsureTokenExists();

					User user = Settings.GetLoggedUser();
					string route = Routes.AccountSubscribedForumsByUserId + user.Id.ToString();

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
							response = await client.GetAsync(Routes.ForumSubscribed + user.Id.ToString());
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
				catch (Exception)
				{
					Alert.Send("Error de connexió");
					return new List<Forum>();
				}
			}
			public static async Task<List<Forum>> Forums(Guid userId)
			{
				try
				{
					EnsureTokenExists();
					string route = Routes.AccountGetSubscribedForums + userId.ToString();
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
				catch (Exception)
				{
					Alert.Send("Error de connexió");
					return new List<Forum>();
				}
			}
			public static async Task<List<Bill>> Bills(Guid forumId)
			{
				try
				{
					string route = Routes.SubscriptionBills + forumId.ToString();

					var response = await client.GetAsync(route);

					return await CheckBillListResponse(response);
				}
				catch (Exception e)
				{
					return new List<Bill>();
				}
			}
		}

		internal class Transactions
		{

		}

		#region Projects
		public static async Task<List<Project>> RequestPersonalProjects(Guid userId)
		{
			try
			{
				EnsureTokenExists();

				string route = Routes.ProjectGetPersonal + userId.ToString();
				HttpResponseMessage response = await client.GetAsync(route);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string forumsString = await response.Content.ReadAsStringAsync();
					return JsonService.Deserialize<List<Project>>(forumsString);
				}
				return null;
			}
			catch (Exception)
			{
				Alert.Send("Error de connexió");
				return new List<Project>();
			}
		}

		public static async Task<List<Project>> RequestParticipatingProjects(Guid userId)
		{
			return new List<Project>();
		}

		public static async Task<List<Bill>> RequestProjectBills(Guid forumId)
		{
			try
			{
				string route = Routes.ProjectBills + forumId.ToString();
				HttpResponseMessage response = await client.GetAsync(route);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					string billsString = await response.Content.ReadAsStringAsync();
					return JsonService.Deserialize<List<Bill>>(billsString);
				}

				return new List<Bill>();
			}
			catch (Exception)
			{
				Alert.Send("Error de connexió");
				return new List<Bill>();
			}
		}

		public static async Task<bool> VoteProject(VoteViewModel model)
		{
			try
			{
				EnsureTokenExists();
				string route = Routes.ProjectVote;
				string modelString = JsonService.Serialize(model);

				HttpResponseMessage response = await client.PostAsync(route, GetStringContent(modelString));

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return true;
				}
				else
				{
					string message = await response.Content.ReadAsStringAsync();
					for (int i = 0; i < 3; i++)
					{
						response = await client.PostAsync(route, GetStringContent(modelString));
						if (response.StatusCode == System.Net.HttpStatusCode.OK)
						{
							return true;
						}
					}

					return false;
				}
			}
			catch (Exception)
			{
				Alert.Send("Error de connexio");
				return false;
			}
		}
		#endregion

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

				var result = await Account.Login(model);

				if (!result.IsSuccess)
				{
					for (int i = 0; i < 3; i++)
					{
						result = await Account.Login(model);
						if (result.IsSuccess) break;
					}
				}
			}
		}

	}
}
