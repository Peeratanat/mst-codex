using Microsoft.Extensions.DependencyInjection;
using MST_Auditlog.Services.ContactServices;

namespace MST_Auditlog.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAudtiLogService, AudtiLogService>(); 
        }
    }
}