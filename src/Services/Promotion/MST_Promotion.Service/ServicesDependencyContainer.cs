using Microsoft.Extensions.DependencyInjection;
using MST_Promotion.Services;

namespace MST_Promotion.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMappingAgreementService, MappingAgreementService>(); 
            services.AddScoped<IMasterPreSalePromotionService, MasterPreSalePromotionService>(); 
            services.AddScoped<IMasterSalePromotionService, MasterSalePromotionService>(); 
            services.AddScoped<IMasterTransferPromotionService, MasterTransferPromotionService>(); 
            
        }
    }
}