using BP.IdentityMS.Data.Constants;
using BP.IdentityMS.Data.Repositories;
using BP.IdentityMS.Data.Repositories.Contracts;
using BP.IdentityMS.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace BP.IdentityMS.Data.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureIdentityData(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepositories()
                .ConfigureMongoDb(configuration)
                .ConfigureDataOptions(configuration);

            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        private static IServiceCollection ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString(ConnectionStrings.MongoDbConnectionString)));
            return services;
        }

        private static IServiceCollection ConfigureDataOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(options => configuration.GetSection(SectionNames.MongoDb).Bind(options));
            return services;
        }
    }
}
