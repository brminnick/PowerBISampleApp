using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class DetailsViewModel : BaseViewModel
	{
		#region Fields
		Command<string> _getDetailsButtonCommand;
		string _embededDashboardUrl;
		#endregion

		#region Properties
		public Command<string> GetDetailsButtonCommand => _getDetailsButtonCommand ??
			(_getDetailsButtonCommand = new Command<string>(async s => await ExecuteGetDetailsButtonCommand(s)));

		public string EmbededDashboardUrl
		{
			get { return _embededDashboardUrl; }
			set { SetProperty(ref _embededDashboardUrl, value); }
		}
		#endregion

		#region Methods
		async Task ExecuteGetDetailsButtonCommand(string url)
		{
			var data = await AzureService.GetPowerBIData<GroupDashboardRootObjectModel>(url);
			EmbededDashboardUrl = data.GroupValueModelList.FirstOrDefault()?.EmbedUrl;
		}
		#endregion
	}
}
