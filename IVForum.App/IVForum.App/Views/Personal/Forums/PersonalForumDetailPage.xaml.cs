using IVForum.App.ViewModels.Personal.Forums;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalForumDetailPage : ContentPage
	{
		public PersonalForumViewModel Model { get; set; }
		public PersonalForumDetailPage(PersonalForumViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = Model = viewModel;
		}

		public async void DeleteForum(object sender, EventArgs e)
		{
			var result = await DisplayAlert("Avís", "Segur vols esborrar aquest fòrum?", "Si", "No");
			if (result)
			{
				// Delete forum from list and send request to API.

				await Navigation.PopToRootAsync();
			}
		}
	}
}