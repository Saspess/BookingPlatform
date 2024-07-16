namespace BP.AccountsMS.Business.Services.Contracts
{
    public interface IEmailService
    {
        Task RequestVerificationCodeAsync();
    }
}
