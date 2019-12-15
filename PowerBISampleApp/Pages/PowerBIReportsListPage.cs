using System.Collections;
using System.Linq;
using Microsoft.PowerBI.Api.V2.Models;
using Xamarin.Forms;

namespace PowerBISampleApp
{
    public class PowerBIReportsListPage : BaseContentPage<PowerBIReportsListViewModel>
    {
        public PowerBIReportsListPage()
        {
            var collectionView = new CollectionView
            {
                SelectionMode = SelectionMode.Single,
                ItemTemplate = new GroupListDataTemplate()
            };
            collectionView.SelectionChanged += HandleSelectionChanged;
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(PowerBIReportsListViewModel.VisibleReportsListData));

            var refreshView = new RefreshView
            {
                Content = collectionView
            };
            refreshView.SetBinding(RefreshView.IsRefreshingProperty, nameof(PowerBIReportsListViewModel.IsReportsListRefreshing));
            refreshView.SetBinding(RefreshView.CommandProperty, nameof(PowerBIReportsListViewModel.RefreshReportsListCommand));

            Title = "Reports List";

            Content = refreshView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Content is RefreshView refreshView
                && refreshView.Content is CollectionView collectionView
                && IsNullOrEmpty(collectionView.ItemsSource))
            {
                refreshView.IsRefreshing = true;
            }

            static bool IsNullOrEmpty(in IEnumerable? enumerable) => !enumerable?.GetEnumerator().MoveNext() ?? true;
        }

        async void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collectionView = (CollectionView)sender;
            collectionView.SelectedItem = null;

            if (e.CurrentSelection.FirstOrDefault() is Report tappedReport)
                await Navigation.PushAsync(new PowerBIWebViewPage(tappedReport.WebUrl));
        }
    }
}
