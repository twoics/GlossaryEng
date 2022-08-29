using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Responses;
using GlossaryEng.Auth.Services.TokenGenerator;

namespace GlossaryEng.Auth.Services.Authenticator;

public class Authenticator : IAuthenticator
{
    private readonly ITokenGenerator _tokenGenerator;

    public Authenticator(ITokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public AuthenticatedUserResponse AuthenticateUser(UserDb user)
    {
        return new AuthenticatedUserResponse
        {
            TokenAccess = _tokenGenerator.GenerateAccessToken(user),
            TokenRefresh = _tokenGenerator.GenerateRefreshToken(user)
        };
    }
}