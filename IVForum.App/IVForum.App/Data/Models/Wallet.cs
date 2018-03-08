using System;
using System.Collections.Generic;

namespace IVForum.App.Data.Models
{
	public class Wallet
    {
		public Guid Id { get; set; }
		public virtual User Owner { get; set; }
		public virtual Forum Forum { get; set; }
		public virtual List<Bill> Bills { get; set; } = new List<Bill>();
	}
}
