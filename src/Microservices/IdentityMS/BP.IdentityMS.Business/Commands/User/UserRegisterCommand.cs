using BP.IdentityMS.Business.Enums;
using MediatR;

namespace BP.IdentityMS.Business.Commands.User
{
    public class UserRegisterCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public Role Role {  get; set; }
    }
}
