﻿using IVForum.App.Models;

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

		public PersonalProjectViewModel(Project p)
		{
			Name = p.Name;
			Title = p.Title;
			Description = p.Description;

			CreationDate = p.CreationDate;
			WebsiteUrl = p.WebsiteUrl;
			RepositoryUrl = p.RepositoryUrl;
		}
	}
}
