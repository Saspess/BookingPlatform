using BP.IdentityMS.Business.Commands.User;
using FluentValidation;

namespace BP.IdentityMS.Business.Validators.User
{
    internal class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(u => u.LastName)
                .MaximumLength(50);

            RuleFor(u => u.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .NotEmpty()
                .Matches(@"[0-9]+")
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .MinimumLength(8)
                .MaximumLength(50);

            RuleFor(u => u.PasswordConfirmation)
                .NotNull()
                .NotEmpty()
                .Equal(u => u.Password);
        }
    }
}
