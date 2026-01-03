using Database.Models.MST;
using MST_Finacc.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PagingExtensions;
using MST_Finacc.Params.Outputs;
using Base.DTOs;

namespace MST_Finacc.Services
{
    public interface IBankBranchBOTService : BaseInterfaceService
    {
        Task<BankBranchBOTPaging> GetBankBranchBOTListAsync(BankBranchBOTFilter filter, PageParam pageParam, BankBranchBOTSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BankBranchBOTDTO> GetBankBranchBOTAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BankBranchBOTDTO> CreateBankBranchBOTAsync(BankBranchBOTDTO input);
        Task<BankBranchBOTDTO> UpdateBankBranchBOTAsync(Guid id, BankBranchBOTDTO input);
        Task<BankBranchBOT> DeleteBankBranchBOTAsync(Guid id);

    }
}
