using System.Security.Claims;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.Business.Common.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BP.Api.Common.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor.HttpContext));
        }

        public string Email => GetEmail();
        public string Role => GetRole();

        private string GetEmail()
        {
            var emailClaim = _httpContext.User.FindFirst(ClaimTypes.Email)
                ?? throw new NotFoundException(ExceptionMessages.ClaimNotFound);
            var email = emailClaim.Value;
            return email;
        }

        private string GetRole()
        {
            var roleClaim = _httpContext.User.FindFirst(ClaimTypes.Role)
                ?? throw new NotFoundException(ExceptionMessages.ClaimNotFound);
            var role = roleClaim.Value;
            return role;
        }
    }
}
