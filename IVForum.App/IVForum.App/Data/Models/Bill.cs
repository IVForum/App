using System;

namespace IVForum.App.Data.Models
{
	public class Bill
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Value { get; set; }
		public string ImgUri { get; set; }

		public virtual Wallet Wallet { get; set; }
	}
}
