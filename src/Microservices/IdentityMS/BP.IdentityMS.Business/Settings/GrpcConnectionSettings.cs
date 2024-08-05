namespace BP.IdentityMS.Business.Settings
{
    internal class GrpcConnectionSettings
    {
        public string AccountsServerAddress { get; set; }
        public string BookingServerAddress { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
