namespace IVForum.App.Data.Models
{
	public class Token
    {
		public string Id { get; set; }
		public string Auth_Token { get; set; }
		public int Expires_In { get; set; }
	}
}
