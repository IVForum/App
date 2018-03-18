using IVForum.App.Data.Enums;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Forums;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
        public ForumTabbedPage()
        {
            InitializeComponent();

			Guid userId = Settings.GetLoggedUser().Id;

			Children.Add(new ForumPage(new ForumViewModel(Origin.User) { UserId = userId }) { Title = "Personals" });
			Children.Add(new ForumPage(new ForumViewModel(Origin.Subscription) { UserId = userId }) { Title = "Suscripcions" });
		}

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ForumCreatePage() { Title = "Crear nou fòrum" }, true);
		}
    }
}