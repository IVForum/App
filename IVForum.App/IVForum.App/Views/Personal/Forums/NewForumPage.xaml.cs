using IVForum.App.ViewModels;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewForumPage : ContentPage
	{
		public CreateNewViewModel Model = new CreateNewViewModel();
		public NewForumPage()
		{
			InitializeComponent();
			BindingContext = Model;
		}

		async void Add(object sender, EventArgs e)
		{

			await Navigation.PopAsync();
		}

		async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}