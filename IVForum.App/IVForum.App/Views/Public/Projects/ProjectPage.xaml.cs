using IVForum.App.Models;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
		public ProjectPage(IOrderedEnumerable<Project> models)
		{
			InitializeComponent();

			ProjectsListView.ItemsSource = models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		public ProjectPage(List<Project> models)
		{
			InitializeComponent();

			ProjectsListView.ItemsSource = models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		private async void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new ProjectDetailPage((Project)e.Item));
		}
	}
}