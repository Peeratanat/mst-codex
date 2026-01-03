using System;
using System.Threading.Tasks;
using Auth;
using Auth_User.Params.Inputs;
using Auth_User.Services.CustomModel;

namespace Auth_User.Services
{
    public interface ITokenService
    {
        Task<ClientJsonWebToken> ClientLoginAsync(ClientLoginParam input);
        Task<JsonWebToken> LoginAsync(LoginParam input);
        Task LogoutAsync(LogoutParam input);

        Task<JsonWebToken> LoginFromAuth(Guid GuID, string EmployeeNo = null);

        Task<JsonWebToken> LoginFromAuthByToken(string GuID, string EmployeeNo = null);

        Task<GenerateUserTokenResult> GenerateUserToken(Guid UserGUID);
    }
}
