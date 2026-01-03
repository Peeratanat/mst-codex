using Database.Models.MST;
using MST_Finacc.Params.Filters;
using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MST_Finacc.Params.Outputs;
using PagingExtensions;
using Base.DTOs;

namespace MST_Finacc.Services
{
    public interface IBankService: BaseInterfaceService
    {
        Task<List<BankDropdownDTO>> GetBankDropdownListAsync(string name, bool? IsCreditCard, CancellationToken cancellationToken = default);
        Task<BankPaging> GetBankListAsync(BankFilter filter, PageParam pageParam, BankSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BankDTO> GetBankAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BankDTO> CreateBankAsync(BankDTO input);
        Task<BankDTO> UpdateBankAsync(Guid id, BankDTO input);
        Task<Bank> DeleteBankAsync(Guid id);
        Task<List<BankDropdownDTO>> GetBankOnlyDropdownListAsync(string name, CancellationToken cancellationToken = default);
    }
}
