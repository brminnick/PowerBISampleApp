using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class DetailsPage : BaseContentPage<DetailsViewModel>
	{
		public DetailsPage(string groupIdApiUrl)
		{
			var isRetrievingDataActivityIndicator = new ActivityIndicator();
			isRetrievingDataActivityIndicator.SetBinding<DetailsViewModel>(IsEnabledProperty, vm => vm.IsRetrievingData);
			isRetrievingDataActivityIndicator.SetBinding<DetailsViewModel>(IsVisibleProperty, vm => vm.IsRetrievingData);

			var getChartButton = new Button
			{
				Text = "Get Chart",
				CommandParameter = groupIdApiUrl
			};
			getChartButton.SetBinding<DetailsViewModel>(IsEnabledProperty, vm => vm.IsRetrievingData);
			getChartButton.SetBinding<DetailsViewModel>(IsVisibleProperty, vm => vm.IsGetChartButtonVisible);
			getChartButton.SetBinding<DetailsViewModel>(Button.CommandProperty, vm => vm.GetChartButtonCommand);

			var iframeSource = new HtmlWebViewSource();
			iframeSource.SetBinding<DetailsViewModel>(HtmlWebViewSource.HtmlProperty, vm => vm.IFrameHtml);

			var embededDashboardWebView = new WebView
			{
				Source = iframeSource
			};

			var relativeLayout = new RelativeLayout();

			Func<RelativeLayout, double> getDetailsButtonWidth = (p) => getChartButton.Measure(relativeLayout.Width, relativeLayout.Height).Request.Width;
			Func<RelativeLayout, double> getDetailsButtonHeight = (p) => getChartButton.Measure(relativeLayout.Width, relativeLayout.Height).Request.Height;

			Func<RelativeLayout, double> getIsRetrievingDataActivityIndicatorWidth = (p) => isRetrievingDataActivityIndicator.Measure(relativeLayout.Width, relativeLayout.Height).Request.Width;
			Func<RelativeLayout, double> getIsRetrievingDataActivityIndicatorHeight = (p) => isRetrievingDataActivityIndicator.Measure(relativeLayout.Width, relativeLayout.Height).Request.Height;

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
