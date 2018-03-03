using IVForum.App.Models;
using IVForum.App.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProfilePage : ContentPage
	{
		private User Model { get; set; } = new User();

		public EditProfilePage(User model)
		{
			InitializeComponent();
			ValidateModel();

			Title = "Editar perfil";
			BindingContext = Model = model;
		}

		private void ValidateModel()
		{
			if (Model.Description is null)
			{
				Model.Description = "Descripció breu";
			}
			if (Model.WebsiteUrl is null)
			{
				Model.WebsiteUrl = "Pàgina web";
			}
			if (Model.FacebookUrl is null)
			{
				Model.FacebookUrl = "Facebook";
			}
			if (Model.TwitterUrl is null)
			{
				Model.TwitterUrl = "Twitter";
			}
			if (Model.RepositoryUrl is null)
			{
				Model.RepositoryUrl = "Repositori";
			}
		}

		private void ValidateSavedModel()
		{
			if (Model.Description == "Descripció breu")
			{
				Model.Description = null;
			}
			if (Model.WebsiteUrl == "Pàgina web")
			{
				Model.WebsiteUrl = null;
			}
			if (Model.FacebookUrl == "Facebook")
			{
				Model.FacebookUrl = null;
			}
			if (Model.TwitterUrl == "Twitter")
			{
				Model.TwitterUrl = null;
			}
			if (Model.RepositoryUrl == "Repositori")
			{
				Model.RepositoryUrl = null;
			}
		}

		private async void SaveChanges(object sender, EventArgs e)
		{
			try
			{
				ValidateSavedModel();

				var result = await ApiService.UpdateUser(Model);

				if (result)
				{
					DependencyService.Get<IMessage>().ShortAlert("Dades desades correctament");
				}
				else
				{
					await DisplayAlert("Error", "Hi ha hagut un error a l'hora de desar les dades", "Ok");
				}
			}
			catch
			{
				await DisplayAlert("Error", "Hi ha hagut un error a l'hora de desar les dades", "Ok");
			}
		}

		private async void Discard(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Descartar els canvis fets?", "Si", "No");

			if (response)
			{
				await Navigation.PopAsync();
			}
		}
	}
}
