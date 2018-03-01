using IVForum.App.Models;

using System;

namespace IVForum.App.ViewModels.Public.Projects
{
	public class PublicProjectViewModel
    {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public DateTime CreationDate { get; set; }

		public string Icon { get; set; }
		public string Background { get; set; }

		public string WebsiteUrl { get; set; }
		public string RepositoryUrl { get; set; }

		public PublicProjectViewModel(Project p)
		{
			Name = p.Name;
			Title = p.Title;
			Description = p.Description;

			Icon = "public-projects.png";
			Background = "banner.jpg";

			CreationDate = p.CreationDate;
			WebsiteUrl = p.WebsiteUrl;
			RepositoryUrl = p.RepositoryUrl;
		}
	}
}
