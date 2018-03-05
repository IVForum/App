using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.Views.Public.Projects;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
		public List<Project> PersonalProjects = new List<Project>();
		private List<Project> ParticipatingProjects = new List<Project>();

        public ProjectTabbedPage()
        {
            InitializeComponent();
			Load();
        }

		private async void Load()
		{
			PersonalProjects = await ApiService.RequestPersonalProjects(Settings.GetLoggedUser().Id);
			Children.Add(new ProjectPage(PersonalProjects) { Title = "Personals" });

			ParticipatingProjects = await ApiService.RequestParticipatingProjects(Settings.GetLoggedUser().Id);
			Children.Add(new ProjectPage(new List<Project>()) { Title = "Participants" });
		}

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProjectCreatePage() { Title = "Afegir nou projecte" }, true);
		}
	}
}