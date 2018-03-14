using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Public.Projects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTabbedPage : TabbedPage
    {
		private User Model = new User();

        public ProfileTabbedPage(User model)
        {
            InitializeComponent();
			Model = model;
			Title = $"{Model.Name} {Model.Surname}";

			Children.Add(new ProfileDetailPage(Model) { Title = "Informació" });
			Children.Add(new ForumPage(new ForumViewModel(Origin.User, Order.Title) { UserId = Model.Id }) { Title = "Fòrums" });
			Children.Add(new ProjectPage(new ProjectViewModel(Origin.User, Order.Title) { UserId = Model.Id }) { Title = "Projectes" });
		}
    }
}