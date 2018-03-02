using System.Text.RegularExpressions;

using Xamarin.Forms;

namespace IVForum.App.Services
{
	public class PasswordValidatorService : TriggerAction<Entry>
    {
		protected override void Invoke(Entry entry)
		{
			Regex pass = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*(_|[^\\w])).+$");

			if (!pass.IsMatch(entry.Text))
			{
				entry.TextColor = Color.Red;
			}
			else
			{
				entry.TextColor = Color.ForestGreen;
			}
		}
    }

	public class EmailValidatorService : TriggerAction<Entry>
	{
		protected override void Invoke(Entry entry)
		{
			Regex email = new Regex("^[a-z0-9._%+-]+@[a-z0-9.-]+[^\\.]\\.[a-z]{2,3}$");

			if (!email.IsMatch(entry.Text))
			{
				entry.TextColor = Color.Red;
			}
			else
			{
				entry.TextColor = Color.ForestGreen;
			}
		}
	}
}
