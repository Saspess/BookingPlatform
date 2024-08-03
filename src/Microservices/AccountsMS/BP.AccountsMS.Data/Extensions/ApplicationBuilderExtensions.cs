using BP.AccountsMS.Data.Constants;
using BP.AccountsMS.Data.Init;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.AccountsMS.Data.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> MigrateAsync(this IApplicationBuilder app, IConfiguration configuration)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString(ConnectionStrings.SqlConnectionString));

            await DbInitializer.InitializaDatabaseAsync(
                configuration.GetConnectionString(ConnectionStrings.MasterConnectionString), 
                sqlConnectionStringBuilder.InitialCatalog);

            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();

            return app;
        }
    }
}
