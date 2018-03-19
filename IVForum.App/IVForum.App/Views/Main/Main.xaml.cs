using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Main
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Main : MasterDetailPage
    {
        public Main()
        {
            InitializeComponent();
			MasterPage.ListView.ItemTapped += ListView_ItemTapped;
        }

		private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			var item = e.Item as MainMenuItem;
			if (item == null)
				return;

			var page = (Page)Activator.CreateInstance(item.TargetType);
			page.Title = item.Title;

			Detail = new NavigationPage(page);
			IsPresented = false;
		}
    }
}