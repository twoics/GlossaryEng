using GlossaryEng.Auth.Data.Entities;

namespace GlossaryEng.Auth.Services.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateAccessToken(UserDb user);

    string GenerateRefreshToken(UserDb user);
}