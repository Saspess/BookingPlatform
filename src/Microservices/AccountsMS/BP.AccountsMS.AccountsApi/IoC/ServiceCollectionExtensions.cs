using BP.AccountsMS.Business.IoC;
using BP.Api.Common.IoC;

namespace BP.AccountsMS.AccountsApi.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAccountsApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAccountsBusiness(configuration)
                .ConfigureGrpc()
                .ConfigureCurrentUserService()
                .ConfigureSwagger();

            return services;
        }

        private static IServiceCollection ConfigureGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            return services;
        }
    }
}
