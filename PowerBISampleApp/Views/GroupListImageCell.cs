using Microsoft.PowerBI.Api.V2.Models;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class GroupListImageCell : ImageCell
	{
		public GroupListImageCell()
		{
			var model = BindingContext as Report;

			this.SetBinding(DetailProperty, nameof(model.Id));
			this.SetBinding(TextProperty, nameof(model.Name));
			this.SetValue(ImageSourceProperty, "PowerBILogo");
		}
	}
}
