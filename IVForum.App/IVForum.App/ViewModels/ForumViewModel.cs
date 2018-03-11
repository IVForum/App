using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

namespace IVForum.App.ViewModels
{
	public class ForumViewModel : BaseViewModel<Forum>
    {
		public Guid UserId { get; set; }

		/// <param name="origin">If set to User, must supply a user Guid to the class UserId property</param>
		/// <param name="order">Defaults to Title if none specified</param>
		public ForumViewModel(Origin origin, Order order = Order.Title)
		{
			Origin = origin;
			Order = order;
		}

		public override async void Load()
		{
			switch (Origin)
			{
				case Origin.Public:
					OrderBy(await ApiService.Forums.Get());
					break;
				case Origin.User:
					OrderBy(await ApiService.Forums.Get(UserId));
					break;
				case Origin.Subscription:
					OrderBy(await ApiService.Subscriptions.Forums());
					break;
				case Origin.Forum:
					break;
				default:
					break;
			}
		}

		private void OrderBy(List<Forum> list)
		{
			switch (Order)
			{
				case Order.Title:
					OrderedModels = list.OrderBy(x => x.Title);
					break;
				case Order.Views:
					OrderedModels = list.OrderBy(x => x.Views);
					break;
				case Order.ProjectCount:
					OrderedModels = list.OrderBy(x => x.Projects.Count);
					break;
				case Order.CreationDate:
					OrderedModels = list.OrderByDescending(x => x.CreationDate);
					break;
				default:
					OrderedModels = list.OrderBy(x => x.Title);
					break;
			}

			Models.Clear();
			foreach (Forum forum in OrderedModels)
				Models.Add(forum);
		}
		
		public override ICommand RefreshCommand
		{
			get
			{
				return new Command(() =>
				{
					IsRefreshing = true;

					Load();

					IsRefreshing = false;
				});
			}
		}
	}
}
