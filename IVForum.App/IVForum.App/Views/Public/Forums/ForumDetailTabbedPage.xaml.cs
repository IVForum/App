using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Projects;

using System;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumDetailTabbedPage : TabbedPage
    {
		private Forum Model;

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
				var subbed = await ApiService.Subscriptions.IsSubscribedToForum(Model.Id);
				
				Children.Add(new ForumDetailPage(Model, subbed.IsSuccess) { Title = "Informació" });
				Children.Add(new ProjectPage(new ProjectViewModel(Origin.Forum, Order.Title) { ForumId = Model.Id }) { Title = "Projectes", Subscribed = subbed.IsSuccess });
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
    }
}