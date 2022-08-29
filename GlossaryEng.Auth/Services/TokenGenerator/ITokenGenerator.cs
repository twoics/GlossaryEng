using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Token;

namespace GlossaryEng.Auth.Services.TokenGenerator;

public interface ITokenGenerator
{
    TokenAccess GenerateAccessToken(UserDb user);

    TokenRefresh GenerateRefreshToken(UserDb user);
}