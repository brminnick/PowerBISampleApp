using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using PowerBISampleApp.Droid;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_Droid))]
namespace PowerBISampleApp.Droid
{
    public class Authenticator_Droid : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string clientId, IEnumerable<string> scopes, string? returnUri)
        {
            AuthenticationResult authenticationResult;

            var applicationBuilder = PublicClientApplicationBuilder.Create(clientId)
                .WithRedirectUri(returnUri ?? $"msal{clientId}://auth")
                .Build();

            var accounts = await applicationBuilder.GetAccountsAsync().ConfigureAwait(false);

            try
            {
                var firstAccount = accounts.FirstOrDefault();
                authenticationResult = await applicationBuilder.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync().ConfigureAwait(false);
            }
            catch (MsalUiRequiredException)
            {
                var currentActivity = Platform.CurrentActivity;
                authenticationResult = await applicationBuilder.AcquireTokenInteractive(scopes).WithParentActivityOrWindow(currentActivity).ExecuteAsync().ConfigureAwait(false);
            }

            return authenticationResult;
        }
    }
}
