using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Auth
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("jwt").Get<JwtOptions>());
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidIssuer = configuration["jwt:issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"]))
                    };
                });
        }
        public static void AddJwt(this IServiceCollection services)
        {
            var JwtOptions = new JwtOptions
            {
                SecretKey = Environment.GetEnvironmentVariable("jwt_SecretKey"),
                ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("jwt_ExpiryMinutes")),
                Issuer = Environment.GetEnvironmentVariable("jwt_Issuer")
            };

            services.AddSingleton(JwtOptions);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false,
                        ValidIssuer = Environment.GetEnvironmentVariable("jwt_Issuer"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwt_SecretKey")))
                    };
                });
        }
    }
}
