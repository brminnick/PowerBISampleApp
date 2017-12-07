using System;

using Microsoft.PowerBI.Api.V2.Models;

using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class PowerBIReportsListPage : BaseContentPage<PowerBIReportsListViewModel>
    {
        #region Constant Fields
        readonly ListView _groupListView;
        #endregion

        #region Constructors
        public PowerBIReportsListPage()
        {
            _groupListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(GroupListImageCell)),
                SeparatorVisibility = SeparatorVisibility.None
            };
            _groupListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.VisibleReportsListData));

            Title = "Reports List";

            Content = _groupListView;
        }
        #endregion

        #region Methods
        protected override void SubscribeEventHandlers() => _groupListView.ItemTapped += HandleItemTapped;

        protected override void UnsubscribeEventHandlers() => _groupListView.ItemTapped -= HandleItemTapped;

        void HandleItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedReportsModel = e.Item as Report;

            Device.BeginInvokeOnMainThread(async () =>
            {
                _groupListView.SelectedItem = null;

                await Navigation.PushAsync(new PowerBIWebViewPage(selectedReportsModel?.WebUrl));
            });
        }
        #endregion
    }
}
