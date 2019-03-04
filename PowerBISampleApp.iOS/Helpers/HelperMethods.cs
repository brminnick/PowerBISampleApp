using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace PowerBISampleApp.iOS
{
    public static class HelperMethods
    {
        public static Task<UIViewController> GetVisibleViewController()
        {
            var tcs = new TaskCompletionSource<UIViewController>();

            Device.BeginInvokeOnMainThread(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case UINavigationController navigationController:
                        tcs.SetResult(navigationController.TopViewController);
                        break;

                    case UITabBarController tabBarController:
                        tcs.SetResult(tabBarController.SelectedViewController);
                        break;

                    case null:
                        tcs.SetResult(rootController);
                        break;

                    default:
                        tcs.SetResult(rootController.PresentedViewController);
                        break;
                }
            });

            return tcs.Task;
        }
    }
}

