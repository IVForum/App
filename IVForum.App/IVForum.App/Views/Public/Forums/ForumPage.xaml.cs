using IVForum.App.Models;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumPage : ContentPage
	{
		private ObservableCollection<Forum> Forums = new ObservableCollection<Forum>();

		public ForumPage(IOrderedEnumerable<Forum> models)
		{
			InitializeComponent();

			foreach (Forum f in models)
			{
				Forums.Add(f);
			}

			ForumsListView.ItemsSource = Forums;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		public ForumPage(List<Forum> models)
		{
			InitializeComponent();

			foreach (Forum f in models)
			{
				Forums.Add(f);
			}

			ForumsListView.ItemsSource = Forums;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		public ForumPage(ObservableCollection<Forum> models)
		{
			InitializeComponent();

			Forums = models;

			ForumsListView.ItemsSource = Forums;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		private async void ForumsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new ForumDetailTabbedPage((Forum)e.Item), true);
		}
	}
}