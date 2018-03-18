using IVForum.App.Data.Models;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumPage : ContentPage
	{
		private ForumViewModel Model;

		public ForumPage(ForumViewModel model)
		{
			InitializeComponent();

			Model = model;
			Model.Load();

			ForumsListView.BindingContext = Model;
			ForumsListView.ItemTapped += async (sender, args) =>
			{
				await Navigation.PushAsync(new ForumDetailTabbedPage((Forum)args.Item), true);
			};
		}
	}
}