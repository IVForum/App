using IVForum.App.Models;
using IVForum.App.Services;
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
		private static List<Forum> PersonalForums = new List<Forum>();
		private static List<Forum> ParticipatingForums = new List<Forum>();

        public ForumTabbedPage()
        {
            InitializeComponent();
			Load();
        }

		private async void Load()
		{
			PersonalForums = await ApiService.RequestForums(Settings.GetLoggedUser().Id);
			if (PersonalForums != null)
			{
				Children.Add(new ForumPage(PersonalForums) { Title = "Personals" }); 
			}

			ParticipatingForums = await ApiService.RequestSubscribedForums(Settings.GetLoggedUser().Id);
			if (ParticipatingForums != null)
			{
				Children.Add(new ForumPage(ParticipatingForums) { Title = "Participants" }); 
			}
		}

		public async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CreateNewPage() { Title = "Afegir nou fòrum" }, true);
		}
    }
}