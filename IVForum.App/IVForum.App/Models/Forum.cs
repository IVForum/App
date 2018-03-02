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
		public DateTime CreationDate { get; set; }
		public int Views { get; set; }

		public User Owner { get; set; }
		public List<Wallet> Wallets { get; set; } = new List<Wallet>();
		public List<Project> Projects { get; set; } = new List<Project>();
	}
}
