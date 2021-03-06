﻿using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumCreatePage : ContentPage
	{
		public ForumCreatePage()
		{
			InitializeComponent();
		}

		private async void Add(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			btn.IsEnabled = false;

			Forum Model = new Forum
			{
				Id = Guid.NewGuid(),
				Title = TitleEntry.Text,
				Description = DescriptionEntry.Text,
				CreationDate = DateTime.Now,
				DateBeginsVote = StartDatePicker.Date.Date,
				DateEndsVote = FinalDatePicker.Date.Date
			};

			var result = await ApiService.Forums.Create(Model);

			if (result.IsSuccess)
			{
				Alert.Send("Fòrum afegit correctament");
				await Navigation.PopToRootAsync(true);
			}
			else
			{
				Alert.Send("Error al afegir el fòrum");
				btn.IsEnabled = true;
			}
		}

		private async void Cancel(object sender, EventArgs e)
		{
			await Navigation.PopToRootAsync(true);
		}
	}
}