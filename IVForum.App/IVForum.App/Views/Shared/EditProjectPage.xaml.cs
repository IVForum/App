using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProjectPage : ContentPage
	{
		private Project Model { get; set; } = new Project();

		public EditProjectPage(Project model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}

		private async void SaveChanges(object sender, EventArgs e)
		{
			var result = true; //await ApiService.UpdateProject(Model);

			if (result)
			{
				Alert.Send("Dades desades");
			}
			else
			{
				Alert.Send("Error al desar les dades");
			}
		}

		private async void Discard(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Descartar canvis?", "Si", "No");

			if (response)
			{
				await Navigation.PopToRootAsync(true);
			}
		}
	}
}