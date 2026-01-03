using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Event.Services;

namespace MST_Event.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IEventService, EventService>(); 
            
        }
    }
}