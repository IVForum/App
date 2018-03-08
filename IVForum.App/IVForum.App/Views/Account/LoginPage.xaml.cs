using IVForum.App.Data.Models;
using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Shared;

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
				await Navigation.PushModalAsync(new LoadingPage(), false);

				// TODO: Regex
				model = new LoginViewModel
				{
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var result = await ApiService.Account.Login(model);

				if (result.IsSuccess)
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
			catch (Exception ex)
			{
				await Navigation.PopModalAsync();
				await DisplayAlert("Error", ex.Message, "Ok");
			}
		}
	}
}