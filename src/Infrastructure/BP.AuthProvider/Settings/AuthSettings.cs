namespace BP.AuthProvider.Settings
{
    internal class AuthSettings
    {
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
        public string Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public string Key { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int TokenLifetimeMinutes { get; set; }
    }
}
