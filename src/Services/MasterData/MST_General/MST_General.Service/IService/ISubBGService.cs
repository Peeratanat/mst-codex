using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using PagingExtensions;
using MST_General.Params.Outputs;

namespace MST_General.Services
{
    public interface ISubBGService : BaseInterfaceService
    {
        Task<List<SubBGDropdownDTO>> GetSubBGDropdownListAsync(string name, Guid? bGID, CancellationToken cancellationToken = default);
        Task<SubBGPaging> GetSubBGListAsync(SubBGFilter filter, PageParam pageParam, SubBGSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<SubBGDTO> GetSubBGAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SubBGDTO> CreateSubBGAsync(SubBGDTO input);
        Task<SubBGDTO> UpdateSubBGAsync(Guid id, SubBGDTO input);
        Task DeleteSubBGAsync(Guid id);
    }
}
