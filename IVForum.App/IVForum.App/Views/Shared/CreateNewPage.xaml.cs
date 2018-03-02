
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNewPage : ContentPage
	{
		internal class CreateNewViewModel
		{
			public string Name { get; set; }
			public string Title { get; set; }
			public string Description { get; set; }
		}

		private CreateNewViewModel Model { get; set; } = new CreateNewViewModel() { Name = "Nom", Title = "Títol", Description = "Descripció" };

		public CreateNewPage()
		{
			InitializeComponent();
			BindingContext = Model;
		}

		public async void Add(object sender, EventArgs e)
		{
			await DisplayAlert("Afegir", $"{NameEntry.Text}, {TitleEntry.Text}, {DescriptionEntry.Text}", "Ok");
		}

		public async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopToRootAsync(true);
		}
	}
}