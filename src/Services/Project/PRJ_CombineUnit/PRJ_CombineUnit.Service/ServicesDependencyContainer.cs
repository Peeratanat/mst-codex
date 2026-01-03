using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using PRJ_CombineUnit.Services;

namespace PRJ_CombineUnit.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICombineUnitService, CombineUnitService>(); 
            
        }
    }
}