using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class SelectionPage : BaseContentPage<SelectionViewModel>
	{
		#region Constant Fields
		readonly Button _powerBIWebViewPageButton, _powerBIGroupListButton;
		#endregion

		#region Constructors
		public SelectionPage()
		{
			_powerBIWebViewPageButton = new Button
			{
				Text = "Open Power BI as WebView"
			};

			_powerBIGroupListButton = new Button
			{
				Text = "Get Power BI Group List"
			};

			Title = "Page List";

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Children={
					_powerBIWebViewPageButton,
					_powerBIGroupListButton
				}
			};
		}
		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			_powerBIWebViewPageButton.Clicked += HandlePowerBIWebViewButtonClicked;
			_powerBIGroupListButton.Clicked += HandlePowerBIIFramePageButtonClicked;

			AreEventHandlersSubscribed = true;
		}

		protected override void UnsubscribeEventHandlers()
		{
			_powerBIWebViewPageButton.Clicked -= HandlePowerBIWebViewButtonClicked;
			_powerBIGroupListButton.Clicked -= HandlePowerBIIFramePageButtonClicked;

			AreEventHandlersSubscribed = false;
		}

		async void HandlePowerBIWebViewButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PowerBIWebViewPage());
		}

		async void HandlePowerBIIFramePageButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PowerBIGroupListPage());
		}
		#endregion
	}
}
