using Xamarin.Forms;

namespace IVForum.App.ViewModels.Static
{
	public class InfoFrame
    {
		public static Frame Create(string icon, string title, string description)
		{
			Frame frame = new Frame
			{
				HasShadow = true
			};

			StackLayout mainStackLayout = new StackLayout();

			StackLayout innerStackLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal
			};

			Image iconImage = new Image
			{
				Source = icon,
				HeightRequest = 24,
				WidthRequest = 24
			};

			double size = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
			Label titleLabel = new Label
			{
				Text = title,
				FontSize = size
			};

			innerStackLayout.Children.Add(iconImage);
			innerStackLayout.Children.Add(titleLabel);

			mainStackLayout.Children.Add(innerStackLayout);

			Label descriptionLabel = new Label
			{
				Text = description
			};

			mainStackLayout.Children.Add(descriptionLabel);

			frame.Content = mainStackLayout;

			return frame;
		}
    }
}
