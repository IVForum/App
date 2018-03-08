using IVForum.App.Data.Models;

namespace IVForum.App.ViewModels
{
	public class ProfileViewModel
    {
		public string Name { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }
		public string RepositoryUrl { get; set; }
		public string WebsiteUrl { get; set; }
		public string FacebookUrl { get; set; }

		public ProfileViewModel(User user)
		{
			Name = user.Name + " " + user.Surname;
			Email = user.Email;
			Avatar = user.Avatar;
			RepositoryUrl = user.RepositoryUrl;
			WebsiteUrl = user.WebsiteUrl;
			FacebookUrl = user.FacebookUrl;
		}
    }
}
