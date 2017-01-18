using System;
using Xamarin.Forms;
using System.Threading.Tasks;
namespace PowerBISampleApp
{
	public class PowerBIIFrameViewModel : BaseViewModel
	{
		public PowerBIIFrameViewModel()
		{
			Task.Run(async () =>
			{
				try
				{
					var authenticationData = await DependencyService.Get<IAuthenticator>()?.Authenticate(
						AzureConstants.Authority,
						AzureConstants.ApplicationID,
						AzureConstants.ObjectID,
						AzureConstants.RedirectURL
					);

					var temp = DateTime.Now;
				}
				catch(Exception e)
				{
					DebugHelpers.PrintException(e);
				}
			});
		}
	}
}
