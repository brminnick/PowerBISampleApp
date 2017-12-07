using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

using Xamarin.Forms;
using Microsoft.PowerBI.Api.V2.Models;

namespace PowerBISampleApp
{
    public class PowerBIReportsListViewModel : BaseViewModel
    {
        #region Fields
        List<Report> _visibleReportsDataList;
        ICommand _initializePowerBIReportsViewModelCommand;
        #endregion

        #region Constructors
        public PowerBIReportsListViewModel() => InitializePowerBIReportsListViewModelCommand?.Execute(null);
        #endregion

        #region Properties
        ICommand InitializePowerBIReportsListViewModelCommand => _initializePowerBIReportsViewModelCommand ??
            (_initializePowerBIReportsViewModelCommand = new Command(async () =>
            {
                var reports = await PowerBIService.GetReports();
                VisibleReportsListData = reports?.Value?.OrderBy(x => x?.Name)?.ToList();
            }));

        public List<Report> VisibleReportsListData
        {
            get => _visibleReportsDataList;
            set => SetProperty(ref _visibleReportsDataList, value);
        }
        #endregion
    }
}
