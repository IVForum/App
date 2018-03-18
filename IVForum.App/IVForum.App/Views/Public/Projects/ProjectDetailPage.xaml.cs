using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.ViewModels.Static;
using IVForum.App.Views.Public.Forums;
using IVForum.App.Views.Public.Profile;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Public.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProjectDetailPage : ContentPage
	{
		private Project Model { get; set; } = new Project();
		public List<Bill> Bills { get; set; } = new List<Bill>();
		private ObservableCollection<Button> Contributions = new ObservableCollection<Button>();
		public bool Subscribed { get; set; } = false;

		public ProjectDetailPage(Project model)
		{
			InitializeComponent();
			BindingContext = Model = model;
			Load();
		}

		private async void Load()
		{
			Title = Model.Title;
			await ApiService.Projects.AddView(Model.Id);

			if (Model.Forum != null)
			{
				Frame forumframe = InfoFrame.Create("book_b.png", "Fòrum", Model.Forum.Title);
				TapGestureRecognizer forumTap = new TapGestureRecognizer();
				forumTap.Tapped += ForumTap_Tapped;
				forumframe.GestureRecognizers.Add(forumTap);
				ProjectStackLayout.Children.Add(forumframe);
			}

			if (Model.TotalMoney > 0)
			{
				string money = Model.TotalMoney + " €";
				ProjectStackLayout.Children.Add(InfoFrame.Create("loading.png", "Progrès", money));
			}

			if (Model.Website != null)
			{
				ProjectStackLayout.Children.Add(InfoFrame.Create("web.png", "Pàgina web", Model.Website));
			}

			if (Model.Repository != null)
			{
				ProjectStackLayout.Children.Add(InfoFrame.Create("repo.png", "Repositori", Model.Repository));
			}

			if (Model.Owner.Id == Settings.GetLoggedUser().Id)
			{
				ToolbarItem edit = new ToolbarItem
				{
					Icon = "edit_w.png",
					Text = "Editar"
				};
				edit.Clicked += Edit_Clicked;

				ToolbarItem delete = new ToolbarItem
				{
					Icon = "cross_w.png",
					Text = "Eliminar"
				};
				delete.Clicked += Delete_Clicked;

				ToolbarItems.Add(edit);
				ToolbarItems.Add(delete);
			}
			else if (Subscribed)
			{
				GenerateContributions();
			}
		}

		private async void ForumTap_Tapped(object sender, EventArgs e)
		{
			Forum model = await ApiService.Forums.Select(Model.Forum.Id);
			await Navigation.PushAsync(new ForumDetailTabbedPage(model));
		}

		private void Edit_Clicked(object sender, EventArgs e)
		{
			Alert.Send("Edit goes here");
		}

		private void GenerateContributions()
		{
			ObservableCollection<Button> contributions = new ObservableCollection<Button>();

			Frame frame = new Frame()
			{
				HasShadow = true
			};

			StackLayout buttons = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};

			foreach (Bill b in Bills)
			{
				Button btn = new Button()
				{
					Text = b.Value.ToString(),
					Image = "banknote_w.png",
					BackgroundColor = Color.Accent
				};
				btn.Clicked += Vote;

				contributions.Add(btn);
				buttons.Children.Add(btn);
			}

			Image money = new Image()
			{
				Source = "banknotes_b.png"
			};

			Label title = new Label()
			{
				Text = "Contribucions",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};

			StackLayout header = new StackLayout() {
				Orientation = StackOrientation.Horizontal
			};

			header.Children.Add(money);
			header.Children.Add(title);

			StackLayout general = new StackLayout();
			general.Children.Add(header);
			general.Children.Add(buttons);

			frame.Content = general;

			ProjectStackLayout.Children.Add(frame);
		}

		private async void Vote(object sender, EventArgs e)
		{
			Button btn = sender as Button;

			VoteViewModel vote = new VoteViewModel()
			{
				ProjectId = Model.Id.ToString(),
				Value = btn.Text
			};

			var result = await ApiService.Transactions.Vote(vote);

			if (result.IsSuccess)
			{
				Alert.Send("Vot enviat");
				Contributions.Remove(btn);
				btn.IsEnabled = false;
				btn.IsVisible = false;
			}
			else
			{
				Alert.Send("Hi ha hagut un error al processar el vot");
				btn.IsEnabled = true;
			}
		}

		private async void Delete_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Eliminar aquest projecte permanentment?", "Si", "No");

			if (!response)
				return;

			var result = await ApiService.Projects.Delete(Model.Id);

			if (result.IsSuccess)
			{
				Alert.Send("Project eliminat correctament");
				await Navigation.PopToRootAsync(true);
			}
			else
			{
				Alert.Send("Hi ha hagut un error al intentar esborrar el projecte");
			}
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}