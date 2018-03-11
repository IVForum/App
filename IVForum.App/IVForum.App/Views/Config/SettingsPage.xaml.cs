using IVForum.App.Services;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Config
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		private List<Setting> settings = new List<Setting>();

		public SettingsPage()
		{
			InitializeComponent();

			settings.Add(new Setting("Opció 1"));
			settings.Add(new Setting("Opció 1"));
			settings.Add(new Setting("Opció 1"));

			SettingsListView.ItemsSource = settings;
		}

		private void SaveChanges(object sender, EventArgs e)
		{
			Alert.Send("Dades desades");
		}

		internal class Setting
		{
			public string Title { get; set; }

			public Setting(string title)
			{
				Title = title;
			}
		}
	}
}