using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using Auth_User.Services;
using Auth;

namespace Auth_User.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>(); 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAgentsBCService, AgentsBCService>();
            //services.AddScoped<IJwtHandler, JwtHandler>(); 
            var JwtOptions = new JwtOptions
            {
                SecretKey = Environment.GetEnvironmentVariable("jwt_SecretKey"),
                ExpiryMinutes = int.Parse(Environment.GetEnvironmentVariable("jwt_ExpiryMinutes")),
                Issuer = Environment.GetEnvironmentVariable("jwt_Issuer")
            };

            services.AddSingleton(JwtOptions);
        }
    }
}