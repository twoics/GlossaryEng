using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Responses;

namespace GlossaryEng.Auth.Services.Authenticator;

public interface IAuthenticator
{
    AuthenticatedUserResponse AuthenticateUser(UserDb user);
}