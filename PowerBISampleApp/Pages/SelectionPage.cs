using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class SelectionPage : BaseContentPage<SelectionViewModel>
	{
		#region Constant Fields
		readonly Button _powerBIWebViewButton;
		#endregion

		#region Constructors
		public SelectionPage()
		{
			_powerBIWebViewButton = new Button
			{
				Text = "Open Power BI as WebView"
			};

			Title = "Page List";

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Children={
					_powerBIWebViewButton
				}
			};
		}
		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			_powerBIWebViewButton.Clicked += HandlePowerBIWebViewButtonClicked;
		}

		protected override void UnsubscribeEventHandlers()
		{
			_powerBIWebViewButton.Clicked -= HandlePowerBIWebViewButtonClicked;
		}

		async void HandlePowerBIWebViewButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PowerBIWebView());
		}
		#endregion
	}
}
