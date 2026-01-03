using Base.DTOs;
using Base.DTOs.PRM;
using MST_Promotion.Params.Filters;
using MST_Promotion.Params.Outputs;
using PagingExtensions;

namespace MST_Promotion.Services
{
    public interface IMappingAgreementService : BaseInterfaceService
    {
        Task<ImportMappingAgreementDTO> GetMappingAgreementsDataFromExcelAsync(FileDTO input);
        Task<List<MappingAgreementDTO>> ConfirmImportMappingAgreementsAsync(ImportMappingAgreementDTO inputs);
        Task<FileDTO> ExportMappingAgreementsAsync(MappingAgreementFilter filter, MappingAgreementSortByParam sortByParam,CancellationToken cancellationToken = default);
        Task<MappingAgreementPaging> GetMappingAgreementsList(MappingAgreementFilter filter, PageParam pageParam, MappingAgreementSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task DeleteMappingAgreementAsync(Guid id);
        Task<FileDTO> ExportTemplatesMappingAgreementsAsync();
        Task<MappingAgreementDTO> AddMappingAgreementsAsync(MappingAgreementDTO inputs);
    }
}
