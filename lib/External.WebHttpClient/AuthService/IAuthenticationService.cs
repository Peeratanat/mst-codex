using Base.DTOs.Auth;

namespace External.WebHttpClient
{
    public interface IAuthenticationService
    {
        Task<IntrospecDTO> GetIntrospect(string token);
    }
}
