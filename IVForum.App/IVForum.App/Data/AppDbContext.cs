using IVForum.App.Data.Models;
using IVForum.App.Services;

using SQLite;

using System.Collections.Generic;

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
			List<Forum> forums = await ApiService.Forums.Get();

			foreach (Forum f in forums)
			{
				await db.InsertOrReplaceAsync(f);
			}
		}
	}
}
