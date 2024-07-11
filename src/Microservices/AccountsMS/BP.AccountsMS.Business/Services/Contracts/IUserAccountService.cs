using GrpcContracts;

namespace BP.AccountsMS.Business.Services.Contracts
{
    public interface IUserAccountService
    {
        Task<CreateUserAccountResponse> CreateUserAccountAsync(CreateUserAccountRequest createUserAccountRequest);
    }
}
