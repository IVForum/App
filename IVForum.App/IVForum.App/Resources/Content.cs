using IVForum.App.Models;

using System;
using System.Collections.Generic;

namespace IVForum.App.Resources
{
	public class Content
    {
		public static User Hamza = new User { Name = "Hamza", Surname = "Saddouki", Email = "hamza@gmail.com", Avatar = "avatar.png", RepositoryUrl = "github.com/Zerixter" };
		public static User Cristian = new User { Name = "Cristian", Surname = "Moraru", Email = "cristian@gmail.com", Avatar = "avatar.png", RepositoryUrl = "github.com/Flysenberg" };
		public static User Dmytro = new User { Name = "Dmytro", Surname = "Holota", Email = "dmytro@gmail.com", Avatar = "avatar.png", RepositoryUrl = "github.com/LemonBF" };

		public static List<Forum> GetForums()
		{
			return new List<Forum>
			{
				new Forum { Name = "IVForum", Title = "IVForum", Description = "Loem ipsum dolot sit amet", CreationDate = DateTime.Now, Owner = Hamza, WebsiteUrl = "ivforum.cat", RepositoryUrl = "github.com/Zerixter/ivforum" },
				new Forum { Name = "Inquisition Discord Bot", Title = "Inquisition", Description = "Loem ipsum dolot sit amet", CreationDate = DateTime.Now, Owner = Cristian, WebsiteUrl = "placeholder.cat", RepositoryUrl = "github.com/Flysenberg/Inquisition" },
				new Forum { Name = "Farmàcies", Title = "Farmàcies BF", Description = "Loem ipsum dolot sit amet", CreationDate = DateTime.Now, Owner = Dmytro, RepositoryUrl = "github.com/LemonBF/Farmacies" }
			};
		}

		public static List<Project> GetProjects()
		{
			return new List<Project>
			{
				new Project { Name = "Inquisition", Title = "Inquisition", Description = "Lorem ipsum dolor sit amet", CreationDate = DateTime.Now, Owner = Cristian, RepositoryUrl = "github.com/Flysenberg/Inquisition" },
				new Project { Name = "Placeholder", Title = "Placeholder.cat", Description = "Lorem ipsum dolor sit amet", CreationDate = DateTime.Now, Owner = Cristian },
				new Project { Name = "IVForum", Title = "IVForum.cat", Description = "Lorem ipsum dolor sit amet", CreationDate = DateTime.Now, Owner = Hamza, RepositoryUrl = "gitgub.com/erixter/ivforum" }
			};
		}
    }
}
