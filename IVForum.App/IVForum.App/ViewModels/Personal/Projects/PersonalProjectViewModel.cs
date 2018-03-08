using IVForum.App.Data.Models;

using System;

namespace IVForum.App.ViewModels.Personal.Projects
{
	public class PersonalProjectViewModel
    {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public DateTime CreationDate { get; set; }

		public string Icon { get; set; }
		public string Background { get; set; }

		public string WebsiteUrl { get; set; }
		public string RepositoryUrl { get; set; }

		public int Progress { get; set; }
		public int Goal { get; set; }

		public PersonalProjectViewModel(Project p)
		{
			Title = p.Title;
			Description = p.Description;

			Icon = "personal-projects.png";
			Background = "banner1.jpg";

			CreationDate = p.CreationDate;
			WebsiteUrl = p.Website;
			RepositoryUrl = p.Repository;
		}
	}
}
