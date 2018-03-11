using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.Views.Personal.Profile;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private User Model { get; set; } = new User();

		public ProfilePage(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Title = model.Name + " " + model.Surname;
		}

		public ProfilePage()
		{
			InitializeComponent();
			Load();
		}

		private async void Load()
		{
			Model = await ApiService.Account.RequestUserDetails(Settings.GetLoggedUser().Id.ToString());

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
	}
}