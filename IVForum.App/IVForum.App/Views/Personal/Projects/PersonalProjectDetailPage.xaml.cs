using IVForum.App.ViewModels.Personal.Projects;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Personal.Projects
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PersonalProjectDetailPage : ContentPage
	{
		public PersonalProjectViewModel Model { get; set; }
		public PersonalProjectDetailPage(PersonalProjectViewModel model)
		{
			InitializeComponent();
			BindingContext = Model = model;
		}
	}
}