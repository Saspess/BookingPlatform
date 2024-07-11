using BP.AccountsMS.Business.Services.Contracts;
using Grpc.Core;
using GrpcContracts;

namespace BP.AccountsMS.AccountsApi.Services
{
    public class GrpcUserService : UserService.UserServiceBase
    {
        private readonly IUserAccountService _userAccountService;

        public GrpcUserService(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public override async Task<CreateUserAccountResponse> CreateUserAccount(CreateUserAccountRequest request, ServerCallContext context)
        {
            var result = await _userAccountService.CreateUserAccountAsync(request);
            return result;
        }
    }
}
