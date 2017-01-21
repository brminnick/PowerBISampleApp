using System.Threading.Tasks;
using System.Collections.Generic;

namespace PowerBISampleApp
{
	public class PowerBIGroupListViewModel : BaseViewModel
	{
		#region Fields
		List<GroupValueModel> _visibleGroupValueDataList;
		#endregion

		#region Constructors
		public PowerBIGroupListViewModel()
		{
			Task.Run(async () =>
			{
				var rootObject = await AuthenticationHelpers.GetPowerBIData<GroupRootObjectModel>(AzureConstants.PowerBIGroupUrl);
				VisibleGroupValueData = rootObject.GroupValueModelList;
			});
		}
		#endregion

		#region Properties
		public List<GroupValueModel> VisibleGroupValueData
		{
			get { return _visibleGroupValueDataList; }
			set { SetProperty(ref _visibleGroupValueDataList, value); }
		}
		#endregion
	}
}
