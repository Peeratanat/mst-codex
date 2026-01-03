using Base.DTOs;
using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using MST_Titledeeds.Params.Filters;
using MST_Titledeeds.Params.Inputs;
using MST_Titledeeds.Params.Outputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Report.Integration;

namespace MST_Titledeeds.Services
{
	public interface ITitleDeedService : BaseInterfaceService
	{
		Task<TitleDeedPaging> GetTitleDeedListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default);
		Task<TitleDeedPaging> GetTitleDeedStatusListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default);
		Task<TitleDeedPaging> GetTitleDeedStatusSelectAllListAsync(Guid? projectID, TitleDeedFilter filter, PageParam pageParam, TitleDeedListSortByParam sortByParam, CancellationToken cancellationToken = default);
		Task<TitleDeedDTO> GetTitleDeedAsync(Guid id, CancellationToken cancellationToken = default);
		Task<TitleDeedDTO> UpdateTitleDeedStatusAsync(Guid id, TitleDeedDTO input);
		Task<TitleDeedDTO> UpdateTitleDeedListStatusAsync(Guid id, List<TitleDeedDTO> input);
		Task<List<TitleDeedDTO>> GetTitleDeedHistoryItemsAsync(Guid id, CancellationToken cancellationToken = default);
		Task<ReportResult> ExportDebtFreePrintFormUrlAsync(TitleDeedReportDTO input, Guid? userID);
		Task<TitleDeedHistoryExcelDTO> ImportTitleDeedHistoryAsync(Guid projectID, FileDTO input, Guid? UserID = null);
		Task<FileDTO> ExportTitleDeedStatusAsync(Guid projectID, TitleDeedDTO input, CancellationToken cancellationToken = default);

    }
}
