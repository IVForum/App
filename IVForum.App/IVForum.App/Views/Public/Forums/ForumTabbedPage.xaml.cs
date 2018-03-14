using IVForum.App.Data.Enums;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		public ForumTabbedPage()
        {
            InitializeComponent();
			
			Children.Add(new ForumPage(new ForumViewModel(Origin.Public, Order.ProjectCount)) { Title = "Top", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(new ForumViewModel(Origin.Public, Order.Views)) { Title = "Popular", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(new ForumViewModel(Origin.Public, Order.CreationDate)) { Title = "Nous", BackgroundColor = Color.GhostWhite });
		}
    }
}