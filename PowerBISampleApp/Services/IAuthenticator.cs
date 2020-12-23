using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace PowerBISampleApp
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string authority, string clientId, IEnumerable<string> scopes, string? returnUri = null);
    }
}
