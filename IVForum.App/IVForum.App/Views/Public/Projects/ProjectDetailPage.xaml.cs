using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.Views.Public.Profile;
using IVForum.App.Views.Shared;

using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectDetailPage : ContentPage
	{
		private Project Model { get; set; } = new Project();

		public ProjectDetailPage(Project model)
		{
			InitializeComponent();
			BindingContext = Model = model;

			Title = model.Title;

			User user = Settings.GetLoggedUser();

			//if (user.Id == model.Owner.Id)
			//{
				Button delete = new Button
				{
					Image = "cross.png",
					Text = "Eliminar",
					BackgroundColor = Color.Red
				};
				delete.Clicked += Delete_Clicked;

				ProjectStackLayout.Children.Add(delete);
			//}
		}

		private async void Delete_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Eliminar aquest projecte permanentment?", "Si", "No");

			if (!response)
				return;

			await Navigation.PushModalAsync(new LoadingPage(), false);

			var result = true; //await ApiService.DeleteProject(Model);

			if (result)
			{
				await Task.Delay(500);

				Alert.Send("Project eliminat correctament");

				await Navigation.PopModalAsync(true);
				await Navigation.PopToRootAsync(false);
			}
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}