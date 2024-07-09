using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BP.AuthProvider.Models;
using BP.AuthProvider.Services.Contracts;
using BP.AuthProvider.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BP.AuthProvider.Services
{
    internal class AuthProviderService : IAuthProviderService
    {
        private readonly AuthSettings _authSettings;

        public AuthProviderService(IOptions<AuthSettings> authSettings)
        {
            _authSettings = authSettings.Value ?? throw new ArgumentNullException(nameof(authSettings));
        }

        public TokenModel GenerateToken(AuthModel authModel)
        {
            ArgumentNullException.ThrowIfNull(authModel, nameof(authModel));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_authSettings.TokenLifetimeMinutes);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, authModel.Email),
                new(ClaimTypes.Role, authModel.Role)
            };

            var JWTToken = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            var tokenModel = new TokenModel
            {
                Token = token
            };

            return tokenModel;
        }
    }
}
