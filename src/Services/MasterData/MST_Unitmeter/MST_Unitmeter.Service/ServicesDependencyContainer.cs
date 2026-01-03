using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Unitmeter.Services;

namespace MST_Unitmeter.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitMeterService, UnitMeterService>(); 
        }
    }
}