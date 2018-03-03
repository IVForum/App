using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Shared;

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		private LoginViewModel model;

		public LoginPage()
		{
			InitializeComponent();
			BindingContext = model = new LoginViewModel
			{
				Email = "Correu electrònic",
				Password = "Contrasenya"
			};
		}

		public LoginPage(SignUpViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = model = new LoginViewModel
			{
				Email = viewModel.Email,
				Password = viewModel.Password
			};
		}

		async void Login(object sender, EventArgs e)
		{
			try
			{
				await Navigation.PushModalAsync(new LoadingPage(), false);

				// TODO: Regex
				model = new LoginViewModel
				{
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var success = await ApiService.RequestLogin(model);

				await Task.Delay(1000);

				if (success)
				{
					Application.Current.MainPage = new Main.Main();
					Settings.Save("loggedin", true);
					
					User user = Settings.GetLoggedUser();

					DependencyService.Get<IMessage>().LongAlert($"Benvingut {user.Name}");
				}
				else
				{
					await Navigation.PopModalAsync();
					await DisplayAlert("Error", "Error al iniciar sessió", "Ok");
				}
			}
			catch
			{
				await Navigation.PopModalAsync();
				await DisplayAlert("Error", "Error al iniciar sessió", "Ok");
			}
		}
	}
}