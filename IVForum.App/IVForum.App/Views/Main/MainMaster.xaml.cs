using IVForum.App.Models;
using IVForum.App.Services;
using IVForum.App.Views.Config;
using IVForum.App.Views.Info;
using IVForum.App.Views.Public.Profile;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMaster : ContentPage
    {
        public ListView ListView;

        public MainMaster()
        {
            InitializeComponent();

            BindingContext = new MainMasterViewModel();
            ListView = MenuItemsListView;
        }

		public void Logout(object sender, EventArgs e)
		{
			Settings.Logout();
			Application.Current.MainPage = new StartupTabbedPage();
		}

		class MainMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuItem> MenuItems { get; set; }
            public User User { get; set; }

            public MainMasterViewModel()
            {
				MenuItems = new ObservableCollection<MainMenuItem>(new[]
                {
                    new MainMenuItem { Id = 0, Title = "Perfil", TargetType = typeof(ProfilePage), Icon = "profile.png" },
					new MainMenuItem { Id = 1, Title = "Fòrums", TargetType = typeof(Personal.Forums.ForumTabbedPage), Icon = "personal_forums.png" },
					new MainMenuItem { Id = 2, Title = "Projectes", TargetType = typeof(Personal.Projects.ProjectTabbedPage), Icon = "personal_projects.png" },
                    new MainMenuItem { Id = 3, Title = "Fòrums públics", TargetType = typeof(Public.Forums.ForumTabbedPage), Icon = "public_forums.png" },
                    new MainMenuItem { Id = 4, Title = "Projectes públics", TargetType = typeof(Public.Projects.ProjectTabbedPage), Icon = "public_projects.png" },
					new MainMenuItem { Id = 5, Title = "Sobre nosaltres", TargetType = typeof(About), Icon = "about.png" },
					new MainMenuItem { Id = 6, Title = "Configuració", TargetType = typeof(SettingsPage), Icon = "settings.png" },
                });

				User = Settings.GetLoggedUser();
            }

			#region INotifyPropertyChanged Implementation
			public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}