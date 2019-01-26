using Microsoft.PowerBI.Api.V2.Models;

using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class PowerBIReportsListPage : BaseContentPage<PowerBIReportsListViewModel>
    {
        #region Constant Fields
        ListView _groupListView;
        #endregion

        #region Constructors
        public PowerBIReportsListPage()
        {
            _groupListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(GroupListImageCell)),
                SeparatorVisibility = SeparatorVisibility.None,
                IsPullToRefreshEnabled = true
            };
            _groupListView.ItemTapped += HandleItemTapped;
            _groupListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.VisibleReportsListData));
            _groupListView.SetBinding(ListView.IsRefreshingProperty, nameof(ViewModel.IsReportsListRefreshing));
            _groupListView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.RefreshReportsListCommand));

            Title = "Reports List";

            Content = _groupListView;
        }
        #endregion

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _groupListView.BeginRefresh();
        }

        #region Methods
        void HandleItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView groupListView 
                    && e.Item is Report tappedReport)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new PowerBIWebViewPage(tappedReport?.WebUrl));
                    groupListView.SelectedItem = null;
                });
            }
        }
        #endregion
    }
}
