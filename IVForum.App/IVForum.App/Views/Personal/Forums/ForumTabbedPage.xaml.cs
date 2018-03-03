using IVForum.App.Models;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Shared;

using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		private ObservableCollection<Forum> Models = new ObservableCollection<Forum>();

        public ForumTabbedPage()
        {
            InitializeComponent();

			TryGetForums();

			Children.Add(new ForumPage(Models) { Title = "Personals", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(Models) { Title = "Participants", BackgroundColor = Color.GhostWhite });
        }

		private async void TryGetForums()
		{
			try
			{
				//User user = Settings.GetLoggedUser();
				//var forums = await ApiService.RequestForums(user.Id);
				
				var forums = IVForum.App.Resources.Content.GetForums();

				foreach (Forum f in forums)
				{
					Models.Add(f);
				}

				DependencyService.Get<IMessage>().ShortAlert("Failed to retrieve from Api, using local instead");

			}
			catch (Exception e)
			{
				DependencyService.Get<IMessage>().ShortAlert(e.Message);
			}
		}

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateNewPage() { Title = "Afegir nou fòrum" }, true);
		}
    }
}