using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewProjectPage : ContentPage
	{
		public CreateNewViewModel Model = new CreateNewViewModel();
		public NewProjectPage()
		{
			InitializeComponent();
			BindingContext = Model;
		}

		async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}