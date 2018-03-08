namespace IVForum.App.Models
{
	public interface IMessage
	{
		void LongAlert(string message);
		void ShortAlert(string message);
	}
}
