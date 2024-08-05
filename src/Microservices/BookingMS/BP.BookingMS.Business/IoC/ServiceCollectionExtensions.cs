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
                .ConfigureBookingData(configuration);

            return services;
        }
    }
}
