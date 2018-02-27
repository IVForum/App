using IVForum.App.ViewModels.Public.Forums;

using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TopForumsPage : ContentPage
	{
		public List<PublicForumViewModel> ForumModels { get; set; } = new List<PublicForumViewModel>();
		public TopForumsPage()
		{
			InitializeComponent();
			ForumModels.Add(new PublicForumViewModel());
			ForumsListView.ItemsSource = ForumModels;
			ForumsListView.ItemSelected += ForumsListView_ItemSelected;
		}

		private async void ForumsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new ForumDetailPage((PublicForumViewModel)e.SelectedItem), true);
		}
	}
}