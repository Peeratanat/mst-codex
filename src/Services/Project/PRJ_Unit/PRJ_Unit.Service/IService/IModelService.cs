using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;

namespace PRJ_Unit.Services
{
    public interface IModelService : BaseInterfaceService
    {
        Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null, CancellationToken cancellationToken = default);
    }
}
