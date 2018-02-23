using System;
using System.Collections.Generic;

namespace IVForum.App.Models
{
	public class User
    {
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public string Avatar { get; set; }

		public string WebsiteUrl { get; set; }
		public string FacebookUrl { get; set; }
		public string TwitterUrl { get; set; }
		public string RepositoryUrl { get; set; }

		public virtual Token Token { get; set; }
		public virtual List<Forum> Forums { get; set; } = new List<Forum>();
		public virtual List<Project> Projects { get; set; } = new List<Project>();
		public virtual List<Wallet> Wallets { get; set; } = new List<Wallet>();
	}
}
