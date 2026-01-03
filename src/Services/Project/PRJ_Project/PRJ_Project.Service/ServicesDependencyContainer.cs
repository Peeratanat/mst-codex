
using Microsoft.Extensions.DependencyInjection;
using PRJ_Project.Services;

namespace PRJ_Project.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<IDropdownService, DropdownService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IRoundFeeService, RoundFeeService>();
            services.AddScoped<ITowerService, TowerService>();
        }
    }
}