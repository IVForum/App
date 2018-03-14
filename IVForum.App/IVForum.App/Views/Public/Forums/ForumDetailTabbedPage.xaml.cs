using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Projects;

using System;
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
				bool subbed = await ApiService.Subscriptions.IsSubscribedToForum(Model.Id.ToString());
				List<Bill> bills = await ApiService.RequestProjectBills(Model.Id);

				var result = await ApiService.Subscriptions.IsSubscribedToForum(Model.Id.ToString());

				Children.Add(new ForumDetailPage(Model) { Title = "Informació", Subscribed = subbed });
				Children.Add(new ProjectPage(new ProjectViewModel(Origin.Forum, Order.Title) { ForumId = Model.Id }) { Title = "Projectes", Subscribed = subbed, Bills = bills });
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
    }
}