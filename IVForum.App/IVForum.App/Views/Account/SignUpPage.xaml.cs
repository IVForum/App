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
				Password = "Contrasenya"
			};
		}

		async void SignUp(object sender, EventArgs e)
		{
			model = new SignUpViewModel
			{
				Name = EntryName.Text,
				Surname = EntrySurname.Text,
				Email = EntryEmail.Text,
				Password = EntryPassword.Text
			};

			var result = await ApiService.RegisterUser(model);

			if (!result)
			{
				await DisplayAlert("Error", "Failed to register user, try again later", "Ok");
			}
			else
			{
				await DisplayAlert("Success", "Account created successfully", "Ok");
				Settings.Save("signedup", true);
				await Navigation.PopAsync(true);
			}
		}
	}
}