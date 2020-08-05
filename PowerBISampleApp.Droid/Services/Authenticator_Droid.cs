using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PowerBISampleApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_Droid))]
namespace PowerBISampleApp.Droid
{
    public class Authenticator_Droid : IAuthenticator
    {
        public Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var uri = new Uri(returnUri);
            var authContext = new AuthenticationContext(authority);
            var platformParams = new PlatformParameters(Xamarin.Essentials.Platform.CurrentActivity);

            if (authContext.TokenCache?.ReadItems()?.Any() is true)
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            return authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
        }
    }
}
