namespace BP.AccountsMS.Business.Services.Contracts
{
    public interface IEmailService
    {
        Task RequestVerificationCodeAsync();
        Task VerifyEmailAsync(string emailVerificationCode);
    }
}
