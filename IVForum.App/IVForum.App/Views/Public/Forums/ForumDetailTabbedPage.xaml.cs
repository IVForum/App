using IVForum.App.ViewModels.Public.Forums;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailTabbedPage : TabbedPage
    {
		public PublicForumViewModel Model { get; set; }

        public ForumDetailTabbedPage(PublicForumViewModel model)
        {
            InitializeComponent();
			BindingContext = Model = model;

			Children.Add(new ForumDetailPage(Model));
			Children.Add(new ForumDetailProjectsPage(Model));
        }
    }
}