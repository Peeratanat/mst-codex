using Base.DTOs.MST;

namespace MST_General.Services
{
    public interface IMasterPriceItemService : BaseInterfaceService
    {
        Task<List<MasterPriceItemDTO>> GetMasterPriceItemDropdownListAsync(string detail, CancellationToken cancellationToken = default);
    }
}
