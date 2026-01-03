using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Kattyfaq.Services;

namespace MST_Kattyfaq.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFrequentQuestionsService, FrequentQuestionsService>(); 
            
        }
    }
}