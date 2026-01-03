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
    public interface IBankBranchService : BaseInterfaceService
    {
        Task<List<BankBranchDropdownDTO>> GetBankBrachDropdownListAsync(Guid bankID, string name, Guid? provinceID = null, CancellationToken cancellationToken = default);
        Task<BankBranchPaging> GetBankBranchListAsync(BankBranchFilter filter, PageParam pageParam, BankBranchSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BankBranchDTO> GetBankBranchAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BankBranchDTO> CreateBankBranchAsync(BankBranchDTO input);
        Task<BankBranchDTO> UpdateBankBranchAsync(Guid id, BankBranchDTO input);
        Task<BankBranch> DeleteBankBranchAsync(Guid id);
    }
}
