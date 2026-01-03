using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_Lg.Params.Filters;
using MST_Lg.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Lg.Services
{
    public interface ILetterOfGuaranteeService: BaseInterfaceService
    {
        Task<LetterOfGuaranteePaging> GetLetterOfGuaranteeAsync(LetterOfGuaranteeFilter filter, PageParam pageParam, LetterOfGuaranteeSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<LetterOfGuaranteeDTO> AddLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input);
        Task<LetterOfGuaranteeDTO> EditLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID);
        Task<bool> DeleteLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input);
        Task<LetterOfGuaranteeDTO> CancelLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID);
        Task<LetterOfGuaranteeDTO> CancelCancelLetterOfGuaranteeAsync(LetterOfGuaranteeDTO input, Guid? userID);
        Task<List<LetterGuaranteeFileDTO>> GetLetterGuaranteeFileListAsync(Guid LetterGuaranteeID, CancellationToken cancellationToken = default);
        Task<string> DeleteLetterGuaranteeFileAsync(Guid id);
        Task<string> MoveFile();
        Task<bool> AddLGGuarantor(MasterCenterDTO input);
    }
}
