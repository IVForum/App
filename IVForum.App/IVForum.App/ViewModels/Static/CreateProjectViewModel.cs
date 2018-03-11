using System;

namespace IVForum.App.ViewModels
{
	public class CreateProjectViewModel
    {
		public string Title { get; set; }
		public string Description { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.Now;

		public string WebsiteUrl { get; set; }
		public string FacebookUrl { get; set; }
		public string TwitterUrl { get; set; }
		public string RepositoryUrl { get; set; }
	}
}
