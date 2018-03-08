using IVForum.App.Data.Enums;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
        public ProjectTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ProjectPage(new ProjectViewModel(Origin.Public, Order.Title)) { Title = "Top" });
			Children.Add(new ProjectPage(new ProjectViewModel(Origin.Public, Order.Views)) { Title = "Populars" });
			Children.Add(new ProjectPage(new ProjectViewModel(Origin.Public, Order.CreationDate)) { Title = "Nous" });
		}
	}
}