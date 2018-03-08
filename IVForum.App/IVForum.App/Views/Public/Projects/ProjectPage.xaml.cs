using IVForum.App.Data.Models;
using IVForum.App.ViewModels;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectPage : ContentPage
	{
		public List<Bill> Bills { get; set; } = new List<Bill>();
		public bool Subscribed { get; set; } = false;

		private BaseViewModel<Project> Model;

		public ProjectPage(BaseViewModel<Project> model)
		{
			InitializeComponent();

			Model = model;

			ProjectsListView.BindingContext = Model;
			ProjectsListView.ItemTapped += async (sender, args) => 
			{
				await Navigation.PushAsync(new ProjectDetailPage((Project)args.Item), true);
			};
		}
	}
}