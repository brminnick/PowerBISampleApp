using System;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new PageListPage())
			{
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("3498db")
			};
		}
	}
}
