using IVForum.App.Data.Enums;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;

namespace IVForum.App.ViewModels
{
	public abstract class BaseViewModel<T> : INotifyPropertyChanged
	{
		public Order Order { get; set; } = Order.Title;
		public Origin Origin { get; set; } = Origin.Public;

		private bool isRefreshing = false;

		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set
			{
				isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}

		public ObservableCollection<T> Models { get; set; } = new ObservableCollection<T>();
		public IOrderedEnumerable<T> OrderedModels { get; set; }

		public abstract void Load();
		public virtual ICommand RefreshCommand
		{
			get
			{
				return new Command(() =>
				{
					IsRefreshing = true;
					
					Load();

					IsRefreshing = false;
				});
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
