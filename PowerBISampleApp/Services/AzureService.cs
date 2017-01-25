using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Xamarin.Forms;
using Plugin.Settings;

namespace PowerBISampleApp
{
	public static class AzureService
	{
		#region Constant Fields
		const string _accessTokenKey = "TokenKey";
		const string _accessTokenExpiresOnKey = "TokenExpirationDateTimeOffsetKey";
		const string _accessTokenTypeKey = "AccessTokenTypeKey";
		const int httpTimeoutInSeconds = 15;
		static readonly HttpClient _client = new HttpClient { Timeout = TimeSpan.FromSeconds(httpTimeoutInSeconds) };
		#endregion

		#region Properties
		static string _accessToken
		{
			get { return CrossSettings.Current.GetValueOrDefault(_accessTokenKey, string.Empty); }
			set { CrossSettings.Current.AddOrUpdateValue(_accessTokenKey, value); }
		}

		static string _accessTokenType
		{
			get { return CrossSettings.Current.GetValueOrDefault(_accessTokenTypeKey, string.Empty); }
			set { CrossSettings.Current.AddOrUpdateValue(_accessTokenTypeKey, value); }
		}

		static DateTimeOffset _accessTokenExpiresOnDateTimeOffset
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

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_accessTokenType, _accessToken);
			var data = await GetDataObjectFromAPI<T>(url);

			return data;
		}

		static async Task<string> GetPowerBIAccessToken()
		{
			if (string.IsNullOrEmpty(_accessToken) || DateTimeOffset.Now.CompareTo(_accessTokenExpiresOnDateTimeOffset) >= 1 || string.IsNullOrEmpty(_accessTokenType))
				await Authenticate();

			return _accessToken;
		}

		static async Task Authenticate()
		{
			var authenticationResult = await DependencyService.Get<IAuthenticator>()?.Authenticate(
						AzureConstants.OAuth2Authority,
						AzureConstants.Resource,
						AzureConstants.ClientId,
						AzureConstants.RedirectURL
					);

			_accessToken = authenticationResult.AccessToken;
			_accessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
			_accessTokenType = authenticationResult.AccessTokenType;
		}

		static async Task<T> GetDataObjectFromAPI<T>(string apiUrl)
		{
			return await Task.Run(async () =>
			{
				try
				{
					var json = await _client.GetStringAsync(apiUrl);

					if (string.IsNullOrWhiteSpace(json))
						return default(T);

					return DeserializeObject<T>(json);
				}
				catch (Exception e)
				{
					DebugHelpers.PrintException(e);
					return default(T);
				}
			});
		}

		#endregion
	}
}
