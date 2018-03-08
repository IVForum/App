using IVForum.App.Data.Shared;
using IVForum.App.Android.Shared;

using System;
using System.IO;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace IVForum.App.Android.Shared
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(path, filename);
		}
	}
}