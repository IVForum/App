using IVForum.App.Models;
using IVForum.App.Services;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
		public List<Project> Projects { get; set; } = new List<Project>();

        public ProjectTabbedPage()
        {
            InitializeComponent();
			Load();
        }

		private async void Load()
		{
			Projects = await ApiService.RequestProjects();

			if (Projects != null)
			{
				Children.Add(new ProjectPage(Projects.OrderBy(x => x.Views)) { Title = "Top", BackgroundColor = Color.GhostWhite });
				Children.Add(new ProjectPage(Projects.OrderBy(x => x.Bills.Count)) { Title = "Popular", BackgroundColor = Color.GhostWhite });
				Children.Add(new ProjectPage(Projects.OrderByDescending(x => x.CreationDate)) { Title = "Nous", BackgroundColor = Color.GhostWhite });
			}
		}
    }
}