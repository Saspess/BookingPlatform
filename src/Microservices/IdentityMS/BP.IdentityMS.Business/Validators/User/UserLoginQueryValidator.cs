using BP.IdentityMS.Business.Queries.User;
using FluentValidation;

namespace BP.IdentityMS.Business.Validators.User
{
    internal class UserLoginQueryValidator : AbstractValidator<UserLoginQuery>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
