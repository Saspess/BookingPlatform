using System.Text;
using BP.AuthProvider.Constants;
using BP.AuthProvider.Services;
using BP.AuthProvider.Services.Contracts;
using BP.AuthProvider.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BP.AuthProvider.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureAuthProvider(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAuthServices()
                .ConfigureAuthOptions(configuration)
                .ConfigureAuthentication(configuration);
            return services;
        }

        private static IServiceCollection ConfigureAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthProviderService, AuthProviderService>();
            return services;
        }

        private static IServiceCollection ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthSettings>(options => configuration.GetSection(SectionNames.AuthSettings).Bind(options));
            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = configuration.GetSection(SectionNames.AuthSettings).Get<AuthSettings>();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = authSettings.ValidateIssuer,
                    ValidIssuer = authSettings.Issuer,
                    ValidateAudience = authSettings.ValidateAudience,
                    ValidAudience = authSettings.Audience,
                    ValidateIssuerSigningKey = authSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Key))
                };
            });

            return services;
        }
    }
}
