using IVForum.App.ViewModels;

using Newtonsoft.Json;

using System.Net.Http;
using System.Threading.Tasks;

namespace IVForum.App.Services
{
	public class ApiService
    {
		private static HttpClient client;

		static ApiService()
		{
			client = new HttpClient
			{
				BaseAddress = new System.Uri("http://192.168.137.138:57571/api/")
			};
		}

		public static async Task<bool> RegisterUser(SignUpViewModel sender)
		{
			string modelString = JsonConvert.SerializeObject(sender, Formatting.Indented);

			//HttpResponseMessage response = await client.PostAsync("accounts/register", new StringContent(modelString, Encoding.UTF8, "application/json"));
			//return response.StatusCode == HttpStatusCode.Created;

			return true;
		}

		public static async Task<bool> Login(SignUpViewModel sender)
		{
			LoginViewModel model = GetLoginViewModel(sender);

			//string modelString = JsonConvert.SerializeObject(model, Formatting.Indented);
			//HttpResponseMessage response = await client.PostAsync("accounts/login", new StringContent(modelString, Encoding.UTF8, "application/json"));

			return true;
		}

		public static async Task<bool> Login(LoginViewModel sender)
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
	}
}
