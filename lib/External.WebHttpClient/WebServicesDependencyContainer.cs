using External.WebHttpClient; 
using Microsoft.Extensions.DependencyInjection;

namespace APSharing.WebHttpClient
{
    public class WebServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        { 
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizeService, AuthorizeService>();
        }
    }
}
