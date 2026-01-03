using Base.DTOs.CTM;
using CTM_CustomerConsent.Params.Filters;
using CTM_CustomerConsent.Params.Outputs;
using PagingExtensions;
using System.Threading.Tasks;

namespace CTM_CustomerConsent.Services.CustomerConsentService
{
    public interface ICustomerConsentService : BaseInterfaceService
    {
        Task UpdateCustomerConsentAsync(UpdateCustomerConsentDTO input);
        Task<ConsentListPaging> GetConSentListAsync(ConsentFilter filter, PageParam pageParam, ConsentListSortByParam sortByParam, CancellationToken cancellationToken = default);
    }
}
