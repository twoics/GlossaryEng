using System.Security.Claims;
using GlossaryEng.Auth.Data.Entities;

namespace GlossaryEng.Auth.Models.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateAccessToken(UserDb user);

    string GenerateRefreshToken(UserDb user);
}