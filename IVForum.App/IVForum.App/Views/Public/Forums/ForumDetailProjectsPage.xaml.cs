using IVForum.App.Models;
using IVForum.App.ViewModels.Personal.Projects;
using IVForum.App.ViewModels.Public.Forums;
using IVForum.App.ViewModels.Public.Projects;
using IVForum.App.Views.Personal.Projects;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailProjectsPage : ContentPage
	{
		public PublicForumViewModel Model { get; set; }
		public List<PublicProjectViewModel> Projects { get; set; } = new List<PublicProjectViewModel>();

		public ForumDetailProjectsPage(PublicForumViewModel model)
		{
			InitializeComponent();
			Model = model;

			foreach (Project p in model.Forum.Projects)
			{
				Projects.Add(new PublicProjectViewModel(p));
			}

			ProjectsListView.ItemsSource = Projects;

			ProjectsListView.ItemTapped += ProjectsListView_ItemTapped;
		}

		private async void ProjectsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			//await Navigation.PushModalAsync(new PersonalProjectDetailPage((PersonalProjectViewModel)e.Item));
		}
	}
}