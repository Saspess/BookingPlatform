namespace BP.Business.Common.Services.Contracts
{
    public interface ICurrentUserService
    {
        string GetEmail();
        string GetRole();
    }
}
