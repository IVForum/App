using IVForum.App.Models;
using IVForum.App.Views.Public.Projects;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
		public List<Project> Models { get; set; } = IVForum.App.Resources.Content.GetProjects();

        public ProjectTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ProjectPage(Models) { Title = "Personals", BackgroundColor = Color.GhostWhite });
			Children.Add(new ProjectPage(Models) { Title = "Participants", BackgroundColor = Color.GhostWhite });
        }
    }
}