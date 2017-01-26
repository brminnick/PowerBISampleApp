using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public class DetailsViewModel : BaseViewModel
	{
		#region Fields
		Command<string> _getDetailsButtonCommand;
		string _IFrameHtml;
		bool _isGetDetailsButtonVisibile = true, _isRetrievingData;
		#endregion

		#region Properties
		public Command<string> GetChartButtonCommand => _getDetailsButtonCommand ??
			(_getDetailsButtonCommand = new Command<string>(async s => await ExecuteGetDetailsButtonCommand(s)));

		public string IFrameHtml
		{
			get { return _IFrameHtml; }
			set { SetProperty(ref _IFrameHtml, value); }
		}

		public bool IsGetChartButtonVisible
		{
			get { return _isGetDetailsButtonVisibile; }
			set { SetProperty(ref _isGetDetailsButtonVisibile, value); }
		}

		public bool IsRetrievingData
		{
			get { return _isRetrievingData; }
			set { SetProperty(ref _isRetrievingData, value); }
		}
		#endregion

		#region Methods
		async Task ExecuteGetDetailsButtonCommand(string url)
		{
			IsRetrievingData = true;

			var data = await PowerBIService.GetPowerBIData<GroupDashboardRootObjectModel>(url);
			var embedUrl = data.GroupValueModelList.FirstOrDefault()?.EmbedUrl;

			IsRetrievingData = false;

			IFrameHtml = CreateiFrameHTML(embedUrl);

			IsGetChartButtonVisible = false;
		}

		string CreateiFrameHTML(string iFrameEmbdedUrl)
		{
			var htmlStringBuilder = new StringBuilder();

			htmlStringBuilder.Append("<html>");
			htmlStringBuilder.Append("<iframe>");
			htmlStringBuilder.Append("<src>");
			htmlStringBuilder.Append(iFrameEmbdedUrl);
			htmlStringBuilder.Append("</src>");
			htmlStringBuilder.Append("</iframe>");
			htmlStringBuilder.Append("</html>");

			return htmlStringBuilder.ToString();
		}
		#endregion
	}
}
