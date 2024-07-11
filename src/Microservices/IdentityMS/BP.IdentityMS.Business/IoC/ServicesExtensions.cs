using System.Reflection;
using BP.IdentityMS.Data.IoC;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BP.AuthProvider.IoC;
using BP.IdentityMS.Business.Settings;
using BP.IdentityMS.Business.Constants;

namespace BP.IdentityMS.Business.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureIdentityBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureIdentityData(configuration)
                .ConfigureAutoMapper()
                .ConfigureMediatR()
                .ConfigureFluentValidation()
                .ConfigureAuthProvider(configuration)
                .ConfigureBusinessOptions(configuration);

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

        private static IServiceCollection ConfigureBusinessOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GrpcConnectionSettings>(options => configuration.GetSection(SectionNames.GrpcSettings).Bind(options));
            return services;
        }
    }
}
