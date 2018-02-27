using System;

namespace IVForum.App.ViewModels.Personal.Projects
{
	public class CreateProjectViewModel
    {
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.Now;

		public string WebsiteUrl { get; set; }
		public string FacebookUrl { get; set; }
		public string TwitterUrl { get; set; }
		public string RepositoryUrl { get; set; }
	}
}
