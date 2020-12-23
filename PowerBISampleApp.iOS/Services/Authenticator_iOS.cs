using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using PowerBISampleApp.iOS;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticator_iOS))]
namespace PowerBISampleApp.iOS
{
    public class Authenticator_iOS : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string clientId, string[] scopes, string? returnUri)
        {
            AuthenticationResult authenticationResult;

            var applicationBuilder = PublicClientApplicationBuilder.Create(clientId)
                .WithRedirectUri(returnUri ?? $"msal{clientId}://auth")
                .WithIosKeychainSecurityGroup("com.minnick.powerbisampleapp")
                .Build();

            var accounts = await applicationBuilder.GetAccountsAsync().ConfigureAwait(false);

            try
            {
                var firstAccount = accounts.FirstOrDefault();
                authenticationResult = await applicationBuilder.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync().ConfigureAwait(false);
            }
            catch (MsalUiRequiredException)
            {
                var currentViewController = await MainThread.InvokeOnMainThreadAsync(Platform.GetCurrentUIViewController);
                authenticationResult = await applicationBuilder.AcquireTokenInteractive(scopes).WithParentActivityOrWindow(currentViewController).ExecuteAsync().ConfigureAwait(false);
            }

            return authenticationResult;
        }
    }
}