using IVForum.App.Models;
using IVForum.App.Views.Public.Profile;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectDetailPage : ContentPage
	{
		private Project Model;

		public ProjectDetailPage(Project model)
		{
			InitializeComponent();
			BindingContext = Model = model;

			Title = model.Title;
			BackgroundColor = Color.GhostWhite;
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}