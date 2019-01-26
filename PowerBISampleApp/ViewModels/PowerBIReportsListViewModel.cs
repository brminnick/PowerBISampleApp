using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBISampleApp
{
    public class PowerBIReportsListViewModel : BaseViewModel
    {
        #region Fields
        bool _isReportsListRefreshing;
        List<Report> _visibleReportsDataList;
        ICommand _refreshReportsListCommand;
        #endregion

        #region Properties
        public ICommand RefreshReportsListCommand => _refreshReportsListCommand ??
            (_refreshReportsListCommand = new AsyncCommand(RefreshReportsList, continueOnCapturedContext: false));

        public List<Report> VisibleReportsListData
        {
            get => _visibleReportsDataList;
            set => SetProperty(ref _visibleReportsDataList, value);
        }

        public bool IsReportsListRefreshing
        {
            get => _isReportsListRefreshing;
            set => SetProperty(ref _isReportsListRefreshing, value);
        }

        async Task RefreshReportsList()
        {
            try
            {
                var reports = await PowerBIService.GetReports().ConfigureAwait(false);
                VisibleReportsListData = reports?.Value?.OrderBy(x => x?.Name)?.ToList();
            }
            finally
            {
                IsReportsListRefreshing = false;
            }
        }
        #endregion
    }
}
