using IVForum.App.Models;
using IVForum.App.Properties;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Shared;

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
			BindingContext = this;
		}

		private async void SignUp(object sender, EventArgs e)
		{
			try
			{
				await Navigation.PushModalAsync(new LoadingPage(), false);

				bool valid = await Validate();

				if (!valid)
					return;

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
					Application.Current.MainPage = new Main.Main();
					Settings.Save("loggedin", true);

					User user = Settings.GetLoggedUser();
					DependencyService.Get<IMessage>().LongAlert($"Benvingut {user.Name}");
				}
				else
				{
					await Navigation.PopModalAsync(false);
					await DisplayAlert("Error", "Error al registrar l'usuari, torna a provar més tard", "Ok");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Navigation.PopModalAsync(false);
				await DisplayAlert("Error", "Error al registrar l'usuari, torna a provar més tard", "Ok");
			}
		}

		private async Task<bool> Validate()
		{
			Regex regexEmail = new Regex("^[a-z0-9._%+-]+@[a-z0-9.-]+[^\\.]\\.[a-z]{2,3}$");

			if (!regexEmail.IsMatch(EntryEmail.Text))
			{
				await DisplayAlert("Error", "Correu electrònic invàlid", "Ok");
				await Navigation.PopModalAsync(false);
				return false;
			}

			Regex regexPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*(_|[^\\w])).+$");

			if (!regexPassword.IsMatch(EntryPassword.Text))
			{
				await DisplayAlert("Error", "Format de la contrasenya incorrecte: Mínim 8 caràcters amb majúscules, minúscules, un número i un caràcter especial", "Ok");
				await Navigation.PopModalAsync(false);
				return false;
			}

			if (EntryPassword.Text != EntryValidatePassword.Text)
			{
				await DisplayAlert("Error", "Les contrasenyes no són iguals", "Ok");
				await Navigation.PopModalAsync(false);
				return false;
			}

			return true;
		}
	}
}