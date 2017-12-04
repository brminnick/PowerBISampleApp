using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Plugin.Settings;

using Xamarin.Forms;

namespace PowerBISampleApp
{
    abstract class PowerBIService : BaseHttpClientService
    {
        #region Constant Fields
        const string _accessTokenKey = "TokenKey";
        const string _accessTokenExpiresOnKey = "TokenExpirationDateTimeOffsetKey";
        const string _accessTokenTypeKey = "AccessTokenTypeKey";
        #endregion

        #region Properties
        static string AccessToken
        {
            get => CrossSettings.Current.GetValueOrDefault(_accessTokenKey, string.Empty);
            set => CrossSettings.Current.AddOrUpdateValue(_accessTokenKey, value);
        }

        static string AccessTokenType
        {
            get => CrossSettings.Current.GetValueOrDefault(_accessTokenTypeKey, string.Empty);
            set => CrossSettings.Current.AddOrUpdateValue(_accessTokenTypeKey, value);
        }

        static DateTimeOffset AccessTokenExpiresOnDateTimeOffset
        {
            get
            {
                DateTimeOffset expirationAsDateTimeOffset;
                var expirationAsString = CrossSettings.Current.GetValueOrDefault(_accessTokenExpiresOnKey, string.Empty);

                if (string.IsNullOrEmpty(expirationAsString))
                    expirationAsDateTimeOffset = new DateTimeOffset(0, 0, 0, 0, 0, 0, TimeSpan.FromMilliseconds(0));
                else
                    DateTimeOffset.TryParse(expirationAsString, out expirationAsDateTimeOffset);

                return expirationAsDateTimeOffset;
            }
            set
            {
                var dateTimeOffsetAsString = value.ToString();
                CrossSettings.Current.AddOrUpdateValue(_accessTokenExpiresOnKey, dateTimeOffsetAsString);
            }
        }
        #endregion

        #region Methods
        public static async Task<T> GetPowerBIData<T>(string url)
        {
            var accessToken = await GetPowerBIAccessToken();

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AccessTokenType, accessToken);

            var data = await GetDataObjectFromAPI<T>(url);

            return data;
        }

        static async Task<string> GetPowerBIAccessToken()
        {
            if (string.IsNullOrWhiteSpace(AccessToken)
                || DateTimeOffset.Now.CompareTo(AccessTokenExpiresOnDateTimeOffset) >= 1
                || string.IsNullOrWhiteSpace(AccessTokenType))
            {
                await Authenticate();
            }

            return AccessToken;
        }

        static async Task Authenticate()
        {
            var authenticationResult = await DependencyService.Get<IAuthenticator>()?.Authenticate(
                        AzureConstants.OAuth2Authority,
                        AzureConstants.Resource,
                        AzureConstants.ClientId,
                        AzureConstants.RedirectURL);

            AccessToken = authenticationResult.AccessToken;
            AccessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
            AccessTokenType = authenticationResult.AccessTokenType;
        }
        #endregion
    }
}
