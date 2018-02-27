using IVForum.App.Models;
using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyProfilePage : ContentPage
	{
		private ProfileViewModel Model;
		public MyProfilePage ()
		{
			InitializeComponent ();
			BindingContext = Model = new ProfileViewModel(new User {
				Name = "Cristian",
				Surname = "Moraru",
				Email = "cristian.moraru@live.com",
				Avatar = "avatar.png",
				RepositoryUrl = "http://www.github.com/Flysenberg",
				WebsiteUrl = "http://www.cristian.moraru.com",
				FacebookUrl = "http://www.facebook.com/CristianMoraru"
			});
		}

		public MyProfilePage(User user)
		{
			InitializeComponent();
			BindingContext = Model = new ProfileViewModel(new User
			{
				Name = "Cristian",
				Surname = "Moraru",
				Email = "cristian.moraru@live.com",
				Avatar = "avatar.png",
				RepositoryUrl = "http://www.github.com/Flysenberg",
				WebsiteUrl = "http://www.cristian.moraru.com",
				FacebookUrl = "http://www.facebook.com/CristianMoraru"
			});
		}

		private async void DeleteAccount(object sender, EventArgs e)
		{
			var result = await DisplayAlert("Avís", "Segut vols eliminar el perfile, no es pot tornar enrere", "Si", "No");
			if (result)
			{
				var confirm = await DisplayAlert("Avís", "Segur?", "Si", "No");

				if (confirm)
				{
					// TODO: Delete account from API;

					await DisplayAlert("Avís", "Perfil esborrat amb èxit", "Ok");

					//Application.Current.MainPage = new StartupTabbedPage();
				}
			}
		}
	}
}