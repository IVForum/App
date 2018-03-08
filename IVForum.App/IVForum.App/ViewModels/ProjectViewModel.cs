using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace IVForum.App.ViewModels
{
	public class ProjectViewModel : BaseViewModel<Project>
    {
		private Order order;
		private Origin origin;
		public Guid ForumId { get; set; }

		public ProjectViewModel(Origin origin = Origin.Public, Order order = Order.Title)
		{
			this.origin = origin;
			this.order = order;
		}

		public override async Task Load()
		{
			List<Project> list = new List<Project>();

			switch (origin)
			{
				case Origin.Public:
					OrderBy(await ApiService.Projects.Get());
					break;
				case Origin.Personal:
					OrderBy(await ApiService.Account.Projects());
					break;
				case Origin.Forum:
					OrderBy(await ApiService.Forums.Projects(ForumId));
					break;
				default:
					break;
			}
		}

		private void OrderBy(List<Project> list)
		{
			switch (order)
			{
				case Order.Title:
					foreach (Project p in list.OrderBy(x => x.Title))
						Models.Add(p);
					break;
				case Order.Views:
					foreach (Project p in list.OrderBy(x => x.Views))
						Models.Add(p);
					break;
				case Order.CreationDate:
					foreach (Project p in list.OrderBy(x => x.CreationDate))
						Models.Add(p);
					break;
				default:
					foreach (Project p in list)
						Models.Add(p);
					break;
			}
		}

		public override ICommand RefreshCommand
		{
			get
			{
				return new Command(async () =>
				{
					IsRefreshing = true;

					await Load();

					IsRefreshing = false;
				});
			}
		}
	}
}
