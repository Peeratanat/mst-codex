
using Database.Models.PRJ;
using Microsoft.Extensions.DependencyInjection;
using PRJ_Unit.Services;


namespace PRJ_Unit.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IHighRiseFeeService, HighRiseFeeService>();
            services.AddScoped<ILockFloorService, LockFloorService>();
            services.AddScoped<ILockUnitService, LockUnitService>();
            services.AddScoped<ILowRiseFeeService, LowRiseFeeService>();
            services.AddScoped<IMinPriceService, MinPriceService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IPriceListService, PriceListService>();
            services.AddScoped<ITitleDeedService, TitleDeedService>();
            services.AddScoped<IUnitControlService, UnitControlService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IWaiveQCService, WaiveQCService>();
            services.AddScoped<IWaiveCustomerSignService, WaiveCustomerSignService>();
            services.AddScoped<IWaterElectricMeterPriceService, WaterElectricMeterPriceService>();
            services.AddScoped<ILowRiseBuildingPriceFeeService, LowRiseBuildingPriceFeeService>();
            services.AddScoped<ILowRiseFenceFeeService, LowRiseFenceFeeService>();
            services.AddScoped<IPreBookService, PreBookService>();
        }
    }
}