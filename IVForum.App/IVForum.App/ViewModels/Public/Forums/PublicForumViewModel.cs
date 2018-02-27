using IVForum.App.Models;

using System;

namespace IVForum.App.ViewModels.Public.Forums
{
	public class PublicForumViewModel
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public string RepositoryUrl { get; set; }
		public string WebsiteUrl { get; set; }
		public string FacebookUrl { get; set; }
		public string Owner { get; set; }

		public PublicForumViewModel()
		{
			Title = "IVForum";
			Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			CreationDate = DateTime.Now;
			RepositoryUrl = "github.com/Flysenberg/IVForum";
			WebsiteUrl = "ivforum.cat";
			FacebookUrl = "facebook.com/ivforum";
			Owner = "Cristian Moraru";
		}

		public PublicForumViewModel(Forum f)
		{
			Title = f.Title;
			Description = f.Description;
			CreationDate = f.CreationDate;
			RepositoryUrl = f.RepositoryUrl;
			WebsiteUrl = f.WebsiteUrl;
			FacebookUrl = f.FacebookUrl;
			Owner = f.Owner.Name + " " + f.Owner.Surname;
		}
	}
}
