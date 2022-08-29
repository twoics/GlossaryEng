using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Responses;

namespace GlossaryEng.Auth.Services.Authenticator;

public interface IAuthenticator
{
    Task<AuthenticatedUserResponse> AuthenticateUserAsync(UserDb user);
}