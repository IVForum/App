using IVForum.App.Models;
using IVForum.App.Services;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		public List<Forum> Forums { get; set; } = new List<Forum>();

		public ForumTabbedPage()
        {
            InitializeComponent();
			Load();
        }

		private async void Load()
		{
			Forums = await ApiService.RequestForums();

			if (Forums != null)
			{
				Children.Add(new ForumPage(Forums.OrderBy(x => x.Views)) { Title = "Top", BackgroundColor = Color.GhostWhite });
				Children.Add(new ForumPage(Forums.OrderBy(x => x.Projects.Count)) { Title = "Popular", BackgroundColor = Color.GhostWhite });
				Children.Add(new ForumPage(Forums.OrderByDescending(x => x.CreationDate)) { Title = "Nous", BackgroundColor = Color.GhostWhite });
			}
		}

		//private async void Search(object sender, EventArgs e)
		//{
		//	await Navigation.PushModalAsync(new SearchPage(), true);
		//}
    }
}