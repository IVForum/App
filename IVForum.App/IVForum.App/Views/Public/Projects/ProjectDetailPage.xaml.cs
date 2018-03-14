using IVForum.App.Data.Models;
using IVForum.App.Services;
using IVForum.App.ViewModels;
using IVForum.App.Views.Public.Profile;
using IVForum.App.Views.Shared;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
			var result = await ApiService.Projects.AddView(Model);

			if (Subscribed)
			{
				GenerateContributions();
			}
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
					Image = "banknote.png"
				};
				btn.Clicked += Vote;

				contributions.Add(btn);
				buttons.Children.Add(btn);
			}

			Image money = new Image()
			{
				Source = "money.png"
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

			bool result = await ApiService.VoteProject(vote);

			if (result)
			{
				Alert.Send("Vot enviat");
				btn.IsEnabled = false;
				Contributions.Remove(btn);
			}
			else
			{
				Alert.Send("Hi ha hagut un error al processar el vot");
			}
		}

		private async void Delete_Clicked(object sender, EventArgs e)
		{
			var response = await DisplayAlert("Avís", "Eliminar aquest projecte permanentment?", "Si", "No");

			if (!response)
				return;

			await Navigation.PushModalAsync(new LoadingPage(), false);

			var result = true; //await ApiService.DeleteProject(Model);

			if (result)
			{
				await Task.Delay(500);

				Alert.Send("Project eliminat correctament");

				await Navigation.PopModalAsync(true);
				await Navigation.PopToRootAsync(false);
			}
		}

		private async void ShowProfile(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ProfileTabbedPage(Model.Owner), true);
		}
	}
}