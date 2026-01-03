using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Titledeeds.Services;

namespace MST_Titledeeds.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITitleDeedService, TitleDeedService>(); 
            
        }
    }
}