using IVForum.App.Data.Enums;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Projects;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectTabbedPage : TabbedPage
    {
        public ProjectTabbedPage()
        {
            InitializeComponent();

			Guid userId = Settings.GetLoggedUser().Id;

			Children.Add(new ProjectPage(new ProjectViewModel(Origin.User, Order.Title) { UserId = userId }) { Title = "Personals" });
			Children.Add(new ProjectPage(new ProjectViewModel(Origin.Subscription, Order.Title)) { Title = "Participants" });
		}

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProjectCreatePage() { Title = "Crear nou projecte" }, true);
		}
	}
}