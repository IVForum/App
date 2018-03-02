using IVForum.App.Models;
using IVForum.App.Views.Public.Projects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailTabbedPage : TabbedPage
    {
		public Forum Model { get; set; }

        public ForumDetailTabbedPage(Forum model)
        {
            InitializeComponent();
			BindingContext = Model = model;

			Children.Add(new ForumDetailPage(Model) { Title = "Informació", BackgroundColor = Color.GhostWhite });
			Children.Add(new ProjectPage(Model.Projects) { Title = "Projectes", BackgroundColor = Color.GhostWhite });
        }
    }
}