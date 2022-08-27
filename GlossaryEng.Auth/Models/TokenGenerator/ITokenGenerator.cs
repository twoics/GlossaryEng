using System.Security.Claims;

namespace GlossaryEng.Auth.Models.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateAccessToken();

    string GenerateRefreshToken();
}