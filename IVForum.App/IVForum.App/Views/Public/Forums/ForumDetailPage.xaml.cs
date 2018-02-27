using IVForum.App.ViewModels.Public.Forums;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailPage : ContentPage
	{
		public PublicForumViewModel Model { get; set; }
		public ForumDetailPage(PublicForumViewModel forumView)
		{
			InitializeComponent();
			BindingContext = Model = forumView;
		}
	}
}