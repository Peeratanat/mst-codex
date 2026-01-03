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
    public interface IServitudeService : BaseInterfaceService
    {
        Task<ServitudePaging> GetServitudeListAsync(ServitudeFilter filter, PageParam pageParam, ServitudeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<ServitudeDTO> AddServitudeAsync(ServitudeDTO input);
        Task<ServitudeDTO> EditServitudeAsync(ServitudeDTO input);
        Task DeleteServitudeAsync(ServitudeDTO input);
    }
}
