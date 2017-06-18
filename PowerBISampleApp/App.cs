using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new PowerBIReportsListPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("3498db")
			};
		}
	}
}
