using IVForum.App.Models;

using System;

namespace IVForum.App.ViewModels.Public.Forums
{
	public class PublicForumViewModel
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreationDate { get; set; }
		public int Views { get; set; }

		public int Projects { get; set; }

		public string Background { get; set; }
		public string Owner { get; set; }
		public Forum Forum { get; set; }

		public PublicForumViewModel(Forum f)
		{
			Title = f.Title;
			Description = f.Description.Substring(0, 140) + "...";
			CreationDate = f.CreationDate;
			Views = f.Views;

			Projects = f.Projects.Count;

			Background = "banner.jpg";
			Owner = f.Owner.Name + " " + f.Owner.Surname;

			Forum = f;
		}
	}
}
