using MST_General.Params.Filters;
using Base.DTOs.MST;
using MST_General.Params.Outputs;
using PagingExtensions;
using Database.Models.DbQueries.MST;

namespace MST_General.Services
{
    public interface IMasterCenterService : BaseInterfaceService
    {
        Task<List<MasterCenterDropdownDTO>> GetMasterCenterDropdownListAsync(string masterCenterGroupKey, string name, string bg,string brandId, CancellationToken cancellationToken = default);
        Task<MasterCenterPaging> GetMasterCenterListAsync(MasterCenterFilter filter, PageParam pageParam, MasterCenterSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterCenterDropdownDTO> GetFindMasterCenterDropdownItemAsync(string masterCenterGroupKey, string key, CancellationToken cancellationToken = default);
        Task<MasterCenterDTO> GetMasterCenterAsync(Guid id, CancellationToken cancellationToken = default);
        Task<MasterCenterDTO> CreateMasterCenterAsync(MasterCenterDTO input);
        Task<MasterCenterDTO> UpdateMasterCenterAsync(Guid id, MasterCenterDTO input);
        Task DeleteMasterCenterAsync(Guid id);

        Task<List<BankBranchBOTDropdownDTO>> GetBankBranchDropdownAsync(string bankCode, string bankBramchName, CancellationToken cancellationToken = default);
        Task<List<MasterCenterDropdownDTO>> GetLGMasterCenterDropdownListAsync(string masterCenterGroupKey, string name, decimal countNumber, CancellationToken cancellationToken = default);
    }
}
