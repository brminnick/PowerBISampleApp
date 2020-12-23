using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace PowerBISampleApp
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string authority, string clientId, string[] scopes, string? returnUri = null);
    }
}
