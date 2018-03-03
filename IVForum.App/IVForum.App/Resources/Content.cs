using IVForum.App.Models;

using System;
using System.Collections.Generic;

namespace IVForum.App.Resources
{
	public class Content
    {
		public static User Hamza = new User
		{
			Id = Guid.NewGuid(),
			Name = "Hamza",
			Surname = "Saddouki",
			Email = "hamza@gmail.com",
			Avatar = "avatar.png",
			RepositoryUrl = "github.com/Zerixter",
			WebsiteUrl = "www.hamza.com",
			TwitterUrl = "@zerixter",
			FacebookUrl = "facebook.com/hamza",
			Description = Properties.Res.Lorem
		};

		public static User Cristian = new User
		{
			Id = Guid.NewGuid(),
			Name = "Cristian",
			Surname = "Moraru",
			Email = "cristian@gmail.com",
			Avatar = "avatar.png",
			RepositoryUrl = "github.com/Flysenberg",
			WebsiteUrl = "www.cristian.moraru.com",
			TwitterUrl = "@cristy0533",
			FacebookUrl = "facebook.com/CristianMoraru",
			Description = Properties.Res.Lorem
		};

		public static User Dmytro = new User
		{
			Id = Guid.NewGuid(),
			Name = "Dmytro",
			Surname = "Holota",
			Email = "dmytro@gmail.com",
			Avatar = "avatar.png",
			RepositoryUrl = "github.com/LemonBF",
			WebsiteUrl = "www.dmytro.com",
			TwitterUrl = "@lemonbf",
			FacebookUrl = "facebook.com/LemonBF",
			Description = Properties.Res.Lorem 
		};

		public static List<Forum> GetForums()
		{
			return new List<Forum>
			{
				new Forum
				{
					Id = Guid.NewGuid(),
					Name = "Apps",
					Title = "Xamarin Apps",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Dmytro,
					Projects = new List<Project> {
						new Project
						{
							Id = Guid.NewGuid(),
							Name = "IVForum",
							Title = "IVForum.cat",
							Description = Properties.Res.Lorem,
							CreationDate = DateTime.Now,
							Owner = Hamza,
							RepositoryUrl = "gitgub.com/Zerixter/ivforum",
							WebsiteUrl = "www.ivforum.cat",
							Background = "banner1.jpg"
						}
					},
					Views = 9,
					Background = "banner1.jpg"
				},
				new Forum
				{
					Id = Guid.NewGuid(),
					Name = "Discord Bots",
					Title = "Discord Bots",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Cristian,
					Projects = new List<Project> {
						new Project
						{
							Id = Guid.NewGuid(),
							Name = "Inquisition",
							Title = "Inquisition",
							Description = Properties.Res.Lorem,
							CreationDate = DateTime.Now,
							Owner = Cristian,
							RepositoryUrl = "github.com/Flysenberg/Inquisition",
							WebsiteUrl = "www.inquisition-bot.com",
							Background = "banner1.jpg"
						}
					},
					Views = 10,
					Background = "banner1.jpg"
				},
				new Forum
				{
					Id = Guid.NewGuid(),
					Name = "Cendraforum",
					Title = "Fòrum d'inversions Cendrassos",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Hamza,
					Projects = new List<Project> {
						new Project
						{
							Id = Guid.NewGuid(),
							Name = "Placeholder",
							Title = "Placeholder.cat",
							Description = Properties.Res.Lorem,
							CreationDate = DateTime.Now,
							Owner = Cristian,
							RepositoryUrl = "github.com/Flysenberg/Placeholder",
							WebsiteUrl = "www.placeholder.cat",
							Background = "banner1.jpg"
						}
					},
					Views = 5,
					Background = "banner1.jpg"
				}
			};
		}

		public static List<Project> GetProjects()
		{
			return new List<Project>
			{
				new Project
				{
					Id = Guid.NewGuid(),
					Name = "IVForum",
					Title = "IVForum.cat",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Hamza,
					RepositoryUrl = "gitgub.com/Zerixter/ivforum",
					WebsiteUrl = "www.ivforum.cat",
					Background = "banner1.jpg"
				},
				new Project
				{
					Id = Guid.NewGuid(),
					Name = "Inquisition",
					Title = "Inquisition",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Cristian,
					RepositoryUrl = "github.com/Flysenberg/Inquisition",
					WebsiteUrl = "www.inquisition-bot.com",
					Background = "banner1.jpg"
				},
				new Project
				{
					Id = Guid.NewGuid(),
					Name = "Placeholder",
					Title = "Placeholder.cat",
					Description = Properties.Res.Lorem,
					CreationDate = DateTime.Now,
					Owner = Cristian,
					RepositoryUrl = "github.com/Flysenberg/Placeholder",
					WebsiteUrl = "www.placeholder.cat",
					Background = "banner1.jpg"
				}
			};
		}

    }
}
