using IVForum.App.ViewModels.Public.Forums;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailPage : ContentPage
	{
		public PublicForumDetailViewModel Model { get; set; }

		public ForumDetailPage(PublicForumViewModel forumView)
		{
			InitializeComponent();
			Model = new PublicForumDetailViewModel(forumView);
			BindingContext = Model;
		}
	}
}