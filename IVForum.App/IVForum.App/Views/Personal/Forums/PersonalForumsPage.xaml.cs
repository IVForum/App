using IVForum.App.Models;
using IVForum.App.ViewModels.Personal.Forums;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalForumsPage : ContentPage
	{
		public List<Forum> Forums { get; set; } = new List<Forum>();
		public List<PersonalForumViewModel> Models { get; set; } = new List<PersonalForumViewModel>();
		public PersonalForumsPage()
		{
			InitializeComponent();

			Forums = IVForum.App.Resources.Content.GetForums();

			foreach (Forum f in Forums)
			{
				Models.Add(new PersonalForumViewModel(f));
			}

			ForumsListView.ItemsSource = Models;
			ForumsListView.ItemTapped += ForumsListView_ItemTapped;
			ForumsListView.SelectedItem = null;
		}

		private async void ForumsListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			PersonalForumViewModel selectedModel = (PersonalForumViewModel) e.Item;
			await Navigation.PushAsync(new PersonalForumDetailPage(selectedModel), true);
		}

		async void AddNew(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new NewForumPage(), true);
		}
	}
}