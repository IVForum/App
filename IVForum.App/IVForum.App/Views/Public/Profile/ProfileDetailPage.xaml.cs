using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels.Static;
using IVForum.App.Views.Personal.Profile;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileDetailPage : ContentPage
	{
		private User Model { get; set; }

		public ProfileDetailPage()
		{
			InitializeComponent();
			Model = Settings.GetLoggedUser();
			BindingContext = Model;
			Title = Model.Name + " " + Model.Surname;
			Load();
			Load(Model);
		}

		public ProfileDetailPage(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Title = model.Name + " " + model.Surname;
			Load(model);
		}

		private async void Load()
		{
			ToolbarItem edit = new ToolbarItem
			{
				Text = "Editar",
				Icon = "edit_w.png"
			};

			edit.Clicked += Edit_Clicked;
			ToolbarItems.Add(edit);
		}

		private async void Load(User model)
		{
			if (model.Description != null)
			{
				ProfileLayout.Children.Add(InfoFrame.Create("info.png", "Descripció", model.Description));
			}

			if (model.RepositoryUrl != null)
			{
				ProfileLayout.Children.Add(InfoFrame.Create("repo.png", "Repositori", model.RepositoryUrl));
			}

			if (model.WebsiteUrl != null)
			{
				ProfileLayout.Children.Add(InfoFrame.Create("web.png", "Pàgina web", model.WebsiteUrl));
			}

			if (model.FacebookUrl != null)
			{
				ProfileLayout.Children.Add(InfoFrame.Create("facebook.png", "Facebook", model.FacebookUrl));
			}

			if (model.TwitterUrl != null)
			{
				ProfileLayout.Children.Add(InfoFrame.Create("twitter.png", "twitter", model.TwitterUrl));
			}
		}

		private async void Edit_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EditProfilePage(Model), true);
		}

		private async void LabelTapped(object sender, EventArgs args)
		{

		}
	}
}