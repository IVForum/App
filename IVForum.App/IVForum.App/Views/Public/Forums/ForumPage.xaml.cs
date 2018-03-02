using IVForum.App.Models;

using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumPage : ContentPage
	{
		public ForumPage(IOrderedEnumerable<Forum> models)
		{
			InitializeComponent();

			ForumsListView.ItemsSource = models;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		public ForumPage(List<Forum> models)
		{
			InitializeComponent();

			ForumsListView.ItemsSource = models;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		private async void ForumsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new ForumDetailTabbedPage((Forum)e.Item), true);
		}
	}
}