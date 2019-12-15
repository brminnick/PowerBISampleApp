using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace PowerBISampleApp
{
	public class App : Xamarin.Forms.Application
	{
		public App()
		{
			var navigationPage = new Xamarin.Forms.NavigationPage(new PowerBIReportsListPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("3498db")
			};

            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

            MainPage = navigationPage;
		}
	}
}
