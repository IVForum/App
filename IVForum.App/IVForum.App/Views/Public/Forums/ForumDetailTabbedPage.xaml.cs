using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.Views.Public.Projects;

using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailTabbedPage : TabbedPage
    {
		private Forum Model;
		private List<Project> ModelProjects = new List<Project>();

        public ForumDetailTabbedPage(Forum model)
        {
            InitializeComponent();
			Model = model;
			Title = Model.Title;
			Load();
        }

		private async void Load()
		{
			try
			{
				bool subbed = await ApiService.IsSubscribedToForum(Model.Id.ToString());
				List<Bill> bills = await ApiService.RequestProjectBills(Model.Id);

				var result = await ApiService.IsSubscribedToForum(Model.Id.ToString());

				Children.Add(new ForumDetailPage(Model) { Title = "Informació", Subscribed = subbed });

				ModelProjects = await ApiService.RequestForumProjects(Model.Id);
				Children.Add(new ProjectPage(ModelProjects) { Title = "Projectes", Subscribed = subbed, Bills = bills });
			}
			catch (System.Exception e)
			{
				Debug.WriteLine(e);
			}
		}
    }
}