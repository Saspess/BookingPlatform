using BP.BookingMS.Data.Constants;
using BP.BookingMS.Data.Contexts;
using BP.BookingMS.Data.Contexts.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.BookingMS.Data.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBookingData(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureDbContext(configuration);

            return services;
        }

        private static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionStrings.SqlConnectionString)));

            return services;
        }
    }
}
