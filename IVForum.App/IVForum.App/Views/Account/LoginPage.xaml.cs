using IVForum.App.Services;
using IVForum.App.ViewModels;

using System;

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
				LoadingActivity.IsRunning = true;

				// TODO: Regex
				model = new LoginViewModel
				{
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var success = true;//await ApiService.RequestLogin(model);

				if (success)
				{
					Settings.Save("loggedin", true);
					LoadingActivity.IsRunning = false;
					Application.Current.MainPage = new Main.Main();
				}
				else
				{
					await DisplayAlert("Error", "Error al iniciar sessió", "Ok");
				}
			}
			catch
			{
				await DisplayAlert("Error", "Error al iniciar sessió", "Ok");
			}
		}
	}
}