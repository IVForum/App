using IVForum.App.Models;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		public List<Forum> Forums { get; set; } = IVForum.App.Resources.Content.GetForums();

        public ForumTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ForumPage(Forums.OrderBy(x => x.Views)) { Title = "Top", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(Forums.OrderBy(x => x.Projects.Count)) { Title = "Popular", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(Forums.OrderBy(x => x.CreationDate)) { Title = "Nous", BackgroundColor = Color.GhostWhite });
        }
    }
}