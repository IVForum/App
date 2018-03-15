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
		public bool Subscribed { get; set; }

		public ForumDetailPage(Forum model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Load();
		}

		public ForumDetailPage(Forum model, bool subbed)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Subscribed = subbed;
			Load();
		}

		private async void Load()
		{
			if (Model.Owner.Id == Settings.GetLoggedUser().Id)
			{
				ToolbarItem delete = new ToolbarItem()
				{
					Text = "Eliminar",
					Icon = "cross_w.png"
				};

				ToolbarItems.Add(delete);

				delete.Clicked += async (sender, args) => {
					var response = await DisplayAlert("Avís", "Segur vols esborrar el fòrum?", "Si", "No");

					if (!response)
					{
						return;
					}

					var result = await ApiService.Forums.Delete(Model);

					if (result.IsSuccess)
					{
						Alert.Send("Fòrum esborrat amb èxit");
						await Navigation.PopToRootAsync();
					}
					else
					{
						Alert.Send(result.Message);
					}
				};
			} else
			{
				DetermineSubscription();
			}
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
					Text = "Afegir Projecte",
					BackgroundColor = Color.ForestGreen
				};
				addProjectButton.Clicked += AddProject;

				ForumStackLayout.Children.Add(addProjectButton);
			}
		}

		private async void Subscribe(object sender, EventArgs e)
		{
			Button btn = sender as Button;

			btn.IsEnabled = false;

			var result = await ApiService.Subscriptions.SubscribeToForum(Model);

			if (result.IsSuccess)
			{
				Alert.Send("T'has suscrit correctament");
				btn.IsEnabled = true;
				ForumStackLayout.Children.Remove(btn);

				Button addProjectButton = new Button
				{
					Image = "plus.png",
					Text = "Afegir Projecte",
					BackgroundColor = Color.ForestGreen
				};
				addProjectButton.Clicked += AddProject;

				ForumStackLayout.Children.Add(addProjectButton);
			}
			else
			{
				Alert.Send("Error al processar la suscripció");
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