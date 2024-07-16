namespace BP.Business.Common.Services.Contracts
{
    public interface ICurrentUserService
    {
        string Email { get; }
        string Role { get; }
    }
}
