using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using BP.Utils.Helpers;
using MediatR;

namespace BP.IdentityMS.Business.Handlers.User
{
    internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, string>
    {
        private readonly IGenericRepository<UserEntity> _userRepository;

        public UserRegisterCommandHandler(IGenericRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<string> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);

            // TODO: Add personal data sending
            // TODO: Check for existing account

            var userEntity = new UserEntity()
            {
                Email = command.Email,
                Password = PasswordHelper.HashPassword(command.Password, PasswordHelper.GenerateSalt())
            };

            var createdEntityId = await _userRepository.CreateAsync(userEntity);
            return createdEntityId;
        }
    }
}
