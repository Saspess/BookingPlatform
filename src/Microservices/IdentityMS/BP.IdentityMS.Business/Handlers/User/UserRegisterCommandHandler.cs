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

            var existingUser = await _userRepository.GetFirstOrDefaultAsync(u => u.Email == command.Email);

            if (existingUser != null)
            {
                throw new ServerConflictException(ExceptionMessages.UserAlreadyExists);
            }

            using var accountsChannel = GetChannel(_grpcConnectionSettings.AccountsServerAddress, _grpcConnectionSettings.ConnectionTimeout);
            var createUserAccountRequest = _mapper.Map<CreateUserAccountRequest>(command);
            var userServiceClient = new UserService.UserServiceClient(accountsChannel);
            var accountCreationResponse = userServiceClient.CreateUserAccount(createUserAccountRequest, cancellationToken: cancellationToken);

            if (!accountCreationResponse.IsSuccessfullyCreated)
            {
                throw new ServerConflictException(accountCreationResponse?.ExceptionMessage);
            }

            using var bookingChannel = GetChannel(_grpcConnectionSettings.BookingServerAddress, _grpcConnectionSettings.ConnectionTimeout);
            var createPartyRequest = _mapper.Map<CreatePartyRequest>(command);
            var partyServiceClient = new PartyService.PartyServiceClient(bookingChannel);
            var partyCreationResponse = partyServiceClient.CreateParty(createPartyRequest, cancellationToken: cancellationToken);

            if (!partyCreationResponse.IsSuccessfullyCreated)
            {
                throw new ServerConflictException(accountCreationResponse?.ExceptionMessage);
            }
            
            var userEntity = _mapper.Map<UserEntity>(command);
            var createdEntityId = await _userRepository.CreateAsync(userEntity);
            return createdEntityId;
        }

        private GrpcChannel GetChannel(string address, int connectionTimeout)
        {
            var channel = GrpcChannel.ForAddress(
                address,
                new GrpcChannelOptions
                {
                    DisposeHttpClient = true,
                    HttpClient = new HttpClient(new SocketsHttpHandler
                    {
                        ConnectTimeout = TimeSpan.FromSeconds(connectionTimeout)
                    })
                });

            return channel;
        }
    }
}
