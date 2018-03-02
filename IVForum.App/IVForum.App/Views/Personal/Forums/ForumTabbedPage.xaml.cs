using IVForum.App.Models;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Shared;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumTabbedPage : TabbedPage
    {
		private List<Forum> Models = IVForum.App.Resources.Content.GetForums();

        public ForumTabbedPage()
        {
            InitializeComponent();

			Children.Add(new ForumPage(Models) { Title = "Personals", BackgroundColor = Color.GhostWhite });
			Children.Add(new ForumPage(Models) { Title = "Participants", BackgroundColor = Color.GhostWhite });
        }

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateNewPage() { Title = "Afegir nou fòrum" }, true);
		}
    }
}