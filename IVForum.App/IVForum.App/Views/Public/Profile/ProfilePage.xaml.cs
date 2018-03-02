using IVForum.App.Models;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		public ProfilePage(User model)
		{
			InitializeComponent();
			BindingContext = model;
		}

		private async void ShowFacebook(object sender, EventArgs e)
		{
			
		}

		private async void ShowTwitter(object sender, EventArgs e)
		{
			
		}
	}
}