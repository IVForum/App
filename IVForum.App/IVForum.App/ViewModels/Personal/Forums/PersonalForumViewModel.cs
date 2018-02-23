using IVForum.App.Models;

namespace IVForum.App.ViewModels.Personal.Forums
{
	public class PersonalForumViewModel
    {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public string Icon { get; set; }
		public string Background { get; set; }

		public string WebsiteUrl { get; set; }
		public string RepositoryUrl { get; set; }
		public string FacebookUrl { get; set; }

		public PersonalForumViewModel(Forum f)
		{
			Name = f.Name;
			Title = f.Title;
			Description = f.Description;

			Icon = f.Icon;
			Background = f.Background;

			WebsiteUrl = f.WebsiteUrl;
			RepositoryUrl = f.RepositoryUrl;
			FacebookUrl = f.FacebookUrl;
		}
    }
}
