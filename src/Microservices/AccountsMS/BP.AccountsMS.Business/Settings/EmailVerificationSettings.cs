namespace BP.AccountsMS.Business.Settings
{
    internal class EmailVerificationSettings
    {
        public int OneTimePasswordLength { get; set; }
        public int OneTimePasswordLifetimeMinutes { get; set; }
    }
}
