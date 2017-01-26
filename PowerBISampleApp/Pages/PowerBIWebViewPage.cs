using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class PowerBIWebViewPage : BaseContentPage<BaseViewModel>
	{
		#region Constructors
		public PowerBIWebViewPage(string webpageUrl)
		{
			Title = "WebView";

			Content = new WebView
			{
				Source = webpageUrl
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
		#endregion
	}
}
