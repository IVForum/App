using IVForum.App.Data.Models;
using IVForum.App.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumPage : ContentPage
	{
		private BaseViewModel<Forum> Model;

		public ForumPage(BaseViewModel<Forum> model)
		{
			InitializeComponent();

			Model = model;

			ForumsListView.BindingContext = Model;
			ForumsListView.ItemTapped += async (ender, args) =>
			{
				await Navigation.PushAsync(new ForumDetailTabbedPage((Forum)args.Item), true);
			};
		}
	}
}