using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class GroupListImageCell : ImageCell
	{
		public GroupListImageCell()
		{
			var model = BindingContext as ReportsModel;

			this.SetBinding(DetailProperty, nameof(model.Id));
			this.SetBinding(TextProperty, nameof(model.Name));
			this.SetValue(ImageSourceProperty, "PowerBILogo");
		}
	}
}
