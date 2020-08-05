using System.Collections;
using System.Linq;
using Microsoft.PowerBI.Api.Models;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace PowerBISampleApp
{
    public class PowerBIReportsListPage : BaseContentPage<PowerBIReportsListViewModel>
    {
        public PowerBIReportsListPage()
        {
            Title = "Reports List";

            Content = new RefreshView
            {
                Content = new CollectionView
                {
                    SelectionMode = SelectionMode.Single,
                    ItemTemplate = new GroupListDataTemplate()
                }.Bind(CollectionView.ItemsSourceProperty, nameof(PowerBIReportsListViewModel.VisibleReportsListData))
                 .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)

            }.Bind(RefreshView.IsRefreshingProperty, nameof(PowerBIReportsListViewModel.IsReportsListRefreshing))
             .Bind(RefreshView.CommandProperty, nameof(PowerBIReportsListViewModel.RefreshReportsListCommand));
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
