using System;
using System.Collections.Generic;

namespace IVForum.App.Models
{
	public class Forum
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public string Icon { get; set; }
		public string Background { get; set; }

		public string WebsiteUrl { get; set; }
		public string RepositoryUrl { get; set; }
		public string FacebookUrl { get; set; }

		public virtual User Owner { get; set; }
		public virtual List<Project> Projects { get; set; } = new List<Project>();
		public virtual List<Wallet> Wallets { get; set; } = new List<Wallet>();
	}
}
