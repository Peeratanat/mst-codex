using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using MST_General.Services;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface ITypeOfRealEstateService : BaseInterfaceService
    {
        Task<List<TypeOfRealEstateDropdownDTO>> GetTypeOfRealEstateDropdownListAsync(string name, CancellationToken cancellationToken = default);
        Task<TypeOfRealEstatePaging> GetTypeOfRealEstateListAsync(TypeOfRealEstateFilter filter, PageParam pageParam, TypeOfRealEstateSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<TypeOfRealEstateDTO> GetTypeOfRealEstateAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TypeOfRealEstateDTO> CreateTypeOfRealEstateAsync(TypeOfRealEstateDTO input);
        Task<TypeOfRealEstateDTO> UpdateTypeOfRealEstateAsync(Guid id, TypeOfRealEstateDTO input);
        Task DeleteTypeOfRealEstateAsync(Guid id);
    }
}
