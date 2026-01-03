using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using PagingExtensions;
using PRJ_UnitInfos.Params.Filters;
using PRJ_UnitInfos.Params.Outputs;
using PRJ_UnitInfos.Services; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_UnitInfos.Services
{
    public interface IHomeInspectionService : BaseInterfaceService
    {
        Task<List<PublicAppointmentDTO>> GetPublicAppointmentCalendar(PublicAppointmentFilter body, Guid? usrID, CancellationToken cancellationToken = default);
        Task<bool> CreateAppointment(CreateAppointmentDTO body, Guid? usrID, CancellationToken cancellationToken = default);
        Task<bool> UpdateAppointment(CreateAppointmentDTO body, Guid? usrID, CancellationToken cancellationToken = default);
        Task<List<PublicAppointmentListDTO>> GetPublicAppointment(PublicAppointmentFilter body, Guid? usrID, CancellationToken cancellationToken = default);
        Task<bool> DeleteAppointment(DeleteDefecttrackingInput body, Guid? usrID, CancellationToken cancellationToken = default);
        Task<List<SeDTO>> GetSEByProjectDropdown(string projectCode);
        Task<List<InspectionTypeDTO>> GetMasterInspectionType();
    }
}
