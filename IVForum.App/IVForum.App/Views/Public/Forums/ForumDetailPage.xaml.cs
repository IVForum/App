using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Profile;

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Forums
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForumDetailPage : ContentPage
	{
		private Forum Model = new Forum();
		public bool Subscribed { get; set; } = false;

		public ForumDetailPage(Forum model)
		{
			Model = model;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			User owner = await ApiService.Account.Details(Model.Owner.Id);

			if (owner != null)
			{
				Model.Owner = owner;
			}

			InitializeComponent();
			BindingContext = Model;
			DetermineSubscription();
			await ApiService.Forums.AddView(Model);
		}

		private void DetermineSubscription()
		{
			if (!Subscribed)
			{
				Button button = new Button
				{
					Image = "plus.png",
					Text = "Suscriure"
				};
				button.Clicked += Subscribe;

				ForumStackLayout.Children.Add(button);
			}
			else
			{
				Button addProjectButton = new Button
				{
					Image = "plus.png",
					Text = "Afegir Projecte"
				};
				addProjectButton.Clicked += AddProject;

				ForumStackLayout.Children.Add(addProjectButton);
			}
		}

		private async void Subscribe(object sender, EventArgs e)
		{
			var result = await ApiService.Subscriptions.SubscribeToForum(Model);

			if (result)
			{
				Alert.Send("T'has suscrit correctament");
			}
			else
			{
				Alert.Send("Error al suscriure al fòrum");
			}
		}

		private async void AddProject(object sender, EventArgs e)
		{
			Dictionary<string, Project> ProjectDictionary = new Dictionary<string, Project>();
			List<Project> projects = await ApiService.Account.Projects();

			foreach (Project p in projects)
			{
				ProjectDictionary.Add(p.Title, p);
			}

			Picker picker = new Picker() {
				Title = "Projectes personals",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			foreach (string key in ProjectDictionary.Keys)
			{
				picker.Items.Add(key);
			}

			ForumStackLayout.Children.Add(picker);

			picker.SelectedIndexChanged += (send, args) =>
			{
				if (picker.SelectedIndex == -1)
				{
					return;
				}
				else
				{
					string projectTitle = picker.Items[picker.SelectedIndex];
					Project projectSelected = ProjectDictionary[projectTitle];

					Button confirm = new Button() {
						Text = "Confirmar",
						BackgroundColor = Color.ForestGreen
					};

					confirm.Clicked += async (btn, arg) =>
					{
						SubscriptionViewModel subscription = new SubscriptionViewModel() {
							ForumId = Model.Id.ToString(),
							ProjectId = projectSelected.Id.ToString()
						};

						var result = await ApiService.Forums.AddProjectToForum(subscription);

						if (result.IsSuccess)
						{
							Alert.Send("Projecte afegit correctament");
						}
						else
						{
							Alert.Send("Error al afegir projecte al fòrum");
						}
					};

					ForumStackLayout.Children.Add(confirm);
				}
			};
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}