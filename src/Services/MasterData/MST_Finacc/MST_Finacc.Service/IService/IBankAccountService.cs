using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.MST;
using MST_Finacc.Params.Filters;
using MST_Finacc.Params.Outputs;
using PagingExtensions;

namespace MST_Finacc.Services
{
    public interface IBankAccountService : BaseInterfaceService
    {
        Task<List<BankAccountDropdownDTO>> GetBankAccountDropdownListAsync(string displayName, string bankAccountTypeKey, Guid? companyID, bool? IsWrongAccount, string paymentMethodTypeKey,bool? IsActive, BankAccountDropdownSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BankAccountPaging> GetBankAccountListAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam, CancellationToken cancellationToken = default);
        Task<BankAccountDTO> GetBankAccountDetailAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BankAccountDTO> CreateBankAccountAsync(BankAccountDTO input);
        Task<BankAccountDTO> CreateChartOfAccountAsync(BankAccountDTO input);
        Task<BankAccountDTO> UpdateBankAccountAsync(Guid id, BankAccountDTO input);
        Task<BankAccountDTO> UpdateChartOfAccountAsync(Guid id, BankAccountDTO input);
        Task DeleteBankAccountAsync(Guid id);
        Task DeleteBankAccountListAsync(List<BankAccountDTO> inputs);

        Task<FileDTO> ExportExcelBankAccAsync(BankAccountFilter filter, PageParam pageParam, BankAccountSortByParam sortByParam, CancellationToken cancellationToken = default);
    }
}
