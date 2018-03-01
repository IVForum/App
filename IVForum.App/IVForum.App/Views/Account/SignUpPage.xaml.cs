using IVForum.App.Services;
using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		public SignUpViewModel model;
		public SignUpPage()
		{
			InitializeComponent();
			BindingContext = model = new SignUpViewModel
			{
				Name = "Nom",
				Surname = "Cognom",
				Email = "Correu electrònic",
				Password = "Contrasenya",
				Legal = Properties.Res.Legal
			};
		}

		async void SignUp(object sender, EventArgs e)
		{
			try
			{
				LoadingActivity.IsRunning = true;

				// TODO: Regex
				model = new SignUpViewModel
				{
					Name = EntryName.Text,
					Surname = EntrySurname.Text,
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var success = await ApiService.RequestSignUp(model);

				if (success)
				{
					LoadingActivity.IsRunning = false;

					await DisplayAlert("Èxit", "L'usuari s'ha creat amb èxit", "Ok");

					Application.Current.MainPage = new Main.Main();
				}
				else
				{
					await DisplayAlert("Error", "Error al registrar l'usuari, torna a provar més tard", "Ok");
				}
			}
			catch
			{
				await DisplayAlert("Error", "Error al registrar l'usuari, torna a provar més tard", "Ok");
			}
		}
	}
}