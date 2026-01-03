using Base.DTOs.MST;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface IBOConfigurationService : BaseInterfaceService
    {
        Task<BOConfigurationDTO> GetBOConfigurationAsync(CancellationToken cancellationToken = default);
        Task<BOConfigurationDTO> UpdateBOConfigurationAsync(Guid id, BOConfigurationDTO input);
    }
}
