using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Token;

namespace GlossaryEng.Auth.Services.TokenGenerator;

public interface ITokenGenerator
{
    /// <summary>
    /// Generates an access token
    /// </summary>
    /// <param name="user">User who needs a token</param>
    /// <returns>Access token</returns>
    TokenAccess GenerateAccessToken(UserDb user);

    /// <summary>
    /// Generates an refresh token
    /// </summary>
    /// <returns>Refresh Token</returns>
    TokenRefresh GenerateRefreshToken();
}