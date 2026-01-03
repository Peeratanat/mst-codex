using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Lg.Services;

namespace MST_Lg.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILetterOfGuaranteeService, LetterOfGuaranteeService>(); 
            services.AddScoped<KafkaService>(); 
            
        }
    }
}