using IVForum.App.Data.Enums;
using IVForum.App.Data.Models;
using IVForum.App.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace IVForum.App.ViewModels
{
	public class ForumViewModel : BaseViewModel<Forum>
    {
		private Order order;
		private Origin origin;

		public ForumViewModel(Origin origin = Origin.Public, Order order = Order.Title)
		{
			this.origin = origin;
			this.order = order;
		}

		public override async Task Load()
		{
			List<Forum> list = await ApiService.Forums.Get();

			switch (order)
			{
				case Order.ProjectCount:

					foreach (Forum f in list.OrderBy(x => x.Views))
						Models.Add(f);

					break;
				case Order.Views:

					foreach (Forum f in list.OrderBy(x => x.Projects.Count))
						Models.Add(f);

					break;
				case Order.CreationDate:

					foreach (Forum f in list.OrderBy(x => x.CreationDate))
						Models.Add(f);

					break;

				default:
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
