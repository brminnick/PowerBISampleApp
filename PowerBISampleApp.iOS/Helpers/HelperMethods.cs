using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace PowerBISampleApp.iOS
{
    public static class HelperMethods
    {
        public static async ValueTask<UIViewController> GetVisibleViewControllerAsync()
        {
            if (Xamarin.Essentials.MainThread.IsMainThread)
                return GetVisibleViewController();

            return await Device.InvokeOnMainThreadAsync(GetVisibleViewController).ConfigureAwait(false);
        }

        public static UIViewController GetVisibleViewController()
        {
            UIViewController? viewController = null;

            var window = UIApplication.SharedApplication.KeyWindow;

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController is null)
            {
                window = UIApplication.SharedApplication
                    .Windows
                    .OrderByDescending(w => w.WindowLevel)
                    .FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);


                viewController = window?.RootViewController ?? throw new InvalidOperationException("Could not find current view controller.");
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return viewController;
        }
    }
}

