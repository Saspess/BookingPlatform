using BP.AccountsMS.Business.Models.Account;
using GrpcContracts;

namespace BP.AccountsMS.Business.Services.Contracts
{
    public interface IAccountService
    {
        Task<AccountViewModel> GetAccountAsync();
        Task<CreateUserAccountResponse> CreateAccountAsync(CreateUserAccountRequest createUserAccountRequest);
    }
}
