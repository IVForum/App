using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectCreatePage : ContentPage
	{
		private Project Model;

		public ProjectCreatePage()
		{
			InitializeComponent();
		}

		private async void Add(object sender, EventArgs e)
		{
			Model = new Project
			{
				Id = Guid.NewGuid(),
				Title = TitleEntry.Text,
				Description = DescriptionEntry.Text,
				CreationDate = DateTime.Now,
				WebsiteUrl = WebsiteEntry.Text,
				RepositoryUrl = RepositoryEntry.Text
			};

			var result = await ApiService.Projects.Create(Model);

			if (result.IsSuccess)
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