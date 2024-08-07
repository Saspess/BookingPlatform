using System.Reflection;
using BP.AuthProvider.IoC;
using BP.BookingMS.Business.Services;
using BP.BookingMS.Business.Services.Contracts;
using BP.BookingMS.Data.IoC;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.BookingMS.Business.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBookingBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureBookingData(configuration)
                .ConfigureAuthProvider(configuration)
                .ConfigureServices()
                .ConfigureAutoMapper()
                .ConfigureFluentValidation();

            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services
                .AddScoped<IPartyService, PartyService>()
                .AddScoped<IHotelService, HotelService>();

            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            return services;
        }
    }
}
