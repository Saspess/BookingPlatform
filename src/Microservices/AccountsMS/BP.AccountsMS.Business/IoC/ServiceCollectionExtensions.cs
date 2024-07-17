using System.Reflection;
using BP.AccountsMS.Business.Constants;
using BP.AccountsMS.Business.Services;
using BP.AccountsMS.Business.Services.Contracts;
using BP.AccountsMS.Business.Settings;
using BP.AccountsMS.Data.IoC;
using BP.AuthProvider.IoC;
using BP.EmailSender.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BP.AccountsMS.Business.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureAccountsBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAccountsData(configuration)
                .ConfigureAutoMapper()
                .ConfigureServices()
                .ConfigureAuthProvider(configuration)
                .ConfigureEmailSender(configuration)
                .ConfigureOptions(configuration);

            return services;
        }

        private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }

        private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailVerificationSettings>(options => configuration.GetSection(SectionNames.EmailVerificationSettings).Bind(options));
            return services;
        }
    }
}
