using IVForum.App.Services;
using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectCreatePage : ContentPage
	{
		private CreateNewViewModel Model;

		public ProjectCreatePage()
		{
			InitializeComponent();
		}

		private async void Add(object sender, EventArgs e)
		{
			Model = new CreateNewViewModel
			{
				Name = NameEntry.Text,
				Title = TitleEntry.Text,
				Description = DescriptionEntry.Text
			};

			var result = await ApiService.CreateProject(Model);

			if (result)
			{
				Alert.Send("Projecte afegit correctament");
				await Navigation.PopToRootAsync(true);
			}
			else
			{
				Alert.Send("Error al afegir el projecte");
			}
		}

		private async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopToRootAsync(true);
		}
	}
}