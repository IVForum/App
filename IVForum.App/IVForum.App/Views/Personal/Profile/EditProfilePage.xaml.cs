using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Profile
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

		private async void SaveChanges(object sender, EventArgs args)
		{
			try
			{
				ValidateSavedModel();

				var result = await ApiService.Account.Update(Model);

				if (result.IsSuccess)
				{
					Alert.Send("Dades desades");
					string modelString = JsonService.Serialize(Model);
					Settings.Save("user", modelString);
				}
				else
				{
					Alert.Send("Error al desar les dades");
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				Alert.Send("Error al desar les dades");
			}
		}

		private async void Discard(object sender, EventArgs args)
		{
			var response = await DisplayAlert("Avís", "Descartar els canvis fets?", "Si", "No");

			if (response)
			{
				await Navigation.PopAsync();
			}
		}
	}
}
