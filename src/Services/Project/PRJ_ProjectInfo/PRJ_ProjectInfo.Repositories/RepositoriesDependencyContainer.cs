using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using PRJ_ProjectInfo.Repositories;

namespace PRJ_ProjectInfo.Repositories
{
    public class MSTGeneralRepositoriesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}