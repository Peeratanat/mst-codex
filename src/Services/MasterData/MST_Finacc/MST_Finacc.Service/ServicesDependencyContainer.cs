using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_Finacc.Services;

namespace MST_Finacc.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBankAccountService, BankAccountService>(); 
            services.AddScoped<IBankBranchBOTService, BankBranchBOTService>(); 
            services.AddScoped<IBankBranchService, BankBranchService>(); 
            services.AddScoped<IBankService, BankService>(); 
            services.AddScoped<IEDCService, EDCService>(); 
            
        }
    }
}