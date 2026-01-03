using MST_General.Params.Filters;
using Base.DTOs.MST;
using MST_General.Params.Outputs;
using PagingExtensions;

namespace MST_General.Services
{
    public interface IMasterCenterGroupService : BaseInterfaceService
    {
        Task<MasterCenterGroupPaging> GetMasterCenterGroupListAsync(MasterCenterGroupFilter filter, PageParam pageParam, MasterCenterGroupSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<MasterCenterGroupDTO> GetMasterCenterGroupAsync(string id, CancellationToken cancellationToken = default);
        Task<MasterCenterGroupDTO> CreateMasterCenterGroupAsync(MasterCenterGroupDTO input);
        Task<MasterCenterGroupDTO> UpdateMasterCenterGroupAsync(string key, MasterCenterGroupDTO input);
        Task DeleteMasterCenterGroupAsync(string id);
    }
}
