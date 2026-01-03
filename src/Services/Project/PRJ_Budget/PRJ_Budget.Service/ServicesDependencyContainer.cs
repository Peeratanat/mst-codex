using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using PRJ_Budget.Services;

namespace PRJ_Budget.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBudgetMinPriceService, BudgetMinPriceService>();
            services.AddScoped<IBudgetPromotionService, BudgetPromotionService>();
            
        }
    }
}