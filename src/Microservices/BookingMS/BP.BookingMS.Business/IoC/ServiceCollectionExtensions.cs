using System.Reflection;
using BP.BookingMS.Business.Services;
using BP.BookingMS.Business.Services.Contracts;
using BP.BookingMS.Data.IoC;
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
                .ConfigureServices()
                .ConfigureAutoMapper();

            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPartyService, PartyService>();
            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
