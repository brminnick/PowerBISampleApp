using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class GroupListImageCell : ImageCell
	{
		public GroupListImageCell()
		{
			this.SetBinding<GroupValueModel>(DetailProperty, m => m.id);
			this.SetBinding<GroupValueModel>(TextProperty, m => m.name);
			this.SetValue(ImageSourceProperty, "PowerBILogo");
		}
	}
}
