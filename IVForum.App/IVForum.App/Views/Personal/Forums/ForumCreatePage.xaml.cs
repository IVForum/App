
using IVForum.App.Services;
using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumCreatePage : ContentPage
	{
		private CreateNewViewModel Model = new CreateNewViewModel();

		public ForumCreatePage()
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

			var result = await ApiService.CreateForum(Model);

			if (result)
			{
				Alert.Send("Fòrum afegit correctament");
				await Navigation.PopToRootAsync(true);
			}
			else
			{
				Alert.Send("Error al afegir el fòrum");
			}
		}

		private async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync(true);
		}
	}
}