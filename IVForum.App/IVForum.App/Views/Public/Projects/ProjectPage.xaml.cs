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
		public List<Bill> Bills { get; set; } = new List<Bill>();
		public bool Subscribed { get; set; } = false;

		public ProjectPage(IOrderedEnumerable<Project> models)
		{
			InitializeComponent();

			foreach (Project p in models)
			{
				Models.Add(p);
			}

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += async (sender, e) => {
				await Navigation.PushAsync(new ProjectDetailPage((Project)e.Item), true);
			};
		}

		public ProjectPage(List<Project> models)
		{
			InitializeComponent();

			foreach (Project p in models)
			{
				Models.Add(p);
			}

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += async (sender, e) => {
				await Navigation.PushAsync(new ProjectDetailPage((Project)e.Item) { Subscribed = this.Subscribed ,Bills = this.Bills }, true);
			};
		}

		public ProjectPage(ObservableCollection<Project> models, bool userSubscribed)
		{
			InitializeComponent();

			Models = models;

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += async (sender, e) => {
				await Navigation.PushAsync(new ProjectDetailPage((Project)e.Item), true);
			};
		}
	}
}