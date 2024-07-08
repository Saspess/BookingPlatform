using AutoMapper;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using MediatR;

namespace BP.IdentityMS.Business.Handlers.User
{
    internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, string>
    {
        private readonly IGenericRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;

        public UserRegisterCommandHandler(IGenericRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<string> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            // TODO: Add personal data sending

            var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == command.Email);

            if (existingUser != null)
            {
                throw new ServerConflictException(ExceptionMessages.UserAlreadyExists);
            }

            var userEntity = _mapper.Map<UserEntity>(command);
            var createdEntityId = await _userRepository.CreateAsync(userEntity);

            return createdEntityId;
        }
    }
}
