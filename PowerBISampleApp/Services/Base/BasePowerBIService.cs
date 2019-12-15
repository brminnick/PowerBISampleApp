using System;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PowerBISampleApp
{
    abstract class BasePowerBIService
    {
        static int _networkIndicatorCount;
        static PowerBIClient? _powerBIClient;

        static string AccessToken
        {
            get => Preferences.Get(nameof(AccessToken), string.Empty);
            set => Preferences.Set(nameof(AccessToken), value);
        }

        static string AccessTokenType
        {
            get => Preferences.Get(nameof(AccessTokenType), string.Empty);
            set => Preferences.Set(nameof(AccessTokenType), value);
        }

        static DateTimeOffset AccessTokenExpiresOnDateTimeOffset
        {
            get
            {
                DateTimeOffset expirationAsDateTimeOffset;

                var expirationAsString = Preferences.Get(nameof(AccessTokenExpiresOnDateTimeOffset), string.Empty);

                if (string.IsNullOrWhiteSpace(expirationAsString))
                    expirationAsDateTimeOffset = new DateTimeOffset(0, 0, 0, 0, 0, 0, TimeSpan.FromMilliseconds(0));
                else
                    DateTimeOffset.TryParse(expirationAsString, out expirationAsDateTimeOffset);

                return expirationAsDateTimeOffset;
            }
            set
            {
                var dateTimeOffsetAsString = value.ToString();
                Preferences.Set(nameof(AccessTokenExpiresOnDateTimeOffset), dateTimeOffsetAsString);
            }
        }

        protected static async ValueTask<PowerBIClient> GetPowerBIClient()
        {
            if (_powerBIClient is null)
            {
                await Authenticate().ConfigureAwait(false);
                _powerBIClient = new PowerBIClient(new TokenCredentials(AccessToken, AccessTokenType));
            }

            return _powerBIClient;
        }

        protected static async ValueTask UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
        {
            if (isActivityIndicatorDisplayed)
            {
                _networkIndicatorCount++;
                await updateIsBusy(true).ConfigureAwait(false);
            }
            else if (--_networkIndicatorCount <= 0)
            {
                _networkIndicatorCount = 0;
                await updateIsBusy(false).ConfigureAwait(false);
            }

            static async ValueTask updateIsBusy(bool isBusy)
            {
                if (MainThread.IsMainThread)
                    Application.Current.MainPage.IsBusy = isBusy;
                else
                    await Device.InvokeOnMainThreadAsync(() => Application.Current.MainPage.IsBusy = isBusy).ConfigureAwait(false);
            }
        }

        static async Task Authenticate()
        {
            if (string.IsNullOrWhiteSpace(AccessToken)
                    || string.IsNullOrWhiteSpace(AccessTokenType)
                    || DateTimeOffset.UtcNow.CompareTo(AccessTokenExpiresOnDateTimeOffset) >= 1)
            {
                await UpdateActivityIndicatorStatus(true).ConfigureAwait(false);

                try
                {
                    var authenticationResult = await DependencyService.Get<IAuthenticator>().Authenticate(
                                                        AzureConstants.OAuth2Authority,
                                                        AzureConstants.Resource,
                                                        AzureConstants.ApplicationId,
                                                        AzureConstants.RedirectURL).ConfigureAwait(false);

                    AccessToken = authenticationResult.AccessToken;
                    AccessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
                    AccessTokenType = authenticationResult.AccessTokenType;
                }
                finally
                {
                    await UpdateActivityIndicatorStatus(false).ConfigureAwait(false);
                }
            }
        }
    }
}
