using IVForum.App.Models;

namespace IVForum.App.ViewModels
{
	public class MainMasterUserViewModel
    {
		public string Username { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }

		public MainMasterUserViewModel()
		{
			Username = "Username";
			Email = "Email";
			Avatar = "Avatar";
		}

		public MainMasterUserViewModel(User user)
		{
			Username = user.Name + " " + user.Surname;
			Email = user.Email;
			Avatar = user.Avatar;
		}
    }
}
