using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class SelectionPage : BaseContentPage<SelectionViewModel>
	{
		#region Constant Fields
		readonly Button _powerBIWebViewPageButton, _powerBIIFramePageButton;
		#endregion

		#region Constructors
		public SelectionPage()
		{
			_powerBIWebViewPageButton = new Button
			{
				Text = "Open Power BI as WebView"
			};

			_powerBIIFramePageButton = new Button
			{
				Text = "Open Power BI as IFrame"
			};

			Title = "Page List";

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Children={
					_powerBIWebViewPageButton,
					_powerBIIFramePageButton
				}
			};
		}
		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			_powerBIWebViewPageButton.Clicked += HandlePowerBIWebViewButtonClicked;
			_powerBIIFramePageButton.Clicked += HandlePowerBIIFramePageButtonClicked;

			AreEventHandlersSubscribed = true;
		}

		protected override void UnsubscribeEventHandlers()
		{
			_powerBIWebViewPageButton.Clicked -= HandlePowerBIWebViewButtonClicked;
			_powerBIIFramePageButton.Clicked -= HandlePowerBIIFramePageButtonClicked;

			AreEventHandlersSubscribed = false;
		}

		async void HandlePowerBIWebViewButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PowerBIWebViewPage());
		}

		async void HandlePowerBIIFramePageButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PowerBIIFramePage());
		}
		#endregion
	}
}
