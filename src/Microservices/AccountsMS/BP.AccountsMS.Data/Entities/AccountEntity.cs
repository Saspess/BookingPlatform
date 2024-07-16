namespace BP.AccountsMS.Data.Entities
{
    public class AccountEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public string Role { get; set; }
    }
}
