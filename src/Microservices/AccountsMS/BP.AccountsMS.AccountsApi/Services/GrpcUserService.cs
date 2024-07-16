using BP.AccountsMS.Business.Services.Contracts;
using Grpc.Core;
using GrpcContracts;

namespace BP.AccountsMS.AccountsApi.Services
{
    public class GrpcUserService : UserService.UserServiceBase
    {
        private readonly IAccountService _userAccountService;

        public GrpcUserService(IAccountService userAccountService)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public override async Task<CreateUserAccountResponse> CreateUserAccount(CreateUserAccountRequest request, ServerCallContext context)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var result = await _userAccountService.CreateAccountAsync(request);
            return result;
        }
    }
}
