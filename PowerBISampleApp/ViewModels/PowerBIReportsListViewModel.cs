using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace PowerBISampleApp
{
	public class PowerBIReportsListViewModel : BaseViewModel
	{
		#region Fields
		List<ReportsModel> _visibleReportsDataList;
		#endregion

		#region Constructors
		public PowerBIReportsListViewModel()
		{
			Task.Run(async () =>
			{
				var rootObject = await PowerBIService.GetPowerBIData<ReportsRootObjectModel>(AzureConstants.PowerBIReportsUrl);
				VisibleReportsListData = rootObject.ReportsModelList.OrderBy(x => x.Name).ToList();
			});
		}
		#endregion

		#region Properties
		public List<ReportsModel> VisibleReportsListData
		{
			get { return _visibleReportsDataList; }
			set { SetProperty(ref _visibleReportsDataList, value); }
		}
		#endregion
	}
}
