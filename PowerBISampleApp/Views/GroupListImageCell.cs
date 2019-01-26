using Microsoft.PowerBI.Api.V2.Models;

using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class GroupListImageCell : ImageCell
    {
        public GroupListImageCell()
        {
            SetValue(ImageSourceProperty, "PowerBILogo");
            this.SetBinding(DetailProperty, nameof(Report.Id));
            this.SetBinding(TextProperty, nameof(Report.Name));
        }
    }
}
