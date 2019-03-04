using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using PowerBISampleApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_iOS))]
namespace PowerBISampleApp.iOS
{
	public class Authenticator_iOS : IAuthenticator
	{
		#region Methods
		public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
		{
			var authContext = new AuthenticationContext(authority);

			if (authContext?.TokenCache?.ReadItems()?.Any() is true)
				authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

			var uri = new Uri(returnUri);
			var controller = await HelperMethods.GetVisibleViewController();
			var platformParams = new PlatformParameters(controller);

			return await authContext?.AcquireTokenAsync(resource, clientId, uri, platformParams);
		}
		#endregion
	}
}
