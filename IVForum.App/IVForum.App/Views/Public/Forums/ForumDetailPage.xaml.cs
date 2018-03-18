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
				ToolbarItem edit = new ToolbarItem()
				{
					Text = "Editar",
					Icon = "edit_w.png"
				};
				ToolbarItems.Add(edit);
				edit.Clicked += Edit_Clicked;

				ToolbarItem delete = new ToolbarItem()
				{
					Text = "Eliminar",
					Icon = "cross_w.png"
				};
				ToolbarItems.Add(delete);
				delete.Clicked += Delete_Clicked;

			} else
			{
				await ApiService.Forums.AddView(Model.Id);
				DetermineSubscription();
			}
		}

		private void Edit_Clicked(object sender, EventArgs e)
		{
			Alert.Send("Edit goes here");
		}

		private async void Delete_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Segur vols esborrar el fòrum?", "Si", "No");

			if (!response)
			{
				return;
			}

			var result = await ApiService.Forums.Delete(Model.Id);

			if (result.IsSuccess)
			{
				Alert.Send("Fòrum esborrat amb èxit");
				await Navigation.PopToRootAsync();
			}
			else
			{
				Alert.Send(result.Message);
			}
		}

		private void DetermineSubscription()
		{
			if (!Subscribed)
			{
				Button button = new Button
				{
					Image = "plus_w.png",
					Text = "Suscriure",
					BackgroundColor = Color.Accent
				};
				button.Clicked += Subscribe;
				ForumStackLayout.Children.Add(button);
			}
			else
			{
				CreateAddProjectButton();
			}
		}

		private async void Subscribe(object sender, EventArgs args)
		{
			Button btn = sender as Button;
			btn.IsEnabled = false;

			var result = await ApiService.Subscriptions.SubscribeToForum(Model);

			if (result.IsSuccess)
			{
				Alert.Send("T'has suscrit correctament");
				ForumStackLayout.Children.Remove(btn);
				CreateAddProjectButton();
			}
			else
			{
				Alert.Send("Error al processar la suscripció");
				btn.IsEnabled = true;
			}
		}

		private void CreateAddProjectButton()
		{
			Button addProjectButton = new Button
			{
				Image = "plus_w.png",
				Text = "Afegir Projecte",
				BackgroundColor = Color.Accent
			};
			addProjectButton.Clicked += AddProject;

			ForumStackLayout.Children.Add(addProjectButton);
		}

		private async void AddProject(object sender, EventArgs e)
		{
			Button btn = sender as Button;
			btn.IsEnabled = false;
			Dictionary<string, Project> ProjectDictionary = new Dictionary<string, Project>();
			List<Project> projects = await ApiService.Account.Projects();

			foreach (Project p in projects)
			{
				ProjectDictionary.Add(p.Title, p);
			}

			Picker picker = new Picker() {
				Title = "Projectes personals"
			};

			foreach (string key in ProjectDictionary.Keys)
			{
				picker.Items.Add(key);
			}

			ForumStackLayout.Children.Add(picker);

			picker.Focus();

			picker.Unfocused += (snd, arg) => { btn.IsEnabled = true; };

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

					ForumStackLayout.Children.Add(confirm);

					confirm.Clicked += async (s, a) => {
						SubscriptionViewModel subscription = new SubscriptionViewModel()
						{
							ForumId = Model.Id.ToString(),
							ProjectId = projectSelected.Id.ToString()
						};

						var result = await ApiService.Forums.AddProjectToForum(subscription);

						if (result.IsSuccess)
						{
							Alert.Send("Projecte afegit correctament");
							ForumStackLayout.Children.Remove(confirm);
							ForumStackLayout.Children.Remove(picker);
							btn.IsEnabled = true;
						}
						else
						{
							Alert.Send("Error al afegir projecte al fòrum");
							btn.IsEnabled = true;
						}
					};
				}
			};
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}