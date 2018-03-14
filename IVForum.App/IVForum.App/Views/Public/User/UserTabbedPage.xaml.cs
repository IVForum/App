using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : TabbedPage
    {
		private UserViewModel Model;

        public UserTabbedPage()
        {
            InitializeComponent();
        }

		public UserTabbedPage(UserViewModel model)
		{
			Model = model;
		}
    }
}