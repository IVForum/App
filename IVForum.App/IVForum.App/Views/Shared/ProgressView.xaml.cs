using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IVForum.App.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressView : ContentView
	{
		public ProgressView ()
		{
			InitializeComponent ();
		}
	}
}