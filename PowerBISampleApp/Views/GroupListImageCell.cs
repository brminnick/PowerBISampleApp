using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class GroupListImageCell : ImageCell
	{
		public GroupListImageCell()
		{
			this.SetBinding<ReportsModel>(DetailProperty, m => m.Id);
			this.SetBinding<ReportsModel>(TextProperty, m => m.Name);
			this.SetValue(ImageSourceProperty, "PowerBILogo");
		}
	}
}
