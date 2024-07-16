using BP.Api.Common.Constants;
using BP.Api.Common.Services;
using BP.Business.Common.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BP.Api.Common.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor()
                .AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme()
                {
                    BearerFormat = SecuritySchemeSettings.BearerFormat,
                    Name = SecuritySchemeSettings.Name,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = SecuritySchemeSettings.Description,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        jwtSecurityScheme,
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}
