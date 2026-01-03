using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface ILegalEntityService : BaseInterfaceService
    {
        Task<List<LegalEntityDropdownDTO>> GetLegalEntityDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<LegalEntityPaging> GetLegalEntityListAsync(LegalFilter filter, PageParam pageParam, LegalEntitySortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<LegalEntityDTO> GetLegalEntityAsync(Guid id, Guid? projectID, CancellationToken cancellationToken = default);
        Task<LegalEntityDTO> CreateLegalEntityAsync(LegalEntityDTO input);
        Task<LegalEntityDTO> UpdateLegalEntityAsync(Guid id, LegalEntityDTO input);
        Task DeleteLegalEntityAsync(Guid id);
    }
}
