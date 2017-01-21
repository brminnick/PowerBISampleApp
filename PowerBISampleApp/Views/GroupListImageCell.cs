using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class GroupListImageCell : ImageCell
	{
		public GroupListImageCell()
		{
			this.SetBinding<GroupValueModel>(DetailProperty, m => m.Id);
			this.SetBinding<GroupValueModel>(TextProperty, m => m.Name);
			this.SetValue(ImageSourceProperty, "PowerBILogo");
		}
	}
}
