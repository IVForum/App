using IVForum.App.Models;
using IVForum.App.ViewModels.Public.Projects;

using System;
using System.Collections.Generic;

namespace IVForum.App.ViewModels.Public.Forums
{
	public class PublicForumDetailViewModel
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }

		public string Background { get; set; }
		public string Owner { get; set; }

		public List<PublicProjectViewModel> Projects { get; set; }

		public PublicForumDetailViewModel(Forum f)
		{
			Title = f.Title;
			Description = f.Description;
			CreationDate = f.CreationDate;

			Background = "banner.jpg";
			Owner = f.Owner.Name + " " + f.Owner.Surname;
		}

		public PublicForumDetailViewModel(PublicForumViewModel sender)
		{
			Forum f = sender.Forum;

			Title = f.Title;
			Description = f.Description;
			CreationDate = f.CreationDate;

			Background = "banner.jpg";
			Owner = f.Owner.Name + " " + f.Owner.Surname;
		}
	}
}
