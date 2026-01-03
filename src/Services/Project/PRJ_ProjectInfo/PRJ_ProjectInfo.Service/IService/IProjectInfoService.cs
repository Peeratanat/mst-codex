using Base.DTOs.PRJ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PRJ_ProjectInfo.Services
{
    public interface IProjectInfoService : BaseInterfaceService
    {
        Task<ProjectInfoDTO> GetProjectInfoAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProjectInfoDTO> UpdateProjectInfoAsync(Guid id, ProjectInfoDTO input, Guid? userID);
    }
}
