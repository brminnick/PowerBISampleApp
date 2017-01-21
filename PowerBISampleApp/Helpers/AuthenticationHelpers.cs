using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using static Newtonsoft.Json.JsonConvert;

using Microsoft.IdentityModel.Clients.ActiveDirectory;

using Xamarin.Forms;

namespace PowerBISampleApp
{
	public static class AuthenticationHelpers
	{
		#region Constant Fields
		const int httpTimeoutInSeconds = 15;
		static readonly HttpClient _client = new HttpClient { Timeout = TimeSpan.FromSeconds(httpTimeoutInSeconds) };
		#endregion

		#region Fields
		static AuthenticationResult _authenticationResult;
		#endregion

		#region Methods
		public static async Task<string> GetPowerBIAccessToken()
		{
			if (_authenticationResult == null)
				await Authenticate();

			return _authenticationResult.AccessToken;
		}

		public static async Task<T> GetPowerBIData<T>(string url)
		{
			var accessToken = await GetPowerBIAccessToken();

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_authenticationResult.AccessTokenType, accessToken);
			var data = await GetDataObjectFromAPI<T>(url);
			return data;
		}

		static async Task Authenticate()
		{
			_authenticationResult = await DependencyService.Get<IAuthenticator>()?.Authenticate(
						AzureConstants.OAuth2Authority,
						AzureConstants.Resource,
						AzureConstants.ClientId,
						AzureConstants.RedirectURL
					);
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
