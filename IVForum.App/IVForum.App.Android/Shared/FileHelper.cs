﻿using IVForum.App.Data.Shared;
using IVForum.App.Droid.Shared;

using System;
using System.IO;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace IVForum.App.Droid.Shared
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