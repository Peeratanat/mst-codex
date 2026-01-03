using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using PRJ_UnitInfos.Services;

namespace PRJ_UnitInfos.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitDocumentService, UnitDocumentService>();
            services.AddScoped<IUnitInfoService, UnitInfoService>();
            services.AddScoped<IHomeInspectionService, HomeInspectionService>();
        }
    }
}