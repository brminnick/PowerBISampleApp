using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class DetailsPage : BaseContentPage<DetailsViewModel>
	{
		public DetailsPage(string groupIdApiUrl)
		{
			var isRetrievingDataActivityIndicator = new ActivityIndicator();
			isRetrievingDataActivityIndicator.SetBinding(IsEnabledProperty, nameof(ViewModel.IsRetrievingData));
			isRetrievingDataActivityIndicator.SetBinding(IsVisibleProperty, nameof(ViewModel.IsRetrievingData));

			var getChartButton = new Button
			{
				Text = "Get Chart",
				CommandParameter = groupIdApiUrl
			};
			getChartButton.SetBinding(IsEnabledProperty, nameof(ViewModel.IsRetrievingData));
			getChartButton.SetBinding(IsVisibleProperty, nameof(ViewModel.IsGetChartButtonVisible));
			getChartButton.SetBinding(Button.CommandProperty, nameof(ViewModel.GetChartButtonCommand));

			var iframeSource = new HtmlWebViewSource();
			iframeSource.SetBinding(HtmlWebViewSource.HtmlProperty, nameof(ViewModel.IFrameHtml));

			var embededDashboardWebView = new WebView
			{
				Source = iframeSource
			};

			var relativeLayout = new RelativeLayout();

			Func<RelativeLayout, double> getDetailsButtonWidth = (p) => getChartButton.Measure(p.Width, p.Height).Request.Width;
			Func<RelativeLayout, double> getDetailsButtonHeight = (p) => getChartButton.Measure(p.Width, p.Height).Request.Height;

			Func<RelativeLayout, double> getIsRetrievingDataActivityIndicatorWidth = (p) => isRetrievingDataActivityIndicator.Measure(p.Width, p.Height).Request.Width;
			Func<RelativeLayout, double> getIsRetrievingDataActivityIndicatorHeight = (p) => isRetrievingDataActivityIndicator.Measure(p.Width, p.Height).Request.Height;

			relativeLayout.Children.Add(embededDashboardWebView,
			   Constraint.Constant(0),
			   Constraint.Constant(0),
			   Constraint.RelativeToParent(parent => parent.Width),
			   Constraint.RelativeToParent(parent => parent.Height)
		   	);

			relativeLayout.Children.Add(getChartButton,
				Constraint.RelativeToParent(parent => parent.Width / 2 - getDetailsButtonWidth(parent) / 2),
				Constraint.Constant(0)
		   	);

			relativeLayout.Children.Add(isRetrievingDataActivityIndicator,
			   	Constraint.RelativeToParent(parent => parent.Width / 2 - getIsRetrievingDataActivityIndicatorWidth(parent) / 2),
				Constraint.RelativeToParent(parent => parent.Height / 2 - getIsRetrievingDataActivityIndicatorHeight(parent) / 2)
           	);

			Content = relativeLayout;
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
