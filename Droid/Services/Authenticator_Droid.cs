using System;
using System.Linq;
using System.Threading.Tasks;

using Android.App;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using PowerBISampleApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_Droid))]
namespace PowerBISampleApp.Droid
{
	public class Authenticator_Droid : IAuthenticator
	{
		public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
		{
			var uri = new Uri(returnUri);
			var authContext = new AuthenticationContext(authority);
			var platformParams = new PlatformParameters((Activity)Xamarin.Forms.Forms.Context);

			if (authContext?.TokenCache?.ReadItems()?.Any() == true)
				authContext = new AuthenticationContext(authContext?.TokenCache?.ReadItems()?.FirstOrDefault()?.Authority);

			var authResult = await authContext?.AcquireTokenAsync(resource, clientId, uri, platformParams);
			return authResult;
		}
	}
}
