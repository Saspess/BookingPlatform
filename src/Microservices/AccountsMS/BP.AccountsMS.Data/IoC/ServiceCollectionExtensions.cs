using System.Data;
using System.Reflection;
using BP.AccountsMS.Data.Constants;
using BP.AccountsMS.Data.UnitOfWork.Contracts;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.AccountsMS.Data.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAccountsData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureUnitOfWork(configuration)
                .ConfigureFluentMigrator(configuration);

            return services;
        }

        private static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString(ConnectionStrings.SqlConnectionString)));
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            return services;
        }

        private static IServiceCollection ConfigureFluentMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddLogging(c => c.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSqlServer()
                    .WithGlobalConnectionString(configuration.GetConnectionString(ConnectionStrings.SqlConnectionString))
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All());

            return services;
        }
    }
}
