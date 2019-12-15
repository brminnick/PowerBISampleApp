using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBISampleApp
{
    public class PowerBIReportsListViewModel : BaseViewModel
    {
        bool _isReportsListRefreshing;
        ICommand? _refreshReportsListCommand;

        public PowerBIReportsListViewModel()
        {
            //https://codetraveler.io/2019/09/11/using-observablecollection-in-a-multi-threaded-xamarin-forms-application/
            Xamarin.Forms.BindingBase.EnableCollectionSynchronization(VisibleReportsListData, null, ObservableCollectionCallback);
        }

        public ICommand RefreshReportsListCommand => _refreshReportsListCommand ??= new AsyncCommand(RefreshReportsList);

        public ObservableCollection<Report> VisibleReportsListData { get; } = new ObservableCollection<Report>();

        public bool IsReportsListRefreshing
        {
            get => _isReportsListRefreshing;
            set => SetProperty(ref _isReportsListRefreshing, value);
        }

        async Task RefreshReportsList()
        {
            VisibleReportsListData.Clear();

            try
            {
                var reports = await PowerBIService.GetReports();

                foreach (var report in reports.Value.OrderBy(x => x.Name))
                    VisibleReportsListData.Add(report);
            }
            finally
            {
                IsReportsListRefreshing = false;
            }
        }

        //https://codetraveler.io/2019/09/11/using-observablecollection-in-a-multi-threaded-xamarin-forms-application/
        void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            lock (collection)
            {
                accessMethod?.Invoke();
            }
        }
    }
}
