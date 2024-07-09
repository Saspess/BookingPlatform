using AutoMapper;
using BP.AuthProvider.Models;
using BP.AuthProvider.Services.Contracts;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.IdentityMS.Business.Queries.User;
using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using BP.Utils.Helpers;
using MediatR;

namespace BP.IdentityMS.Business.Handlers.User
{
    internal class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, TokenModel>
    {
        private readonly IGenericRepository<UserEntity> _userRepository;
        private readonly IAuthProviderService _authProviderService;
        private readonly IMapper _mapper;

        public UserLoginQueryHandler(IGenericRepository<UserEntity> userRepository, IMapper mapper, IAuthProviderService authProviderService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authProviderService = authProviderService ?? throw new ArgumentNullException(nameof(authProviderService));

        }

        public async Task<TokenModel> Handle(UserLoginQuery query, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));

            var existingUser = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == query.Email)
                ?? throw new NotFoundException(ExceptionMessages.UserNotFound);

            var isPasswordValid = PasswordHelper.VerifyPassword(query.Password, existingUser.Password);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialsException(ExceptionMessages.InvalidCredentials);
            }

            var authModel = _mapper.Map<AuthModel>(existingUser);
            var token = _authProviderService.GenerateToken(authModel);
            return token;
        }
    }
}
