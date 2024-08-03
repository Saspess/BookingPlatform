namespace BP.AccountsMS.Data.Entities
{
    public class OneTimePasswordEntity
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; }
        public DateTimeOffset ExpiredAtUtc { get; set; }
    }
}
