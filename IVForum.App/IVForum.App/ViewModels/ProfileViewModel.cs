using IVForum.App.Models;

namespace IVForum.App.ViewModels
{
	public class ProfileViewModel
    {
		public string Name { get; set; }
		public string Email { get; set; }
		public string RepositoryUrl { get; set; }
		public string WebsireUrl { get; set; }
		public string FacebookUrl { get; set; }

		public ProfileViewModel(User user)
		{
			Name = user.Name + " " + user.Surname;
			Email = user.Email;
			RepositoryUrl = user.RepositoryUrl;
			WebsireUrl = user.WebsiteUrl;
			FacebookUrl = user.FacebookUrl;
		}
    }
}
