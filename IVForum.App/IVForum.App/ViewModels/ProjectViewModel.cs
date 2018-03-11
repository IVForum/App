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
	public class ProjectViewModel : BaseViewModel<Project>
    {
		public Guid ForumId { get; set; }
		public Guid UserId { get; set; }

		public ProjectViewModel(Origin origin = Origin.Public, Order order = Order.Title)
		{
			Origin = origin;
			Order = order;
		}

		public override async void Load()
		{
			switch (Origin)
			{
				case Origin.Public:
					OrderBy(await ApiService.Projects.Get());
					break;
				case Origin.User:
					OrderBy(await ApiService.Projects.Get(UserId));
					break;
				case Origin.Forum:
					OrderBy(await ApiService.Forums.Projects(ForumId));
					break;
				case Origin.Subscription:
					OrderBy(await ApiService.Projects.Get());
					break;
				default:
					break;
			}
		}

		private void OrderBy(List<Project> list)
		{
			switch (Order)
			{
				case Order.Title:
					OrderedModels = list.OrderBy(x => x.Title);
					break;
				case Order.Views:
					OrderedModels = list.OrderBy(x => x.Views);
					break;
				case Order.CreationDate:
					OrderedModels = list.OrderBy(x => x.CreationDate);
					break;
				default:
					OrderedModels = list.OrderBy(x => x.Title);
					break;
			}

			Models.Clear();
			foreach (Project project in OrderedModels)
				Models.Add(project);
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
