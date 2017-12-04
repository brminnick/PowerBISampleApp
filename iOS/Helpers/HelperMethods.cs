using System.Threading.Tasks;

using UIKit;

namespace PowerBISampleApp.iOS
{
	public static class HelperMethods
	{
		#region Methods
        public static Task<UIViewController> GetVisibleViewController()
        {
            return XamarinFormsHelpers.BeginInvokeOnMainThreadAsync(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case UINavigationController navigationController:
                        return navigationController.TopViewController;

                    case UITabBarController tabBarController:
                        return tabBarController.SelectedViewController;

                    case null:
                        return rootController;

                    default:
                        return rootController.PresentedViewController;
                }
            });
        }
		#endregion
	}
}

