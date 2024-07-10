using System.Data;
using BP.AccountsMS.Data.Constants;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.AccountsMS.Data.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureAccountsData(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureUnitOfWork(configuration);

            return services;
        }

        private static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString(ConnectionStrings.SqlConnectionString)));
            services.AddScoped<IUnitOfWork, BP.AccountsMS.Data.UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
