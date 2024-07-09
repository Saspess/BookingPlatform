using BP.AuthProvider.Models;

namespace BP.AuthProvider.Services.Contracts
{
    public interface IAuthProviderService
    {
        TokenModel GenerateToken(AuthModel authModel);
    }
}
