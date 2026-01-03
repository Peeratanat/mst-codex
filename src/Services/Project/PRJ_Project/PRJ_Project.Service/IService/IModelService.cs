using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Inputs;
using PRJ_Project.Params.Outputs;

namespace PRJ_Project.Services
{
    public interface IModelService : BaseInterfaceService
    {
        Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null, CancellationToken cancellationToken = default);
        Task<ModelPaging> GetModelListAsync(Guid projectID, ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ModelPaging> GetModelListAllAsync(Guid projectID, ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ModelDTO> GetModelAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default);
        Task<ModelDTO> CreateModelAsync(Guid projectID, ModelDTO input);
        Task<ModelDTO> UpdateModelAsync(Guid projectID, Guid id, ModelDTO input);
        Task<ModelDTO> UpdateModelListAsync(Guid projectID, List<ModelDTO> inputs, List<Guid> ids);
        Task<Model> DeleteModelAsync(Guid projectID, Guid id);
    }
}
