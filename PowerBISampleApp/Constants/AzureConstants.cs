namespace PowerBISampleApp
{
	public static class AzureConstants
	{
		public const string Authority = "https://login.windows.net/common";
		public const string ClientId = "f797e4b7-5a4f-4684-bb4b-520d005511d2";
		public const string ObjectId = "561b13a9-2351-40f8-9a88-77d10f3e0f86";
		public const string RedirectURL = "http://PowerBISampleApp.azurewebsites.net/.auth/login/done";
		public const string Resource = "https://analysis.windows.net/powerbi/api";
		public const string ServiceUrl = "https://PowerBISampleApp.azurewebsites.net";

		public const string OAuth2Authority = "https://login.windows.net/common/oauth2/authorize";

		public const string PowerBIOAuth2AuthorityUrl = "https://login.windows.net/common/oauth2/authorize";
		public const string PowerBIDataSetUrl = "https://api.powerbi.com/v1.0/myorg/datasets";
		public const string PowerBIGroupsUrl = "https://api.powerbi.com/v1.0/myorg/groups";
		public const string PowerBIReportsUrl = "https://api.powerbi.com/v1.0/myorg/reports";
	}
}
