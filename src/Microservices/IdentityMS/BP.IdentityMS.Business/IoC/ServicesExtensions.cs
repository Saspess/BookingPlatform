using System.Reflection;
using BP.IdentityMS.Data.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.IdentityMS.Business.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureIdentityBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentityData(configuration)
                .ConfigureAutoMapper()
                .ConfigureMediatR();

            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
