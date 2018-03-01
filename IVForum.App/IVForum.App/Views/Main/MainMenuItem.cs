using System;

namespace IVForum.App.Views.Main
{

	public class MainMenuItem
    {
        public MainMenuItem()
        {
            TargetType = typeof(Public.Forums.ForumTabbedPage);
        }

        public int Id { get; set; }
        public string Title { get; set; }
		public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}