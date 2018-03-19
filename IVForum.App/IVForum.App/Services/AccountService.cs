using IVForum.App.Data.Models;
using IVForum.App.Data.Shared;
using IVForum.App.Properties;
using IVForum.App.ViewModels;

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IVForum.App.Services
{
	public class AccountService
    {
		private const string MediaType = "application/json";
		private static HttpClient client = new HttpClient() { BaseAddress = new Uri(Routes.Base) };
		private static void Authorize(Token token)
		{
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Auth_Token);
		}

		public static async Task<HttpResult> Login(LoginViewModel model)
		{
			string route = Routes.AccountLogin;
			string modelString = JsonService.Serialize(model);

			HttpResponseMessage response = await client.PostAsync(route, GetStringContent(modelString));

			if (response.StatusCode == HttpStatusCode.OK)
			{
				string tokenString = await response.Content.ReadAsStringAsync();
				Settings.Save("token", tokenString);

				Token token = JsonService.Deserialize<Token>(tokenString);
				Authorize(token);
				var result = await GetUserDetails(token.Id);

				if (result.IsSuccess)
				{
					return new HttpResult(true, response.StatusCode);
				}

				return new HttpResult(false, "Failed to retrieve user details");
			}
			string message = await response.Content.ReadAsStringAsync();

			return new HttpResult(false, response.StatusCode, await response.Content.ReadAsStringAsync());
		}
		public static async Task<HttpResult> Login(SignUpViewModel model)
		{
			return await Login(new LoginViewModel() { Email = model.Email, Password = model.Password });
		}
		public static async Task<HttpResult> Login(User model)
		{
			return await Login(new LoginViewModel() { Email = model.Email, Password = model.Password });
		}

		private static StringContent GetStringContent(string model)
		{
			return new StringContent(model, Encoding.UTF8, MediaType);
		}
		private static async Task<HttpResult> GetUserDetails(string userId)
		{
			try
			{
				string route = Routes.AccountGetUserById + userId;

				HttpResponseMessage response = await client.GetAsync(route);

				if (response.StatusCode == HttpStatusCode.OK)
				{
					string userString = await response.Content.ReadAsStringAsync();
					Settings.Save("user", userString);
					return new HttpResult(true, response.StatusCode);
				}

				string message = await response.Content.ReadAsStringAsync();

				return new HttpResult(false, response.StatusCode, await response.Content.ReadAsStringAsync());
			}
			catch (Exception e)
			{
				return new HttpResult(false, e.Message);
			}
		}
		private static async Task<HttpResult> GetUserDetails(Guid userId)
		{
			return await GetUserDetails(userId.ToString());
		}
		private static async Task<HttpResult> GetUserDetails(User user)
		{
			return await GetUserDetails(user.Id.ToString());
		}
	}
}
