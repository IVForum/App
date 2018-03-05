using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Public.Projects;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Profile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileTabbedPage : TabbedPage
    {
		private User Model = new User();
		private List<Forum> ModelForums = new List<Forum>();
		private List<Project> ModelProjects = new List<Project>();

        public ProfileTabbedPage(User model)
        {
            InitializeComponent();
			Load(model);	
        }

		private async void Load(User model)
		{
			Model = await ApiService.RequestUserDetails(model.Id);
			Title = $"{Model.Name} {Model.Surname}";
			Children.Add(new ProfilePage(Model) { Title = "Informació" });

			ModelForums = await ApiService.RequestForums(Model.Id);
			Children.Add(new ForumPage(ModelForums) { Title = "Fòrums" });

			ModelProjects = await ApiService.RequestProjects(Model.Id);
			Children.Add(new ProjectPage(ModelProjects) { Title = "Projectes" });
		}
    }
}