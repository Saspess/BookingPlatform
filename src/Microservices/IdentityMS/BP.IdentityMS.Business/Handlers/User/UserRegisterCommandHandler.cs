using AutoMapper;
using BP.Business.Common.Constants;
using BP.Business.Common.Exceptions;
using BP.IdentityMS.Business.Commands.User;
using BP.IdentityMS.Business.Settings;
using BP.IdentityMS.Data.Entities;
using BP.IdentityMS.Data.Repositories.Contracts;
using Grpc.Net.Client;
using GrpcContracts;
using MediatR;
using Microsoft.Extensions.Options;

namespace BP.IdentityMS.Business.Handlers.User
{
    internal class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, string>
    {
        private readonly IGenericRepository<UserEntity> _userRepository;
        private readonly IMapper _mapper;
        private readonly GrpcConnectionSettings _grpcConnectionSettings;

        public UserRegisterCommandHandler(IGenericRepository<UserEntity> userRepository, IMapper mapper, IOptions<GrpcConnectionSettings> grpcConnectionSettings)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _grpcConnectionSettings = grpcConnectionSettings.Value ?? throw new ArgumentNullException(nameof(grpcConnectionSettings));
        }

        public async Task<string> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var createUserAccountRequest = _mapper.Map<CreateUserAccountRequest>(command);

            using var channel = GrpcChannel.ForAddress(_grpcConnectionSettings.ServerAddress, new GrpcChannelOptions
            {
                DisposeHttpClient = true,
                HttpClient = new HttpClient(new SocketsHttpHandler
                {
                    ConnectTimeout = TimeSpan.FromSeconds(_grpcConnectionSettings.ConnectionTimeout)
                })
            });

            var client = new UserService.UserServiceClient(channel);
            var accountCreationResponse = client.CreateUserAccount(createUserAccountRequest, cancellationToken: cancellationToken);

            if (!accountCreationResponse.IsSuccessfullyCreated)
            {
                throw new ServerConflictException(accountCreationResponse?.ExceptionMessage);
            }

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
