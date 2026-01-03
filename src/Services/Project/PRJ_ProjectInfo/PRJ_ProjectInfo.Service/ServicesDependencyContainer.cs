using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using PRJ_ProjectInfo.Services;

namespace PRJ_ProjectInfo.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IProjectInfoService, ProjectInfoService>();

        }
    }
}