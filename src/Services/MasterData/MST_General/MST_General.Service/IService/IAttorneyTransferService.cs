using Base.DTOs.MST;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface IAttorneyTransferService : BaseInterfaceService
    {
        Task<List<AttorneyTransferListDTO>> GetAttorneyTransferDropdownListAsync(string name, CancellationToken cancellationToken = default);
        //Task<AttorneyTransferPaging> GetAttorneyTransferListAsync(AttorneyTransferFilter filter, PageParam pageParam, AttorneyTransferSortByParam sortByParam);
        //Task<AttorneyTransferDTO> GetAttorneyTransferAsync(Guid id);
        //Task<AttorneyTransferDTO> CreateAttorneyTransferAsync(AttorneyTransferDTO input);
        //Task<AttorneyTransferDTO> UpdateAttorneyTransferAsync(Guid id, AttorneyTransferDTO input);
        //Task<AttorneyTransfer> DeleteAttorneyTransferAsync(Guid id);
    }
}
