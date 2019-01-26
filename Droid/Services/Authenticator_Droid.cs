using System;
using System.Linq;
using System.Threading.Tasks;

using Android.App;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Plugin.CurrentActivity;

using PowerBISampleApp.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_Droid))]
namespace PowerBISampleApp.Droid
{
    public class Authenticator_Droid : IAuthenticator
    {
        #region Methods
        public Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var uri = new Uri(returnUri);
            var authContext = new AuthenticationContext(authority);
            var platformParams = new PlatformParameters(CrossCurrentActivity.Current.Activity);

            if (authContext?.TokenCache?.ReadItems()?.Any() is true)
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            return authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
        }
        #endregion
    }
}
