using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class PowerBIReportsListViewModel : BaseViewModel
	{
		#region Fields
		List<ReportsModel> _visibleReportsDataList;
        ICommand _initializePowerBIReportsViewModelCommand;
        #endregion

        #region Constructors
        public PowerBIReportsListViewModel() => InitializePowerBIReportsListViewModelCommand?.Execute(null);
		#endregion

		#region Properties
		public List<ReportsModel> VisibleReportsListData
		{
			get =>  _visibleReportsDataList;
			set => SetProperty(ref _visibleReportsDataList, value);
		}

        ICommand InitializePowerBIReportsListViewModelCommand => _initializePowerBIReportsViewModelCommand ??
            (_initializePowerBIReportsViewModelCommand = new Command(async () =>
            {
                var rootObject = await PowerBIService.GetPowerBIData<ReportsRootObjectModel>(AzureConstants.PowerBIReportsUrl);
                VisibleReportsListData = rootObject.ReportsModelList.OrderBy(x => x.Name).ToList();
            }));
		#endregion
	}
}
