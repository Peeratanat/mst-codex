using System;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Auth
{
    public interface IJwtHandler
    {
        ClientJsonWebToken ClientCreate(Guid userId, string displayName);
        JsonWebToken Create(Guid userId, string displayName, string refreshToken = null);
    }
}
