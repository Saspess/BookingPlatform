using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BP.EmailSender.Constants;
using BP.EmailSender.Settings;
using BP.EmailSender.Services;
using BP.EmailSender.Services.Contracts;

namespace BP.EmailSender.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureEmailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureEmailServices()
                .ConfigureEmailOptions(configuration);

            return services;
        }

        private static IServiceCollection ConfigureEmailServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailSendingService, EmailSendingService>();
            return services;
        }

        private static IServiceCollection ConfigureEmailOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(options => configuration.GetSection(SectionNames.EmailSettings).Bind(options));
            return services;
        }
    }
}
