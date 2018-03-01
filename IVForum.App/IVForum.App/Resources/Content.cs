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
			List<Project> Projects = GetProjects();

			return new List<Forum>
			{
				new Forum { Name = "IVForum", Title = "IVForum", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Hamza, WebsiteUrl = "ivforum.cat", RepositoryUrl = "github.com/Zerixter/ivforum", Projects = Projects, Views = 5 },
				new Forum { Name = "Inquisition Discord Bot", Title = "Inquisition", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Cristian, WebsiteUrl = "placeholder.cat", RepositoryUrl = "github.com/Flysenberg/Inquisition", Projects = Projects, Views = 10 },
				new Forum { Name = "Farmàcies", Title = "Farmàcies BF", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Dmytro, RepositoryUrl = "github.com/LemonBF/Farmacies", Projects = Projects, Views = 9 }
			};
		}

		public static List<Project> GetProjects()
		{
			return new List<Project>
			{
				new Project { Name = "Inquisition", Title = "Inquisition", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Cristian, RepositoryUrl = "github.com/Flysenberg/Inquisition", WebsiteUrl = "www.inquisition-bot.com" },
				new Project { Name = "Placeholder", Title = "Placeholder.cat", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Cristian, RepositoryUrl = "github.com/Flysenberg/Placeholder", WebsiteUrl = "www.placeholder.cat" },
				new Project { Name = "IVForum", Title = "IVForum.cat", Description = Properties.Res.Lorem, CreationDate = DateTime.Now, Owner = Hamza, RepositoryUrl = "gitgub.com/Zerixter/ivforum", WebsiteUrl = "www.ivforum.cat" }
			};
		}
    }
}
