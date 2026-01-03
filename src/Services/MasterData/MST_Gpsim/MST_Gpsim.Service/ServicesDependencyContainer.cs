using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Gpsim.Services;

namespace MST_Gpsim.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IGPSimulateService, GPSimulateService>(); 
            services.AddScoped<IReallocateMinPriceService, ReallocateMinPriceService>(); 
        }
    }
}