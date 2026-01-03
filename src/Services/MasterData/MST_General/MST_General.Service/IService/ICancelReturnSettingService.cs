using Base.DTOs.MST;

namespace MST_General.Services
{
    public interface ICancelReturnSettingService : BaseInterfaceService
    {
        Task<CancelReturnSettingDTO> GetCancelReturnSettingAsync(CancellationToken cancellationToken = default);
        Task<CancelReturnSettingDTO> UpdateCancelReturnSettingAsync(Guid id, CancelReturnSettingDTO input);
    }
}
