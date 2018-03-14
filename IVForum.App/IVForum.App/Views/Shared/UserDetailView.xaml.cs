using IVForum.App.Data.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserDetailView : ContentView
	{
		private User Model;

		public UserDetailView(User model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}
	}
}