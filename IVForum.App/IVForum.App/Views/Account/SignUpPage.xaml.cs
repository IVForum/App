using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Shared;

using System;
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
			BindingContext = model = new SignUpViewModel
			{
				Name = "Nom",
				Surname = "Cognom",
				Email = "Correu electrònic",
				Password = "Contrasenya",
				ValidatePassword = "Confirmar contrasenya",
				Legal = Properties.Res.Legal
			};
		}

		async void SignUp(object sender, EventArgs e)
		{
			try
			{
				await Navigation.PushModalAsync(new LoadingPage(), false);

				Regex regexEmail = new Regex("^[a-z0-9._%+-]+@[a-z0-9.-]+[^\\.]\\.[a-z]{2,3}$");

				if (!regexEmail.IsMatch(EntryEmail.Text))
				{
					await DisplayAlert("Error", "Correu electrònic invàlid", "Ok");
					await Navigation.PopModalAsync(false);
					return;
				}

				Regex regexPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*(_|[^\\w])).+$");

				if (!regexPassword.IsMatch(EntryPassword.Text))
				{
					await DisplayAlert("Error", "Format de la contrasenya incorrecte: Mínim 8 caràcters amb majúscules, minúscules, un número i un caràcter especial", "Ok");
					await Navigation.PopModalAsync(false);
					return;
				}

				if (EntryPassword.Text != EntryValidatePassword.Text)
				{
					await DisplayAlert("Error", "Les contrasenyes no són iguals", "Ok");
					await Navigation.PopModalAsync(false);
					return;
				}

				// TODO: Regex
				model = new SignUpViewModel
				{
					Name = EntryName.Text,
					Surname = EntrySurname.Text,
					Email = EntryEmail.Text,
					Password = EntryPassword.Text
				};

				var success = await ApiService.RequestSignUp(model);

				await Task.Delay(1000);

				if (success)
				{
					Application.Current.MainPage = new Main.Main();
					//await DisplayAlert("Èxit", "L'usuari s'ha creat amb èxit", "Ok");
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
			catch
			{
				await Navigation.PopModalAsync(false);
				await DisplayAlert("Error", "Error al registrar l'usuari, torna a provar més tard", "Ok");
			}
		}
	}
}