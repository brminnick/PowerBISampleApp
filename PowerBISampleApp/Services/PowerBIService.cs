using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;

using Plugin.Settings;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public static class PowerBIService
	{
		#region Constant Fields
		const string _accessTokenKey = "TokenKey";
		const string _accessTokenExpiresOnKey = "TokenExpirationDateTimeOffsetKey";
		const string _accessTokenTypeKey = "AccessTokenTypeKey";

		static readonly TimeSpan _httpTimeout = TimeSpan.FromSeconds(15);
		static readonly JsonSerializer _serializer = new JsonSerializer();
		static readonly HttpClient _client = CreateHttpClient();
		#endregion

		#region Properties
		static string AccessToken
		{
			get { return CrossSettings.Current.GetValueOrDefault(_accessTokenKey, string.Empty); }
			set { CrossSettings.Current.AddOrUpdateValue(_accessTokenKey, value); }
		}

		static string AccessTokenType
		{
			get { return CrossSettings.Current.GetValueOrDefault(_accessTokenTypeKey, string.Empty); }
			set { CrossSettings.Current.AddOrUpdateValue(_accessTokenTypeKey, value); }
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

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AccessTokenType, accessToken);

			var data = await GetDataObjectFromAPI<T>(url);

			return data;
		}

		static async Task<string> GetPowerBIAccessToken()
		{
			if (string.IsNullOrEmpty(AccessToken) || DateTimeOffset.Now.CompareTo(AccessTokenExpiresOnDateTimeOffset) >= 1 || string.IsNullOrEmpty(AccessTokenType))
				await Authenticate();

			return AccessToken;
		}

		static async Task Authenticate()
		{
			var authenticationResult = await DependencyService.Get<IAuthenticator>()?.Authenticate(
						AzureConstants.OAuth2Authority,
						AzureConstants.Resource,
						AzureConstants.ClientId,
						AzureConstants.RedirectURL
					);

			AccessToken = authenticationResult.AccessToken;
			AccessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
			AccessTokenType = authenticationResult.AccessTokenType;
		}

		static async Task<T> GetDataObjectFromAPI<T>(string apiUrl)
		{
			try
			{
				using (var stream = await _client.GetStreamAsync(apiUrl).ConfigureAwait(false))
				using (var reader = new StreamReader(stream))
				using (var json = new JsonTextReader(reader))
				{
					if (json == null)
						return default(T);

					return await Task.Run(() => _serializer.Deserialize<T>(json));
				}
			}
			catch (Exception e)
			{
				DebugHelpers.PrintException(e);
				return default(T);
			}
		}

		static HttpClient CreateHttpClient()
		{
			var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = System.Net.DecompressionMethods.GZip });
			client.Timeout = _httpTimeout;
			client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

			return client;
		}
		#endregion
	}
}
