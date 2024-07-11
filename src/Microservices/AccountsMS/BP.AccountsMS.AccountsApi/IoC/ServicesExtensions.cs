using BP.AccountsMS.Business.IoC;

namespace BP.AccountsMS.AccountsApi.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureAccountsApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAccountsBusiness(configuration)
                .ConfigureGrpc();

            return services;
        }

        private static IServiceCollection ConfigureGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            return services;
        }
    }
}
