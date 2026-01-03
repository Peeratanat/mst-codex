using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Inputs;
using PRJ_Unit.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_Unit.Services
{
	public interface ITitleDeedService : BaseInterfaceService
	{
		Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default);
		Task<TitleDeedDTO> GetTitleDeedAsync(Guid id, CancellationToken cancellationToken = default);
		Task<TitleDeedDTO> CreateTitleDeedAsync(Guid projectID, TitleDeedDTO input);
		Task<TitleDeedDTO> UpdateTitleDeedAsync(Guid projectID, Guid id, TitleDeedDTO input);
		Task<TitledeedExcelDTO> ImportTitleDeedAsync(Guid projectID, FileDTO input, Guid? UserID = null);
		Task<FileDTO> ExportExcelTitleDeedAsync(Guid projectID, CancellationToken cancellationToken = default);
		Task<TitledeedDetail> DeleteTitleDeedAsync(Guid projectID, Guid id);
		Task UpdateMultipleHouseNosAsync(Guid projectID, UpdateMultipleHouseNoParam input);
		Task UpdateMultipleLandOfficesAsync(Guid projectID, UpdateMultipleLandOfficeParam input);
		Task UpdateMultipleHouseNoReceivedYearAsync(Guid projectID, UpdateMultipleHouseNoReceivedYearParam input);

        Task<SyncTitledeedFromLandResponse> SyncTitledeedFromLandAsync(string projectNo, CancellationToken cancellationToken = default);
    }
}
