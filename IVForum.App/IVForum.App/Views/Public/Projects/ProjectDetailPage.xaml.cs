using IVForum.App.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectDetailPage : ContentPage
	{
		public ProjectDetailPage(Project model)
		{
			InitializeComponent();
			BindingContext = model;
			Title = model.Title;
			BackgroundColor = Color.GhostWhite;
		}
	}
}