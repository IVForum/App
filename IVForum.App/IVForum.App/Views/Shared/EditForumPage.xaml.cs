using IVForum.App.Data.Models;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditForumPage : ContentPage
	{
		private Forum Model { get; set; } = new Forum();

		public EditForumPage(Forum model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}

		private async void SaveChanges(object sender, EventArgs e)
		{

		}

		private async void Discard(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Descartar canvis fets?", "Si", "No");

			if (response)
			{
				await Navigation.PopToRootAsync(true);
			}
		}
	}
}