using IVForum.App.Models;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
		private ObservableCollection<Project> Models { get; set; } = new ObservableCollection<Project>();

		public ProjectPage(IOrderedEnumerable<Project> models)
		{
			InitializeComponent();

			foreach (Project p in models)
			{
				Models.Add(p);
			}

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		public ProjectPage(List<Project> models)
		{
			InitializeComponent();

			foreach (Project p in models)
			{
				Models.Add(p);
			}

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		public ProjectPage(ObservableCollection<Project> models)
		{
			InitializeComponent();

			Models = models;

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		private async void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new ProjectDetailPage((Project)e.Item), true);
		}
	}
}