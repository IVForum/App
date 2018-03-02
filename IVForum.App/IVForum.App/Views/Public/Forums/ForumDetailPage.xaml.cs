using IVForum.App.Models;
using IVForum.App.Views.Public.Profile;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailPage : ContentPage
	{
		private Forum Model;

		public ForumDetailPage(Forum model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfilePage(Model.Owner), true);
		}
	}
}