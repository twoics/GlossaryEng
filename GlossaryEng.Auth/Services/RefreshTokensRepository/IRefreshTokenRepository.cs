using GlossaryEng.Auth.Data.Entities;
using GlossaryEng.Auth.Models.Requests;

namespace GlossaryEng.Auth.Services.RefreshTokensRepository;

public interface IRefreshTokenRepository
{
    Task CreateToken(RefreshToken refreshToken);

    Task DeleteTokenIfExist(UserDb user);
}