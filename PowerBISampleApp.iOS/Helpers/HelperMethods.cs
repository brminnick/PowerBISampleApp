using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;

namespace PowerBISampleApp.iOS
{
    public static class HelperMethods
    {
        public static Task<UIViewController> GetVisibleViewControllerAsync() =>
            MainThread.InvokeOnMainThreadAsync(GetVisibleViewController);

        public static UIViewController GetVisibleViewController() => Platform.GetCurrentUIViewController();
    }
}

