using IVForum.App.Models;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
		public List<Project> Models { get; set; } //= IVForum.App.Resources.Content.GetProjects(); 

        public ProjectTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ProjectPage(Models.OrderBy(x => x.Views)) { Title = "Top", BackgroundColor = Color.GhostWhite });
			Children.Add(new ProjectPage(Models.OrderBy(x => x.Bills.Count)) { Title = "Popular", BackgroundColor = Color.GhostWhite });
			Children.Add(new ProjectPage(Models.OrderBy(x => x.CreationDate)) { Title = "Nous", BackgroundColor = Color.GhostWhite });
        }
    }
}