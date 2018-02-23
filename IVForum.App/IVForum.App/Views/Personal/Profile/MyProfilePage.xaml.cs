using IVForum.App.Models;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyProfilePage : ContentPage
	{
		private ProfileViewModel model;
		public MyProfilePage ()
		{
			InitializeComponent ();
			BindingContext = model = new ProfileViewModel(new User {
				Name = "Cristian",
				Surname = "Moraru",
				Email = "cristian.moraru@live.com",
				RepositoryUrl = "http://www.github.com/Flysenberg"
			});
		}

		public MyProfilePage(User user)
		{
			InitializeComponent();
			BindingContext = model = new ProfileViewModel(new User
			{
				Name = "Cristian",
				Surname = "Moraru",
				Email = "cristian.moraru@live.com",
				RepositoryUrl = "http://www.github.com/Flysenberg"
			});
		}

		async void OnItemSelected(object sender, ItemTappedEventArgs e)
		{
			await DisplayAlert("Tap detected", $"You tapped {sender}", "Ok");
		}
	}
}