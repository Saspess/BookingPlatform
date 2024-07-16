﻿namespace BP.AccountsMS.Business.Models.Account
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Role { get; set; }
    }
}
