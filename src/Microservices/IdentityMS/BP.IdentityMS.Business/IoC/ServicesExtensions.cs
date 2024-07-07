using System.Reflection;
using BP.IdentityMS.Data.IoC;
using FluentValidation;
using FluentValidation.AspNetCore;
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
                .ConfigureMediatR()
                .ConfigureFluentValidation();

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

        private static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            return services;
        }
    }
}
