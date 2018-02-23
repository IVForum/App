using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Config
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		private Dictionary<string, Page> Pages { get; set; }
		public SettingsPage()
		{
			InitializeComponent();
		}

		async void SaveChanges(object sender, EventArgs e)
		{
			
		}
	}
}