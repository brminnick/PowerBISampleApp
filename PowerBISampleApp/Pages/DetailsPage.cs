using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class DetailsPage : BaseContentPage<DetailsViewModel>
	{
		public DetailsPage(string groupIdApiUrl)
		{
			var getDetailsButton = new Button
			{
				Text = "Get Details",
				CommandParameter = groupIdApiUrl
			};
			getDetailsButton.SetBinding<DetailsViewModel>(Button.CommandProperty, vm => vm.GetDetailsButtonCommand);

			var embededDashboardLabel = new Label();
			embededDashboardLabel.SetBinding<DetailsViewModel>(Label.TextProperty, vm => vm.EmbededDashboardUrl);

			Content = new StackLayout
			{
				Children = {
					getDetailsButton,
					embededDashboardLabel
				}
			};
		}

		protected override void SubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = true;
		}

		protected override void UnsubscribeEventHandlers()
		{
			AreEventHandlersSubscribed = false;
		}
	}
}
