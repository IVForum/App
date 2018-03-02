using IVForum.App.Models;
using IVForum.App.Views.Public.Forums;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		private List<Forum> Models = IVForum.App.Resources.Content.GetForums();

        public ForumTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ForumPage(Models) { Title = "Personals", BackgroundColor = Color.GhostWhite });
        }
    }
}