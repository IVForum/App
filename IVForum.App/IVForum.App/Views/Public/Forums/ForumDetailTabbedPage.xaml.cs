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
				var subbed = await ApiService.Subscriptions.IsSubscribedToForum(Model.Id);
				List<Bill> bills = new List<Bill>();

				bool sub = false;
				if (subbed.IsSuccess)
				{
					bills = await ApiService.Subscriptions.Bills(Model.Id);
					sub = true;
				}

				Children.Add(new ForumDetailPage(Model, sub) { Title = "Informació" });
				Children.Add(new ProjectPage(new ProjectViewModel(Origin.Forum, Order.Title) { ForumId = Model.Id }) { Title = "Projectes", Subscribed = subbed.IsSuccess, Bills = bills });
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}
    }
}