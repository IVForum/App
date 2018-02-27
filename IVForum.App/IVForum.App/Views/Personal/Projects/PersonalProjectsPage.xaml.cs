using IVForum.App.Models;
using IVForum.App.ViewModels.Personal.Projects;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalProjectsPage : ContentPage
	{
		public List<Project> Projects { get; set; } = new List<Project>();
		public List<PersonalProjectViewModel> Models { get; set; } = new List<PersonalProjectViewModel>();
		public bool IsRefreshing { get; set; } = false;

		public PersonalProjectsPage()
		{
			InitializeComponent();

			Projects = IVForum.App.Resources.Content.GetProjects();

			foreach (Project p in Projects)
			{
				Models.Add(new PersonalProjectViewModel(p));
			}

			ProjectsListView.ItemsSource = Models;
			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
			ProjectsListView.SelectedItem = null;
		}

		private async void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new PersonalProjectDetailPage((PersonalProjectViewModel)e.Item));
		}

		async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewProjectPage(), true);
		}
	}
}