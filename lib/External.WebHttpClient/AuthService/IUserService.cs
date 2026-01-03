using Base.DTOs.AppAuth;
using Common.Helper.Logging;
namespace External.WebHttpClient
{
    public interface IUserService
    {
        LogModel LogModel { get; set; } 

        Task<ValidateLoginDTO> ValidateMainRole(string token, string empCode);

    }
}