
using Base.DTOs;
using Base.DTOs.PRJ;
using PagingExtensions;
using PRJ_Project.Params.Outputs;
namespace PRJ_Project.Services
{
    public interface IAgreementService : BaseInterfaceService
    {
        Task<AgreementDTO> UpdateAgreementAsync(Guid projectID, Guid id, AgreementDTO input);
        Task<AgreementDTO> GetAgreementAsync(Guid projectID, CancellationToken cancellationToken = default);
    }
}
