using IVForum.App.Data.Models;
using IVForum.App.Services;

using System;

namespace IVForum.App.ViewModels
{
	public class UserViewModel : BaseViewModel<User>
	{
		public Guid UserId { get; set; }
		public User UserModel { get; set; }

		public UserViewModel()
		{
			Load();
		}

		public override async void Load()
		{
			UserModel = await ApiService.Account.Details(UserId);
		}
	}
}
