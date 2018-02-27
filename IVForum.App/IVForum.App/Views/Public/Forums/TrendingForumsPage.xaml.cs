using IVForum.App.Models;
using IVForum.App.ViewModels.Public.Forums;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrendingForumsPage : ContentPage
	{
		public List<Forum> Forums { get; set; } = new List<Forum>();
		public List<PublicForumViewModel> Models { get; set; } = new List<PublicForumViewModel>();

		public TrendingForumsPage()
		{
			InitializeComponent();

			ForumsListView.ItemsSource = Models;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
		}

		private async void ForumsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			await Navigation.PushAsync(new ForumDetailPage((PublicForumViewModel)e.Item));
		}
	}
}