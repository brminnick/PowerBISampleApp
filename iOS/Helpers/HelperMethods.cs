using UIKit;

namespace PowerBISampleApp.iOS
{
	public static class HelperMethods
	{
		public static UIViewController GetVisibleViewController()
		{
			return XamarinFormsHelpers.BeginInvokeOnMainThreadAsync<UIViewController>(() =>
			{
				var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

				if (rootController.PresentedViewController == null)
					return rootController;

				if (rootController.PresentedViewController is UINavigationController)
				{
					return ((UINavigationController)rootController.PresentedViewController).TopViewController;
				}

				if (rootController.PresentedViewController is UITabBarController)
				{
					return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
				}

				return rootController.PresentedViewController;
			}).Result;
		}
	}
}

