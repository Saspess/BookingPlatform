using System.Reflection;
using BP.AccountsMS.Business.Services;
using BP.AccountsMS.Business.Services.Contracts;
using BP.AccountsMS.Data.IoC;
using BP.AuthProvider.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.AccountsMS.Business.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAccountsBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAccountsData(configuration)
                .ConfigureAutoMapper()
                .ConfigureServices()
                .ConfigureAuthProvider(configuration);

            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
    }
}
