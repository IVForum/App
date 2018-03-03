using IVForum.App.Models;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Public.Projects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTabbedPage : TabbedPage
    {
        public ProfileTabbedPage(User model)
        {
            InitializeComponent();

			Title = model.Name + " " + model.Surname;

			Children.Add(new ProfilePage(model) { Title = "Informació" });
			Children.Add(new ForumPage(model.Forums) { Title = "Fòrums" });
			Children.Add(new ProjectPage(model.Projects) { Title = "Projectes" });
        }
    }
}