using IVForum.App.Data.Models;
using IVForum.App.Services;
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
			Load();
		}

		public ProfileDetailPage(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Title = model.Name + " " + model.Surname;
		}

		private async void Load()
		{
			Model = Settings.GetLoggedUser();

			BindingContext = Model;
			Title = Model.Name + " " + Model.Surname;

			ToolbarItem edit = new ToolbarItem
			{
				Text = "Editar",
				Icon = "edit_w.png"
			};

			edit.Clicked += Edit_Clicked;

			ToolbarItems.Add(edit);
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