using IVForum.App.Models;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserProfileView : ContentView
	{
		public ProfileViewModel Model { get; set; }
		public UserProfileView()
		{
			InitializeComponent();
			BindingContext = Model = new ProfileViewModel(new User { Name = "Cristian", Surname = "Moraru", Avatar = "avatar.png" });
		}

		public UserProfileView(User user)
		{
			InitializeComponent();
			BindingContext = Model = new ProfileViewModel(user);
		}
	}
}