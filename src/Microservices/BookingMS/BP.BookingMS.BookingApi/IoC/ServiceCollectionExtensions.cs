using BP.BookingMS.Business.IoC;
using BP.Api.Common.IoC;

namespace BP.BookingMS.BookingApi.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBookingApi(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureBookingBusiness(configuration)
                .ConfigureGrpc()
                .ConfigureCurrentUserService()
                .ConfigureSwagger();

            return services;
        }

        private static IServiceCollection ConfigureGrpc(this IServiceCollection services)
        {
            services.AddGrpc();
            return services;
        }
    }
}
