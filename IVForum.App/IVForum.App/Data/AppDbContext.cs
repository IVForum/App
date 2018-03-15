using IVForum.App.Data.Models;
using IVForum.App.Services;

using SQLite;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace IVForum.App.Data
{
	public class AppDbContext
    {
		private readonly SQLiteAsyncConnection db;

		public AppDbContext(string dbPath)
		{
			db = new SQLiteAsyncConnection(dbPath);
		}

		public async void CreateStructure()
		{
			await db.CreateTableAsync<User>();
			await db.CreateTableAsync<Forum>();
			await db.CreateTableAsync<Project>();
			await db.CreateTableAsync<Wallet>();
			await db.CreateTableAsync<Bill>();
		}

		public async void Sync()
		{
			try
			{
				#region Public forums
				var forumList = await ApiService.Forums.Get();
				foreach (Forum f in forumList)
					await db.InsertOrReplaceAsync(f);
				#endregion

				#region Public Projects
				var projectList = await ApiService.Projects.Get();
				foreach (Project p in projectList)
					await db.InsertOrReplaceAsync(p);
				#endregion
			}
			catch (System.Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		public async Task<List<Forum>> GetPublicForums()
		{
			return await db.Table<Forum>().ToListAsync();
		}

		public async Task<List<Forum>> GetPersonalForums()
		{
			User user = Settings.GetLoggedUser();
			return await db.Table<Forum>().Where(x => x.Owner.Id == user.Id).ToListAsync();
		}

		public async Task<List<Project>> GetPublicProjects()
		{
			return await db.Table<Project>().ToListAsync();
		}

		public async Task<List<Project>> GetPersonalProjects()
		{
			User user = Settings.GetLoggedUser();
			return await db.Table<Project>().Where(x => x.Owner.Id == user.Id).ToListAsync();
		}
	}
}
