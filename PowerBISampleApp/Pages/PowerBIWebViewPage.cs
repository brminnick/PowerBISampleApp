using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class PowerBIWebViewPage : ContentPage
    {
        public PowerBIWebViewPage(string webpageUrl)
        {
            Title = "WebView";

            Content = new WebView { Source = webpageUrl };
        }
    }
}
