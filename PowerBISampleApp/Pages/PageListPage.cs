using System;
using System.Linq;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class PageListPage : BaseContentPage<BaseViewModel>
	{
		#region Fields
		readonly ListView _listView;
		readonly Dictionary<Type, string> _pageListDictionary;
		#endregion

		#region Constructors
		public PageListPage()
		{
			_pageListDictionary = new Dictionary<Type, string> 
			{
				{ typeof(PowerBIWebView), "Power BI as WebView" }
			};

			_listView = new ListView
			{
				ItemsSource = _pageListDictionary.Values,
			};

			Title = "Page List";

			Content = _listView;
		}
		#endregion

		#region Methods
		protected override void SubscribeEventHandlers()
		{
			_listView.ItemSelected += HandleItemSelected;
		}

		protected override void UnsubscribeEventHandlers()
		{
			_listView.ItemSelected -= HandleItemSelected;
		}

		async void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedItemValue = e.SelectedItem.ToString();

			var selectedItemType = _pageListDictionary.FirstOrDefault(x => x.Value.Equals(selectedItemValue)).Key;
			var page = Activator.CreateInstance(selectedItemType) as ContentPage;

			await Navigation.PushAsync(page);
		}
		#endregion
	}
}
