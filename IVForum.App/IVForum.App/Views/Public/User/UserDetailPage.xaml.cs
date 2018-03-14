using IVForum.App.Data.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetailPage : ContentPage
	{
		private User Model;

		public UserDetailPage(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}
	}
}