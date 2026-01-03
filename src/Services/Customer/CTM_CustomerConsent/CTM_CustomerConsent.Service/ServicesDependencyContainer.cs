using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using CTM_CustomerConsent.Services;
using CTM_CustomerConsent.Services.CustomerConsentService;

namespace CTM_CustomerConsent.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICustomerConsentService, CustomerConsentService>(); 
        }
    }
}