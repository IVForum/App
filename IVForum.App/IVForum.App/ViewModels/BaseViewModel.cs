using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IVForum.App.ViewModels
{
	public abstract class BaseViewModel<T> : INotifyPropertyChanged
	{
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

		public abstract Task Load();
		public abstract ICommand RefreshCommand { get; }
		
		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
