using Database.Models.MST;
using Microsoft.Extensions.DependencyInjection;
using MST_General.Services;

namespace MST_General.Service
{
    public class ServicesDependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IBGService, BGService>();
            services.AddScoped<ISubBGService, SubBGService>();
            services.AddScoped<IAgentsService, AgentsService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<ILandOfficeService, LandOfficeService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            services.AddScoped<ISubDistrictService, SubDistrictService>();
            services.AddScoped<ITypeOfRealEstateService, TypeOfRealEstateService>();
            services.AddScoped<ILegalEntityService, LegalEntityService>();
            services.AddScoped<IMasterCenterGroupService, MasterCenterGroupService>();
            services.AddScoped<IMasterCenterService, MasterCenterService>();
            services.AddScoped<IMasterPriceItemService, MasterPriceItemService>();
            services.AddScoped<IAgentEmployeeService, AgentEmployeeService>();
            services.AddScoped<IBOConfigurationService, BOConfigurationService>();
            services.AddScoped<IAttorneyTransferService, AttorneyTransferService>();
            services.AddScoped<ICancelReasonService, CancelReasonService>();
            services.AddScoped<ICancelReturnSettingService, CancelReturnSettingService>();
            services.AddScoped<IServitudeService, ServitudeService>();
            services.AddScoped<ISpecMaterialService, SpecMaterialService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAgentsExternalService, AgentsExternalService>();
            services.AddScoped<IAgentsEmployeeExternalService, AgentsExternalEmployeesService>();
            services.AddScoped<IGLAccountTypeService, GLAccountTypeService>();
        }
    }
}