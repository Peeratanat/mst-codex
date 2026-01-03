using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using Auth_RolePermission.Services;

namespace Auth_RolePermission.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPermissionService, PermissionService>(); 
            
        }
    }
}