using BP.AuthProvider.Models;
using MediatR;

namespace BP.IdentityMS.Business.Queries.User
{
    public class UserLoginQuery : IRequest<TokenModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
