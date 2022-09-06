using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.CustomResult;
using GlossaryEng.Auth.Models.Requests;
using GlossaryEng.Auth.Models.Responses;

namespace GlossaryEng.Auth.Services.Authenticator;

public interface IAuthenticator
{
    Task<AuthenticatedUserResponse> AuthenticateUserAsync(UserDb user);

    Task<CustomResult> DeleteTokenAsync(string tokenRefresh);

    Task<CustomResult> LogoutAsync(LogoutRequest logoutRequest);
    
    Task<UserDb?> GetUserFromRefreshToken(string tokenRefresh);
}