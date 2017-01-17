using System;
using Xamarin.Forms;
namespace PowerBISampleApp
{
	public class PowerBIWebView : ContentPage
	{
		#region Constructors
		public PowerBIWebView()
		{
			Title = "WebView";

			Content = new WebView
			{
				Source = @"https://msit.powerbi.com/groups/2d3880fa-6882-4fed-9e77-bc3742e75c16/dashboards/8dfc7fa9-efd4-40ad-962e-15f12fc96042"
			};
		}
		#endregion
	}
}
