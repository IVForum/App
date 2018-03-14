using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Shared;

using System;
using System.Diagnostics;

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

		async void Login(object sender, EventArgs args)
		{
			Button btn = sender as Button;

			try
			{
				btn.IsEnabled = false;
				await Navigation.PushModalAsync(new LoadingPage(), false);

				// TODO: Regex
				model = new LoginViewModel
				{
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var result = await AccountService.Login(model);

				if (result.IsSuccess)
				{
					Application.Current.MainPage = new Main.Main();
					Settings.Save("loggedin", true);

					User user = Settings.GetLoggedUser();

					Alert.Send($"Benvingut {user.Name}");
				}
				else
				{
					await Navigation.PopModalAsync(false);
					Alert.Send(result.Message);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				await Navigation.PopModalAsync(false);
				Alert.Send("Error al iniciar sessió");
			}
			finally
			{
				btn.IsEnabled = true;
			}
		}
	}
}