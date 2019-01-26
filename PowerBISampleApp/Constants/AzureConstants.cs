namespace PowerBISampleApp
{
	public static class AzureConstants
	{
#warning Update this constant with the Azure AD ApplicationId for your Native App Registration: https://github.com/brminnick/PowerBISampleApp#applicationid
        public const string ApplicationId = "f797e4b7-5a4f-4684-bb4b-520d005511d2";

#warning Update this constant with the Azure AD Redirect Url for your Native App Registration: https://github.com/brminnick/PowerBISampleApp#redirect-url
        public const string RedirectURL = "https://PowerBISampleApp.azurewebsites.net/.auth/login/done";
		
        public const string Resource = "https://analysis.windows.net/powerbi/api";

		public const string OAuth2Authority = "https://login.microsoftonline.com/common";
	}
}
