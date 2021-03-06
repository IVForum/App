﻿using System;
using System.Collections.Generic;

namespace IVForum.App.Data.Models
{
	public class Project
    {
		public Guid Id { get; set; }
		
		public string Title { get; set; }
		public string Description { get; set; }
		public int Views { get; set; }

		public string Icon { get; set; }
		public string Background { get; set; }

		public DateTime CreationDate { get; set; }

		public int TotalMoney { get; set; }

		public string WebsiteUrl { get; set; }
		public string RepositoryUrl { get; set; }

		public virtual Forum Forum { get; set; } = null;
		public virtual User Owner { get; set; }
		public virtual List<Bill> Bills { get; set; } = new List<Bill>();
	}
}
