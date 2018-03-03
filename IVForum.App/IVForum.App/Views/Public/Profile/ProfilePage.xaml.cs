using IVForum.App.Models;
using IVForum.App.Views.Shared;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private User Model { get; set; }

		public ProfilePage(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Title = model.Name + " " + model.Surname;
		}

		public ProfilePage()
		{
			InitializeComponent();

			//Model = Settings.GetLoggedUser();
			Model = IVForum.App.Resources.Content.Cristian;

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

		private async void ShowFacebook(object sender, EventArgs e)
		{
			
		}

		private async void ShowTwitter(object sender, EventArgs e)
		{
			
		}
	}
}