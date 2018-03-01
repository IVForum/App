using IVForum.App.Models;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailPage : ContentPage
	{
		public ForumDetailPage(Forum model)
		{
			InitializeComponent();
			BindingContext = model;
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			// ...
		}
	}
}