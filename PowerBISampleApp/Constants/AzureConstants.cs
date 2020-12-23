using System.Collections.Generic;

namespace PowerBISampleApp
{
    public static class AzureConstants
    {
#warning Update this constant with the Azure AD ApplicationId for your Native App Registration: https://github.com/brminnick/PowerBISampleApp#applicationid
        public const string ApplicationId = "f797e4b7-5a4f-4684-bb4b-520d005511d2";

        public const string OAuth2Authority = "https://login.microsoftonline.com/common";

        public static IReadOnlyList<string> Scopes { get; } = new[]
        {
            "User.Read",
            "https://analysis.windows.net/powerbi/api/Report.Read.All"
        };
    }
}
