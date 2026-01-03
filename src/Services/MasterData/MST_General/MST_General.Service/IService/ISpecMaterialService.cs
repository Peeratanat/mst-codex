using Base.DTOs;
using Base.DTOs.CMS;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models.MST;
using MST_General.Params.Filters;
using MST_General.Params.Outputs;
using PagingExtensions;
using Report.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface ISpecMaterialService : BaseInterfaceService
    {
        Task<SpecMaterialCollectionPaging> GetSpecMaterialCollectionListAsync(SpecMaterialCollectionFilter filter, PageParam pageParam, SpecMaterialCollectionSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<SpecMaterialCollectionDetailPaging> GetSpecMaterialCollectionDetailAsync(Guid id, SpecMaterialCollectionDetailFilter filter, PageParam pageParam, SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<List<ModelDropdownDTO>> GetUnitModelByProjectAsync(Guid projectid, Guid collectionID, CancellationToken cancellationToken = default);
        Task<SpecMaterialCollectionDTO> EditSpecMaterialCollectionAsync(Guid ID,Guid projectID, bool IsActive, string Name, List<SpecMaterialCollectionDetailDTO> model);
        Task DeleteSpecMaterialItemAsync(Guid id);

        Task<SpecMaterialCollectionDetailPaging> GetAllSpecMaterialCollectionItemsAsync(SpecMaterialCollectionDetailFilter filter, PageParam pageParam, SpecMaterialCollectionDetailSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<SpecMaterialItemDTO> AddSpecMaterialItemsAsync(SpecMaterialCollectionDTO input);
        Task DeleteSpecMaterialCollectionAsync(Guid id);

        Task<SpecMaterialItemDTO> EditSpecMaterialItemsAsync(SpecMaterialCollectionDTO input);
        Task<SpecMaterialItemDTO> GetSpecMaterialItemByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SpecMaterialCollectionDetailDTO> GetSpecMaterialDetailByItemIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<FileDTO> ExportTemplateSpecMaterialAsync(Guid ProjectID);
        Task<FileDTO> ExportSpecMaterialDetailAsync(Guid SpecMaterialCollectionID);
        Task<SpecMaterialCollectionDTO> GetSpecMaterialCollectionAsync(Guid id, CancellationToken cancellationToken = default);
        Task<SpecMaterialExcelDTO> ImportSpecMaterialExcel(Guid projectID, FileDTO input, Guid? userID);

        
        Task<ReportResult> ExportSpecMaterialTMPPrintFormUrlAsync(Guid ID, bool IsEN = false);
        Task<bool> AddErrorRecordAsync(List<ImportError> input);


        Task<bool> UpdateCollectionAgreementAsync(Guid agreementID);
        Task<ReportResult> ExportSpecMaterialPrintFormUrlAsync(Guid ID, bool IsEN = false);


    }
}
