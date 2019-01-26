using System;
using System.Threading.Tasks;

using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace PowerBISampleApp
{
    abstract class BasePowerBIService
    {
        #region Fields
        static int _networkIndicatorCount = 0;
        static PowerBIClient _powerBIClient;
        #endregion

        #region Properties
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
        #endregion

        #region Methods
        protected static async ValueTask<PowerBIClient> GetPowerBIClient()
        {
            if (_powerBIClient is null)
            {
                await Authenticate().ConfigureAwait(false);
                _powerBIClient = new PowerBIClient(new TokenCredentials(AccessToken, AccessTokenType));
            }

            return _powerBIClient;
        }

        protected static void UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
        {
            if (isActivityIndicatorDisplayed)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = true);
                _networkIndicatorCount++;
            }
            else if (--_networkIndicatorCount <= 0)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = false);
                _networkIndicatorCount = 0;
            }
        }

        static async Task Authenticate()
        {
            if (string.IsNullOrWhiteSpace(AccessToken)
                    || string.IsNullOrWhiteSpace(AccessTokenType)
                    || DateTimeOffset.UtcNow.CompareTo(AccessTokenExpiresOnDateTimeOffset) >= 1)
            {
                UpdateActivityIndicatorStatus(true);

                try
                {
                    var authenticationResult = await DependencyService.Get<IAuthenticator>()?.Authenticate(
                                AzureConstants.OAuth2Authority,
                                AzureConstants.Resource,
                                AzureConstants.ApplicationId,
                                AzureConstants.RedirectURL);

                    AccessToken = authenticationResult.AccessToken;
                    AccessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
                    AccessTokenType = authenticationResult.AccessTokenType;
                }
                finally
                {
                    UpdateActivityIndicatorStatus(false);
                }
            }
        }
        #endregion
    }
}
